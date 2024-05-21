import { Component, OnDestroy } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { MainService } from 'src/core/http-services/main.service';
import { IResumeTextModel } from 'src/core/models/main.model';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnDestroy {
  private destroy$ = new Subject<void>();
  file: any;
  showError: boolean = false;
  responseString: string = '';
  displayString: string = "";
  currentIndex: number = 0;
  interval: any;
  showResponse: boolean = false;
  showLoader: boolean = false;

  constructor(private service: MainService) { this.startDisplaying(); }

  onFileChange(event: any) {
    this.file = event.target.files[0];
    if (this.file) {
      this.showError = false;
    }
  }

  processResume() {
    if (!this.file) {
      this.showError = true;
    } else {
      this.showError = false
      this.showResponse = true;
      this.showLoader = true;
      this.service.sendFile(this.file).pipe(takeUntil(this.destroy$)).subscribe((res: any) => {
        this.responseString = res;
        this.showLoader = false;
        this.startDisplaying();
      });
    }
  }

  onClearInput() {
    this.file = null;
  }

  onReload() {
    this.destroy$.next();
    this.displayString = "";
    this.showLoader = false;
    this.showResponse = false;
    this.currentIndex = 0;
    clearInterval(this.interval);
    this.onClearInput();
  }

  startDisplaying() {
    this.interval = setInterval(() => {
      if (this.currentIndex < this.responseString.length) {
        this.displayString += this.responseString.charAt(this.currentIndex);
        this.currentIndex++;
      } else {
        clearInterval(this.interval);
      }
    }, 10);
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}

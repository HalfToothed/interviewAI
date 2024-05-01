import { Component } from '@angular/core';
import { MainService } from 'src/core/http-services/main.service';
import { IResumeTextModel } from 'src/core/models/main.model';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {

  processedResume: string = '';
  file: any;

  constructor(private service : MainService){}

  onFileChange(event: any) {
    this.file = event.target.files[0];
  }

  processResume() {
    if(this.file)
    {
      this.service.sendFile(this.file).subscribe((res: any)=> {
        this.processedResume = res;
       })
    }
  }
}

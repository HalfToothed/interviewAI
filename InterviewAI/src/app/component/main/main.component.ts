import { Component } from '@angular/core';
import { MainService } from 'src/core/http-services/main.service';
import { IResumeTextModel } from 'src/core/models/main.model';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {

  resumeText: string = '';
  processedResume: string = '';

  constructor(private service : MainService){}

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.readResumeFile(file);
    }
  }

  readResumeFile(file: File) {
    const reader = new FileReader();
    reader.readAsText(file);
    reader.onload = (e) => {
      this.processedResume = e.target?.result as string;
    };
  }

  processResume() {
    const model: IResumeTextModel = {
      prompt: this.resumeText
    };

    this.service.prompt(model).subscribe((res: any) => {
      console.log(res);
        this.processedResume = res;
      },
      (error) => {
        console.error('An error occurred:', error);
      }
    );

  }
}

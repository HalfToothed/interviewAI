import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IResumeTextModel } from '../models/main.model';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(private http: HttpClient) { }

  prompt(model : IResumeTextModel){
    return this.http.post("https://localhost:7059/Master/GenerateQuestions", model, {responseType : 'text'});
  }

  sendFile(file : File){
    return this.http.post("https://localhost:7059/Master/GetFile", file, {responseType : 'text'});
  }
}
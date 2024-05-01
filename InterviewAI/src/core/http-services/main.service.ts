import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  sendFile(file : File){
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post(`${this.baseUrl}/GetFile`, formData, {responseType : 'text'});
  }
}
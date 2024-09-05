import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'LetEase Property Management';
  properties: any[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getProperties();
  }

  getProperties() {
    this.http.get<any[]>('/api/properties').subscribe(
      (result) => {
        this.properties = result;
      },
      (error) => {
        console.error('There was an error retrieving the properties!', error);
      }
    );
  }
}

import { Component } from '@angular/core';
import { APP_TITLE } from './constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = APP_TITLE;
}

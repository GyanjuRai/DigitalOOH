import { Component } from '@angular/core';
import { ErrorStateHandlerService } from './core/services/errorstate.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'DigitalOOH.Web';

  constructor(public errorState: ErrorStateHandlerService) {}
}

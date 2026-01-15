import { Component } from "@angular/core";
import { ErrorStateHandlerService } from "../../core/services/errorstate.service";

@Component({
    selector: 'something-went-wrong',
    templateUrl: './something-wrong.component.html',
    styleUrl: './something-wrong.component.css'
})
export class SomethingWrongComponent {
    
    constructor(
        public errorHandler: ErrorStateHandlerService,
        
    ) {}

    getServer(): boolean {
       return  this.errorHandler.getServer();
    }

    reload() {
        window.location.reload();
    }
}
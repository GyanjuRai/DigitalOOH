import { Component } from "@angular/core";
import { Router } from "@angular/router";

@Component({
    selector: 'non-found',
    templateUrl: './non-found.component.html',
    styleUrl: './non-found.component.css'
})
export class NonFoundComponent {
    
    constructor(private router: Router) {}

    return() {
        this.router.navigate(['/']);
    }
}
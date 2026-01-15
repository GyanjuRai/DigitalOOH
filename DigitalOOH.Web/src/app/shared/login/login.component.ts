import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuthService } from "../../core/services/auth.service";
import { AccountService } from "../../core/services/account.service";

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit, OnDestroy {
    
    formGroup!: FormGroup;

    constructor(
        public fb: FormBuilder,
        private auth: AuthService,
        private acc: AccountService,

    ) {}

    ngOnInit(): void {
      this.formGroup = this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required]
      });
    }

    ngOnDestroy(): void {
        
    }
}
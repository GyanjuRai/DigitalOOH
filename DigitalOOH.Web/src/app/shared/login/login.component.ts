import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuthService } from "../../core/services/auth.service";
import { AccountService } from "../../core/services/account.service";
import { Subject, takeUntil } from "rxjs";
import { loginResponse, userLoginParam } from "../../core/models/account.model";
import { responseModel } from "../../core/models/base.model";
import { responseEnum } from "../../core/models/enum";
import { Router } from "@angular/router";
import { SnackbarService } from "../../core/services/snackbar.service";

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit, OnDestroy {
    
    private __unsubscribeAll: Subject<any>;
    formGroup!: FormGroup;
    userLoginParam =  {} as userLoginParam;
    hidePassword: boolean = false;

    constructor(
        public fb: FormBuilder,
        private auth: AuthService,
        private acc: AccountService,
        private router: Router,
        private snackbar: SnackbarService

    ) 
    {
      this.__unsubscribeAll = new Subject<any>();
    }

    ngOnInit(): void {
      this.formGroup = this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required]
      });

      if(!this.auth.isAuthenticated()) {
        this.auth.clearAuth();
      }
    }

    async login() {
      if(this.formGroup.valid) {
        const email = this.formGroup.value.email.trim();
        const password = this.formGroup.value.password;
        this.userLoginParam = {email, password};
        await this.acc.login(this.userLoginParam)
        .pipe(takeUntil(this.__unsubscribeAll))
        .subscribe((response: responseModel<loginResponse>) => {
            if(response.type === responseEnum.sucess && response.data != null){
              this.auth.setSession(response.data);
              this.router.navigate(['/screens']);
            } else {
              this.snackbar.warning(response.message);
            }
        })
      }
    }

    ngOnDestroy(): void {
        this.__unsubscribeAll.next(null);
        this.__unsubscribeAll.complete();
    }
}
import { APP_INITIALIZER, ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { appRoutes } from './app.routing';
import { AppConst } from './app.const';
import { HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HttpErrorInterceptor } from './core/interceptor/http-interceptor';
import { GlobalErrorHandler } from './core/error-handler/global-errorhandler';
import { SomethingWrongComponent } from './errors/something-wrong/something-wrong.component';
import { NonFoundComponent } from './errors/non-found/non-found.component';
import { LoginModule } from './shared/login/login.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { SnackbarModule } from './shared/components/snackbar/snackbar.module';
import { MainLayoutComponent } from './shared/layouts/main-layout.component';
import { FeatureModule } from './features/features.module';
import { GridModule } from './shared/components/grid-config/grid-config.module';
import { CommonModule } from '@angular/common';


@NgModule({
  declarations: [
    AppComponent,
    SomethingWrongComponent,
    NonFoundComponent,
    MainLayoutComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    HttpClientModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    MatSnackBarModule,
    SnackbarModule,
    LoginModule,
  ],
  providers: [
    AppConst,
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    },
    {
      provide: APP_INITIALIZER,
      useFactory: (appConst: AppConst) => () => appConst.loadConfig(),
      deps: [AppConst],
      multi: true
    },
    provideHttpClient(),
    // {
    //   provide: HTTP_INTERCEPTORS,
    //   useClass: RequestInterceptor,
    //   multi: true
    // },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    },
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

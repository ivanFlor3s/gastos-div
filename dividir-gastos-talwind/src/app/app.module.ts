import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

//Modules
import { AppRoutingModule } from './app-routing.module';
import { ClickOutsideDirective } from './directives/click-outside.directive';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorsModule } from '../core/errors/errors.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//Components
import { AppComponent } from './app.component';

//Interceptor
import { AuthInterceptor } from '@core/auth.interceptor';

//Ngxs
import { NgxsStoragePluginModule, StorageOption } from '@ngxs/storage-plugin';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { AppState } from '@core/state/app-state/app.state';
import { NgxsModule } from '@ngxs/store';
import { SharedModule } from './shared/shared.module';
import { GroupState } from '@core/state';
import { environment } from 'src/environments/environment';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
    declarations: [AppComponent, ClickOutsideDirective],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        FormsModule,
        SharedModule,
        ErrorsModule.forRoot({ required: 'Este campo es requerido' }),
        HttpClientModule,
        NgxsModule.forRoot([AppState, GroupState], {
            developmentMode: !environment.PRODUCTION,
        }),
        NgxsReduxDevtoolsPluginModule.forRoot(),
        NgxsStoragePluginModule.forRoot({
            key: [AppState],
            storage: StorageOption.LocalStorage,
        }),
        NgxSpinnerModule.forRoot({ type: 'ball-scale-multiple' }),
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    ],
    bootstrap: [AppComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}

import { Routes, RouterModule } from '@angular/router';
import { LoginComponent, RegisterComponent } from './components';
import { AuthContainerComponent } from './container/auth-container/auth-container.component';

const routes: Routes = [
    {
        path: '',
        component: AuthContainerComponent,
        children: [
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
        ],
    },
];

export const AuthRoutes = RouterModule.forChild(routes);

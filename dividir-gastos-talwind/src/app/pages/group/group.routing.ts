import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { GroupContainerComponent } from './container';

const routes: Routes = [{ path: '', component: GroupContainerComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class GroupRoutingModule {}

import { NgModule } from '@angular/core';
import { DashboardComponent } from './pages/dashboard.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '@shared/shared.module';

const routes: Routes = [
    {
        path: '',
        component: DashboardComponent,
        data: {
            title: 'Dashboard',
        },
    },
];

@NgModule({
    imports: [SharedModule, RouterModule.forChild(routes)],
    declarations: [DashboardComponent],
})
export class DashboardModule {}

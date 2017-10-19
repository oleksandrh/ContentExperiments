import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ScriptsComponent } from './components/scripts/scripts.component';
import { ResultsComponent } from './components/results/results.component';
import { BlockUIModule } from 'ng-block-ui';
import { FormsModule } from '@angular/forms';
import { PopoverModule } from 'ng2-popover';
import { MatDialogModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

export const sharedConfig: NgModule = {
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        ScriptsComponent,
        ResultsComponent,
        HomeComponent
    ],
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'scripts', component: ScriptsComponent },
            { path: 'results', component: ResultsComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        BlockUIModule,
        FormsModule,
        PopoverModule,
        MatDialogModule,
        BrowserAnimationsModule
    ]
};

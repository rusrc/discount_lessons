import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InitComponent } from './pages/home-page/components/init/init.component';
import { FilterComponent } from './pages/home-page/components/filter/filter.component';
import { CardComponent } from './pages/home-page/components/card/card.component';


@NgModule({
  declarations: [
    AppComponent,
    InitComponent,
    FilterComponent,
    CardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

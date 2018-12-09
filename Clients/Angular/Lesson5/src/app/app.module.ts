import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InitComponent } from './pages/home-page/components/init/init.component';
import { FilterComponent } from './pages/home-page/components/filter/filter.component';
import { CardComponent } from './pages/home-page/components/card/card.component';
import { AboutComponent } from './pages/about-page/about/about.component';


@NgModule({
  declarations: [
    AppComponent,
    InitComponent,
    FilterComponent,
    CardComponent,
    AboutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InitComponent } from './pages/home-page/components/init/init.component';
import { InitComponent as InitPromotionItemComponent } from './pages/promotion-item-page/components/init/init.component';

@NgModule({
  declarations: [
    AppComponent,
    InitComponent,
    InitPromotionItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

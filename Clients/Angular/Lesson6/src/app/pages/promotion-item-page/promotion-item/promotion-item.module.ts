import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PromotionItemRoutingModule } from './promotion-item-routing.module';
import { InitComponent } from '../components/init/init.component';

@NgModule({
  declarations: [InitComponent],
  imports: [
    CommonModule,
    PromotionItemRoutingModule
  ]
})
export class PromotionItemModule { }

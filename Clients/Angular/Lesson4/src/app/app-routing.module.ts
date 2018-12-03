import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InitComponent } from './pages/home-page/components/init/init.component';
import { InitComponent as InitPromotionItemComponent } from './pages/promotion-item-page/components/init/init.component';

const routes: Routes = [
  { path: '', component: InitComponent },
  { path: 'promotion-item', component: InitPromotionItemComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

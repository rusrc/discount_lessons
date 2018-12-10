import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AboutComponent } from './pages/about-page/about/about.component';
import { InitComponent } from './pages/home-page/components/init/init.component';

const routes: Routes = [
  { path: '', component: InitComponent },
  { path: 'about', component: AboutComponent },
  {
    path: 'promotion-item',
    loadChildren: './pages/promotion-item-page/promotion-item/promotion-item.module#PromotionItemModule'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

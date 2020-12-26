import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { PreloadAllModules, RouterModule } from "@angular/router";
import { AppComponent } from "./app.component";
import { HeaderComponent } from "./navbar/header/header.component";
import { LoginNavComponent } from "./navbar/login-nav/login-nav.component";
import { ROUTES } from "./app.routes";
import { HomeComponent } from "./home/home.component";
import { BrinquedosComponent } from "./brinquedos/brinquedos.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { CreateUsuarioComponent } from "./usuario/create-usuario/create-usuario.component";
import { InputComponent } from "./shared/input/input.component";
import { CommonModule } from "@angular/common";
import { NotificationService } from "./shared/messages/notification.service";
import { SnackbarComponent } from "./shared/messages/snackbar/snackbar.component";
import {} from "rxjs";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgxMaskModule, IConfig } from "ngx-mask";
import { LoginComponent } from "./login/login.component";
import { JwtInterceptor } from "./_helper/jwt.interceptor";
import { ErrorInterceptor } from "./_helper/error.interceptor";
import { AddPropostaComponent } from "./proposta/add-proposta/add-proposta.component";
import { FooterComponent } from "./navbar/footer/footer.component";
import { BrinquedoDetalheComponent } from "./brinquedos/brinquedo-detalhe/brinquedo-detalhe.component";
import { CreateAnuncioComponent } from "./anuncio/create-anuncio/create-anuncio.component";
import { PaginationComponent } from "./shared/pagination/pagination.component";
import { RegionPageComponent } from "./shared/region-page/region-page.component";
import { ListBaseComponent } from "./shared/list-base/list-base.component";
import { ServiceBaseService } from "./services/serviceBase.service";
import { AnuncioService } from "./services/anuncio.service";
import { LoginService } from "./services/login.service";
import { HomeAnuncioComponent } from "./anuncio/home-anuncio/home-anuncio.component";
import { AddBrinquedoComponent } from "./brinquedos/add-brinquedo/add-brinquedo.component";
import { ListBase } from "./shared/CRUD/ListBase";
import { ListBrinquedoComponent } from "./brinquedos/list-brinquedo/list-brinquedo.component";
import { BrinquedoService } from "./services/brinquedo.service";
import { EnderecoService } from "./services/endereco.service";
import { NgxPaginationModule } from 'ngx-pagination';
import { PagerService } from "./services/pager.service";
import { ImagensService } from "./services/imagens.service";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginNavComponent,
    HomeComponent,
    BrinquedosComponent,
    CreateUsuarioComponent,
    InputComponent,
    SnackbarComponent,
    LoginComponent,
    AddPropostaComponent,
    FooterComponent,
    BrinquedoDetalheComponent,
    CreateAnuncioComponent,
    PaginationComponent,
    RegionPageComponent,
    ListBaseComponent,
    HomeAnuncioComponent,
    AddBrinquedoComponent,
    ListBrinquedoComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    NgxPaginationModule,
    NgxMaskModule.forRoot(),
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot(ROUTES, { preloadingStrategy: PreloadAllModules }),
  ],
  exports: [InputComponent, CommonModule, FormsModule, ReactiveFormsModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    NotificationService,
    ServiceBaseService,
    LoginService,
    BrinquedoService,
    EnderecoService,
    PagerService,
    AnuncioService,
    ImagensService
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

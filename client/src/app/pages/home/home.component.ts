import { Component } from '@angular/core';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { IntroComponent } from '../../components/intro/intro.component';
import { GetStartedComponent } from '../../components/get-started/get-started.component';
import { FooterComponent } from '../../components/footer/footer.component';
@Component({
  standalone: true,
  selector: 'app-home',
  imports: [
    NavbarComponent,
    IntroComponent,
    GetStartedComponent,
    FooterComponent
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

}

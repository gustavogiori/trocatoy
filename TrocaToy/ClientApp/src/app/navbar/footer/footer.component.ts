import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-footer",
  templateUrl: "./footer.component.html",
  styleUrls: ["./footer.component.css"],
})
export class FooterComponent implements OnInit {
  today: Date;
  year: string;
  constructor() {}

  ngOnInit() {
    this.today = new Date();
    this.year = this.today.getFullYear().toString();
  }
}

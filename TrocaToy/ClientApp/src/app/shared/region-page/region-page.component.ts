import { Component, Input, OnInit } from "@angular/core";

@Component({
  selector: "app-region-page",
  templateUrl: "./region-page.component.html",
  styleUrls: ["./region-page.component.css"],
})
export class RegionPageComponent implements OnInit {
  @Input() Title;
  @Input() classIcon;
  constructor() {}

  ngOnInit() {}
}

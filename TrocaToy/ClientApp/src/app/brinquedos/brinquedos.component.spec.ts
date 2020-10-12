/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BrinquedosComponent } from './brinquedos.component';

describe('BrinquedosComponent', () => {
  let component: BrinquedosComponent;
  let fixture: ComponentFixture<BrinquedosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BrinquedosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BrinquedosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

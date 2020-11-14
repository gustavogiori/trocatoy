/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ListBrinquedoComponent } from './list-brinquedo.component';

describe('ListBrinquedoComponent', () => {
  let component: ListBrinquedoComponent;
  let fixture: ComponentFixture<ListBrinquedoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListBrinquedoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListBrinquedoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

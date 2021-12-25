import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HelloBisontecaComponent } from './hello-bisonteca.component';

describe('HelloBisontecaComponent', () => {
  let component: HelloBisontecaComponent;
  let fixture: ComponentFixture<HelloBisontecaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HelloBisontecaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HelloBisontecaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

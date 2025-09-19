import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Tableros } from './tableros';
import { Tablero } from '../../../../core/models/tablero.model';
import { mockTablero } from '../../../../testing/mocks/tablero.mock';

describe('Tableros', () => {
  let component: Tableros;
  let fixture: ComponentFixture<Tableros>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Tableros]
    }).compileComponents();

    fixture = TestBed.createComponent(Tableros);
    component = fixture.componentInstance;

    // Simular componentes hijos
    component.listComponent = jasmine.createSpyObj('ListTableros', ['listTableros'], { tableros: [] });
    component.updateComponent = jasmine.createSpyObj('UpdateTableros', ['cargarTableroParaEditar']);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('seleccionarTablero debería cargar tablero en componente de edición', () => {
    component.seleccionarTablero(mockTablero);
    expect(component.updateComponent.cargarTableroParaEditar).toHaveBeenCalledWith(mockTablero);
  });

  it('refrescarLista debería llamar listTableros en componente de lista', () => {
    component.refrescarLista();
    expect(component.listComponent.listTableros).toHaveBeenCalled();
  });

  it('agregarTableroLista debería agregar tablero a la lista', () => {
    component.agregarTableroLista(mockTablero);
    expect(component.listComponent.tableros).toContain(mockTablero);
  });
});

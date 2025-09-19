import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateTableros } from './CreateTableros';
import { TablerosService } from '../../../../core/services/tableros.service';
import { of, throwError } from 'rxjs';
import { mockCreateTableroDTO, mockTablero } from '../../../../testing/mocks/tablero.mock';


describe('CreateTableros', () => {
  let component: CreateTableros;
  let fixture: ComponentFixture<CreateTableros>;
  let tablerosServiceMock: jasmine.SpyObj<TablerosService>;

  beforeEach(async () => {
    tablerosServiceMock = jasmine.createSpyObj('TablerosService', ['createTablero']);

    await TestBed.configureTestingModule({
      imports: [CreateTableros],
      providers: [
        { provide: TablerosService, useValue: tablerosServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CreateTableros);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('addTablero llama a createTablero y emite el evento', () => {
    component.nuevoTablero = { ...mockCreateTableroDTO };
    tablerosServiceMock.createTablero.and.returnValue(of(mockTablero));

    spyOn(component.tableroCreadoEvent, 'emit');

    component.addTablero();

    expect(tablerosServiceMock.createTablero).toHaveBeenCalledWith(mockCreateTableroDTO);
    expect(component.tableros).toContain(mockTablero);
    expect(component.tableroCreadoEvent.emit).toHaveBeenCalledWith(mockTablero);
  });

  it('addTablero maneja error correctamente', () => {
    const errorResponse = { error: { errores: [{ mensaje: 'Error de prueba' }] } };
    tablerosServiceMock.createTablero.and.returnValue(throwError(() => errorResponse));

    component.addTablero();

    expect(component.errorMessage).toEqual(['Error de prueba']);
  });
});

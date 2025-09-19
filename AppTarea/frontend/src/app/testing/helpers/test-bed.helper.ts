import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

export function setupTestingModule(providers: any[] = []) {
  TestBed.configureTestingModule({
    imports: [HttpClientTestingModule],
    providers
  });
}



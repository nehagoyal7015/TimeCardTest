import { TestBed } from '@angular/core/testing';

import { HttpErrorInterceptorServiceInterceptor } from './http-error-interceptor-service.interceptor';

describe('HttpErrorInterceptorServiceInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      HttpErrorInterceptorServiceInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: HttpErrorInterceptorServiceInterceptor = TestBed.inject(HttpErrorInterceptorServiceInterceptor);
    expect(interceptor).toBeTruthy();
  });
});

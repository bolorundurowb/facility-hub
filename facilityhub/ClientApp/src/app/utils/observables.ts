import { Observable } from 'rxjs';

declare module 'rxjs' {
  interface Observable<T> {
    asPromise(): Promise<T>;
  }
}

Observable.prototype.asPromise = function <T>() {
  return new Promise<T>((resolve, reject) => {
    this.subscribe({
      next: (value) => resolve(value),
      error: (error: any) => reject(error),
    });
  });
};

export interface OperationResult<T> {
  errorMessage: string | null;
  status: number;
  result: T;
}

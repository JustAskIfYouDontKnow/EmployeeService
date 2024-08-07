namespace Domain.Operation
{
    public class OperationResult<TResult> : OperationResult
    {

        public OperationResult() { }

        public OperationResult(TResult? result) : this()
        {
            Result = result;
        }

        public new static OperationResult<TResult> Success => new() { Status = OperationResultStatus.Success };

        public TResult? Result { get; set; }

        public new static OperationResult<TResult> Copy(OperationResult result)
        {
            return new OperationResult<TResult>
            {
                Status = result.Status,
                ErrorMessage = result.ErrorMessage,
                Exceptions = result.Exceptions,
                SuccessMessage = result.SuccessMessage,
            };
        }

        public new static OperationResult<TResult> Fail(Exception e)
        {
            return new OperationResult<TResult>
            {
                Status = OperationResultStatus.Failure,
                ErrorMessage = e.Message,
                Exceptions = new[] { e }
            };
        }

        public new static OperationResult<TResult> FailCustom(string message)
        {
            return new OperationResult<TResult>
            {
                Status = OperationResultStatus.Failure,
                ErrorMessage = message
            };
        }

        public static implicit operator TResult?(OperationResult<TResult?> value) => value.Result;

        public static implicit operator OperationResult<TResult>(TResult value) => new()
        {
            Result = value,
            Status = OperationResultStatus.Success,
            Exceptions = new List<Exception>()
        };
    }
}

namespace Domain.Operation
{
    public static class Operation
    {

        public static OperationResult Run(Action action)
        {
            try
            {
                action();

                return OperationResult.Success;
            }
            catch (Exception e)
            {
                return new OperationResult
                {
                    Status = OperationResultStatus.Failure,
                    ErrorMessage = e.Message,
                    Exceptions = new[] {e}
                };
            }
        }

        public static OperationResult<T> Run<T>(Func<T> func)
        {
            try
            {
                var result = func();
                return new OperationResult<T>
                {
                    Result = result,
                    Status = OperationResultStatus.Success,
                    Exceptions = new List<Exception>()
                };
            }
            catch (Exception e)
            {
                return new OperationResult<T>
                {
                    Status = OperationResultStatus.Failure,
                    ErrorMessage = e.Message,
                    Exceptions = new[] {e}
                };
            }
        }
        
        public static async Task<OperationResult<T>> RunAsync<T>(Func<Task<T>> func)
        {
            try
            {
                var result = await func();
                return new OperationResult<T>
                {
                    Result = result,
                    Status = OperationResultStatus.Success,
                    Exceptions = new List<Exception>()
                };
            }
            catch (Exception e)
            {
                return new OperationResult<T>
                {
                    Status = OperationResultStatus.Failure,
                    ErrorMessage = e.Message,
                    Exceptions = new[] { e }
                };
            }
        }
        
        public static async Task<OperationResult> RunAsync(Func<Task> func)
        {
            try
            {
                await func();
                return OperationResult.Success;
            }
            catch (Exception e)
            {
                return new OperationResult
                {
                    Status = OperationResultStatus.Failure,
                    ErrorMessage = e.Message,
                    Exceptions = new[] { e }
                };
            }
        }

    }
}
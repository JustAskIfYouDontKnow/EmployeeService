using System.Text.Json.Serialization;

namespace Domain.Operation
{
    public class OperationResult
    {
        public static OperationResult Success => new OperationResult { Status = OperationResultStatus.Success };

        public string? ErrorMessage { get; set; }
        [JsonIgnore] public IEnumerable<Exception>? Exceptions { get; set; }
        [JsonIgnore] public string? SuccessMessage { get; set; }
        public OperationResultStatus Status { get; set; }
        

        public static OperationResult Copy(OperationResult result)
        {
            return new OperationResult
            {
                Status = result.Status,
                ErrorMessage = result.ErrorMessage,
                Exceptions = result.Exceptions,
                SuccessMessage = result.SuccessMessage,
            };
        }

        public static OperationResult Fail(Exception e)
        {
            return new OperationResult
            {
                Status = OperationResultStatus.Failure,
                ErrorMessage = e.Message,
                Exceptions = new[] { e }
            };
        }

        public static OperationResult FailCustom(string message)
        {
            return new OperationResult
            {
                Status = OperationResultStatus.Failure,
                ErrorMessage = message
            };
        }

    }
}
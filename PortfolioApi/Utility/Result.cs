namespace PortfolioApi.Utility
{
    public class Result<T>
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        // Nullable in case of Failure
        // Needs to be generic because we need to send lists and single objects etc.
        public T? Data { get; set; }

        public static Result<T> Success(T data, string message) => new Result<T> { Successful = true, Data = data, Message = message };
        public static Result<T> Failure(string message) => new Result<T> { Successful = false, Message = message };

    }
}

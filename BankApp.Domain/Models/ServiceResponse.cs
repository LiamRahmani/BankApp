namespace BankApp.Domain.Models
{
    // Returning a wrapper object to the client with every service call
    // Advantages : you can add additional info to the returning result, like a success or exception message
    // - the front-end is able to react to this additional info and read the actual data with the help of HTTP interceptors, for instance
    // and we can make use of generics to use the correct types.
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
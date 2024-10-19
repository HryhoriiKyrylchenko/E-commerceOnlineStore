namespace E_commerceOnlineStore.Utilities
{
    /// <summary>
    /// Represents the result of an operation that can either succeed or fail, 
    /// with optional data and error messages.
    /// </summary>
    /// <typeparam name="T">The type of data returned if the operation is successful.</typeparam>
    public class OperationResult<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Gets or sets the data returned by the operation if it was successful.
        /// This property is optional and will be null if the operation failed.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Gets or sets a list of error messages encountered during the operation.
        /// This property will contain any validation errors or issues that occurred.
        /// </summary>
        public List<string> Errors { get; set; } = [];

        /// <summary>
        /// Creates an <see cref="OperationResult{T}"/> indicating a successful operation, 
        /// containing the specified data.
        /// </summary>
        /// <param name="data">The data to return in the success result.</param>
        /// <returns>An <see cref="OperationResult{T}"/> with a success status and the provided data.</returns>
        public static OperationResult<T> SuccessResult(T data)
        {
            return new OperationResult<T> { Succeeded = true, Data = data };
        }

        /// <summary>
        /// Creates an <see cref="OperationResult{T}"/> indicating a failed operation, 
        /// containing the specified error messages.
        /// </summary>
        /// <param name="errors">A list of error messages encountered during the operation.</param>
        /// <returns>An <see cref="OperationResult{T}"/> with a failure status and the provided errors.</returns>
        public static OperationResult<T> FailureResult(List<string> errors)
        {
            return new OperationResult<T> { Succeeded = false, Errors = errors };
        }
    }
}

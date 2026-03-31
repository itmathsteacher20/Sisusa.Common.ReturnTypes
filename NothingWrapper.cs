namespace Sisusa.Common.ReturnTypes
{
    /// <summary>
    /// A simple static factory for returning a "nothing" value of type FailureOrNothing. This can be used in methods that return FailureOrNothing to indicate a successful operation with no meaningful value to return.
    /// </summary>
    public static class NothingWrapper
    {
        /// <summary>
        /// Gets a value that represents the absence of a result or failure.
        /// </summary>
        /// <remarks>Use this property to indicate that an operation completed without producing a value
        /// or an error. This is typically used in scenarios where a method can succeed without returning meaningful
        /// data.</remarks>
        public static FailureOrNothing Nothing => FailureOrNothing.Nothing;
    }


    ///<summary>
    /// Factory class for creating instances of <see cref="FailureOrNothing"/> and <see cref="FailureOr{T}"/>.
    /// This class provides convenient methods for generating success and failure results, allowing for consistent handling of operation outcomes across the application.
    /// </summary>
    public static class Result
    {
        /// <summary>
        /// Gets a value that represents the absence of a result or failure.
        /// </summary>
        /// <remarks>Use this property to indicate that an operation completed without returning a value
        /// or encountering a failure. This is typically used in scenarios where a method can succeed without producing
        /// a result.</remarks>
        public static FailureOrNothing Nothing => FailureOrNothing.Nothing;

        /// <summary>
        /// Creates a failed result containing the specified error message.
        /// </summary>
        /// <typeparam name="T">The type of the value that would be returned on success.</typeparam>
        /// <param name="errorMessage">The error message describing the reason for the failure. Cannot be null or empty.</param>
        /// <returns>A <see cref="FailureOr{T}"/> instance representing a failed result with the provided error message.</returns>
        public static FailureOr<T> Failure<T>(string errorMessage) => FailureOr<T>.Fail(errorMessage);

        /// <summary>
        /// Creates a failed result containing the specified exception.
        /// </summary>
        /// <typeparam name="T">The type of the value that would be returned on success.</typeparam>
        /// <param name="ex">The exception that describes the failure. Cannot be null.</param>
        /// <returns>A <see cref="FailureOr{T}"/> instance representing a failed result with the provided exception.</returns>
        public static FailureOr<T> Failure<T>(Exception ex) => FailureOr<T>.Fail(ex);

        /// <summary>
        /// Creates a failed result containing the specified error message and exception.
        /// </summary>
        /// <typeparam name="T">The type of the value that would be returned on success.</typeparam>
        /// <param name="errorMessage">The error message that describes the reason for the failure. Cannot be null or empty.</param>
        /// <param name="ex">The exception that caused the failure. Cannot be null.</param>
        /// <returns>A <see cref="FailureOr{T}" /> instance representing a failed result with the provided error message and exception.</returns>
        public static FailureOr<T> Failure<T>(string errorMessage, Exception ex) => FailureOr<T>.Fail(ex, errorMessage);

        /// <summary>
        /// Creates a failed result containing the specified failure information.
        /// </summary>
        /// <typeparam name="T">The type of the value that would be returned on success.</typeparam>
        /// <param name="failure">The details of the failure.</param>
        /// <returns>A <see cref="FailureOr{T}"/> instance representing a failed result with the provided error message and exception.</returns>
        public static FailureOr<T> Failure<T>(Failure failure) => FailureOr<T>.Fail(failure);

        /// <summary>
        /// Creates a successful result containing the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the value to be wrapped in the successful result.</typeparam>
        /// <param name="value">The value to be wrapped in the successful result. Cannot be null if the result type does not allow null
        /// values.</param>
        /// <returns>A successful <see cref="FailureOr{T}"/> instance containing the specified value.</returns>
        public static FailureOr<T> Success<T>(T value) => FailureOr<T>.Succeed(value);


    }
}

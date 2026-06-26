using Sisusa.Common.ReturnTypes.Failures;

namespace Sisusa.Common.ReturnTypes
{
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
        /// Creates a failed FailureOrNothing containing the specified error message.
        /// 
        /// This is a generic failure that expects the errorMessage to explain the cause.
        /// </summary>
        /// <remarks>Delegates to FailureOrNothing.Fail.</remarks>
        /// <param name="errorMessage">The error message describing the failure.</param>
        /// <returns>A FailureOrNothing representing a failed result with the provided error message.</returns>
        public static FailureOrNothing Failure(string errorMessage) => FailureOrNothing.Fail(errorMessage);

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

        /// <summary>
        /// Creates a successful result indicating that no failure has occurred.
        /// </summary>
        /// <returns>A <see cref="FailureOrNothing"/> instance representing a successful outcome with no associated failure.</returns>
        public static FailureOrNothing Ok() => FailureOrNothing.Nothing;

        /// <summary>
        /// Creates a successful result containing the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the value to be wrapped in the successful result.</typeparam>
        /// <param name="value">The value to include in the successful result. Cannot be null if the result type does not allow null values.</param>
        /// <returns>A <see cref="FailureOr{T}"/> instance representing a successful result containing the specified value.</returns>
        public static FailureOr<T> Ok<T>(T value) => FailureOr<T>.Succeed(value);

        /// <summary>
        /// Creates a failure result indicating that the specified entity was not found.
        /// </summary>
        /// <param name="entityName">The name of the entity that could not be found. Cannot be null or empty.</param>
        /// <returns>A Failure instance representing an item not found error for the specified entity.</returns>
        public static Failure NotFound(string entityName) => new Failures.ItemNotFoundFailure(entityName);

        /// <summary>
        /// Creates a failure result indicating that the specified entity was not found.
        /// </summary>
        /// <param name="entityName">The name of the entity that could not be found. Cannot be null or empty.</param>
        /// <param name="exception">An optional exception that provides additional context for the failure. May be null.</param>
        /// <returns>A Failure instance representing an entity-not-found error for the specified entity.</returns>
        public static Failure NotFound(string entityName, Exception? exception = null)
            => new Failures.ItemNotFoundFailure(entityName, exception);

        /// <summary>
        /// Creates a failure result indicating that a specified entity was not found.
        /// </summary>
        /// <remarks>Use this method to standardize not found error reporting for entities by name and
        /// identifier. The optional exception parameter can be used to include underlying exception details if
        /// available.</remarks>
        /// <param name="entityName">The name of the entity type that was not found. Cannot be null or empty.</param>
        /// <param name="entityId">The unique identifier of the entity that was not found.</param>
        /// <param name="exception">An optional exception that provides additional context for the failure, or null if not applicable.</param>
        /// <returns>A Failure instance representing the not found error for the specified entity and identifier.</returns>
        public static Failure NotFound(string entityName, int entityId, Exception? exception = null)
            => new Failures.ItemNotFoundFailure(entityName, entityId, exception);

        /// <summary>
        /// Creates a failure result indicating that a specified entity was not found.
        /// </summary>
        /// <remarks>Use this method to standardize not found error handling when an entity cannot be
        /// located by its identifier.</remarks>
        /// <param name="entityName">The name of the entity type that was not found. Cannot be null or empty.</param>
        /// <param name="identifier">The identifier value used to search for the entity. Cannot be null or empty.</param>
        /// <param name="exception">An optional exception that provides additional context for the failure, or null if not applicable.</param>
        /// <returns>A Failure instance representing the not found error for the specified entity and identifier.</returns>
        public static Failure NotFound(string entityName, string identifier, Exception? exception = null) 
            => new Failures.ItemNotFoundFailure(entityName, identifier, exception);

        /// <summary>
        /// Creates a failure result indicating that a specified entity was not found.
        /// </summary>
        /// <param name="entityName">The name of the entity that could not be found. Cannot be null or empty.</param>
        /// <param name="identifier">The unique identifier of the entity that was not found.</param>
        /// <param name="exception">An optional exception that provides additional context for the failure, or null if not applicable.</param>
        /// <returns>A <see cref="Failure"/> instance representing the not found error for the specified entity and identifier.</returns>
        public static Failure NotFound(string entityName, Guid identifier, Exception? exception = null) 
            => new Failures.ItemNotFoundFailure(entityName, identifier, exception);

        /// <summary>
        /// Creates a failure result indicating that a specified entity was not found.
        /// </summary>
        /// <param name="entityName">The name of the entity that could not be found. Cannot be null or empty.</param>
        /// <param name="identifier">The unique identifier of the entity that was not found.</param>
        /// <param name="exception">An optional exception that provides additional context for the failure. May be null.</param>
        /// <returns>A Failure instance representing the not found error for the specified entity and identifier.</returns>
        public static Failure NotFound(string entityName, long identifier, Exception? exception = null) 
            => new Failures.ItemNotFoundFailure(entityName, identifier, exception);

        /// <summary>
        /// Creates a failure that represents a conflict with the current state of the resource.
        /// </summary>
        /// <param name="message">The error message that describes the conflict. If not specified, a default message is used.</param>
        /// <param name="exception">The exception that caused the conflict, or null if no exception is associated.</param>
        /// <returns>A Failure instance indicating a resource conflict.</returns>
        public static Failure Conflict(string message= "Operation could not be completed due to a conflict with the current state of the resource.", Exception? exception = null) 
            => new Failures.ResourceConflictFailure(message, exception);

        /// <summary>
        /// Creates a failure result that represents a constraint violation error.
        /// </summary>
        /// <param name="message">The error message that describes the constraint violation. If not specified, a default message is used.</param>
        /// <param name="exception">The exception that caused the constraint violation, or null if not applicable.</param>
        /// <returns>A Failure instance representing a constraint violation error.</returns>
        public static Failure ConstraintViolation(string message = "Operation could not be completed due to a constraint violation.", Exception? exception = null) 
            => new Failures.ConstraintViolationFailure(message, exception);

        /// <summary>
        /// Creates a failure result indicating that an operation could not be completed due to a missing or unmet
        /// dependency.
        /// </summary>
        /// <param name="message">The error message that describes the dependency issue. If not specified, a default message is used.</param>
        /// <param name="exception">An optional exception that provides additional context for the failure. May be null.</param>
        /// <returns>A Failure instance representing the unmet dependency condition.</returns>
        public static Failure DependencyNotMet(string message = "Operation could not be completed because a required dependency was not met.", Exception? exception = null) 
            => new Failures.DependencyFailure(message, exception);

        /// <summary>
        /// Creates a failure result indicating that an operation could not be completed due to an external service
        /// failure.
        /// </summary>
        /// <param name="message">The error message that describes the failure. If not specified, a default message is used.</param>
        /// <param name="exception">The exception that caused the failure, or null if no exception is available.</param>
        /// <returns>A Failure instance representing an external service failure with the specified message and exception.</returns>
        public static Failure ExternalServiceFailed(string message = "Operation could not be completed because an external service failed.", Exception? exception = null) 
            => new Failures.ExternalServiceFailure(message, exception);

        /// <summary>
        /// Creates a failure result that indicates the operation could not be completed due to invalid input.
        /// </summary>
        /// <param name="message">The error message that describes the invalid input. If not specified, a default message is used.</param>
        /// <param name="exception">An optional exception that provides additional context for the failure. May be null.</param>
        /// <returns>A Failure instance representing an invalid input error.</returns>
        public static Failure InvalidInput(string message = "Operation could not be completed due to invalid input.", Exception? exception = null) 
            => new Failures.InvalidInputFailure(message, exception);

        /// <summary>
        /// Creates a failure result that represents an unauthorized access error.
        /// </summary>
        /// <param name="message">The error message that describes the unauthorized access. If not specified, a default message is used.</param>
        /// <param name="exception">An optional exception that provides additional context for the unauthorized access. May be null.</param>
        /// <returns>A Failure instance representing an unauthorized access error.</returns>
        public static Failure Unauthorized(string message = "Operation could not be completed due to unauthorized access.", Exception? exception = null) 
            => new Failures.AuthenticationFailure(message, exception);

        /// <summary>
        /// Creates a failure result that indicates the operation was denied due to insufficient permissions.
        /// </summary>
        /// <param name="message">The error message that describes the reason for the denial. If not specified, a default message is used.</param>
        /// <param name="exception">An optional exception that provides additional context for the failure. May be null.</param>
        /// <returns>A Failure instance representing a permission-denied failure.</returns>
        public static Failure Denied(string message = "Operation could not be completed due to insufficient permissions.", Exception? exception = null) 
            => new Failures.PermissionDeniedFailure(message, exception);

        /// <summary>
        /// Creates a failure result that represents an internal error encountered during operation processing.
        /// </summary>
        /// <param name="message">The error message that describes the internal error. If not specified, a default message is used.</param>
        /// <param name="exception">The exception that caused the internal error, or null if no exception is associated.</param>
        /// <returns>A failure result indicating an internal error, containing the specified message and optional exception.</returns>
        public static Failure InternalError(string message = "An internal error occurred while processing the operation.", Exception? exception = null) 
            => new Failures.InternalFailure(message, exception);

        /// <summary>
        /// Creates a failure result that indicates an operation has timed out.
        /// </summary>
        /// <param name="message">The error message that describes the timeout condition. If not specified, a default message is used.</param>
        /// <param name="exception">An optional exception that provides additional context for the timeout. May be null.</param>
        /// <returns>A Failure instance representing a timeout error.</returns>
        public static Failure TimeOut(string message = "Operation could not be completed because it timed out.", Exception? exception = null) 
            => new Failures.TimeoutFailure(message, exception);

        /// <summary>
        /// Creates a failure result indicating that the requested operation is not implemented.
        /// </summary>
        /// <param name="message">The error message that describes the reason the operation is not implemented. If not specified, a default
        /// message is used.</param>
        /// <param name="exception">An optional exception that provides additional context for the failure. May be null.</param>
        /// <returns>A Failure instance representing a not implemented operation failure.</returns>
        public static Failure NotImplemented(string message = "Operation could not be completed because it is not implemented.", Exception? exception = null) 
            => new Failures.NotImplementedFailure(message, exception);
        
        /// <summary>
        /// Creates a failure result that indicates an operation could not be completed due to a database connection
        /// failure.
        /// </summary>
        /// <param name="message">The error message that describes the reason for the failure. If not specified, a default message is used.</param>
        /// <param name="exception">The exception that caused the failure, or null if no exception is associated.</param>
        /// <returns>A Failure instance representing a database connection failure.</returns>
        public static Failure DatabaseConnectionFailed(string message = "Operation could not be completed because the database connection failed.", Exception? exception = null) 
            => new Failures.DatabaseConnectionFailure(message, exception);

        /// <summary>
        /// Creates a failure result that indicates an operation could not be completed because an object failed validation.
        /// </summary>
        /// <param name="source">The type name of the object that failed validation.</param>
        /// <param name="path">The name of the property that failed validation.</param>
        /// <returns>A Failure instance representing a validation error.</returns>
        public static Failure FailedValidation(string source, string path)
            => new Failures.ValidationFailure(source, path);

        /// <summary>
        /// Creates a failure result that indicates an operation could not be completed because an object failed validation.
        /// </summary>
        /// <param name="source">The type name of the object that failed validation.</param>
        /// <param name="path">The name of the property that failed validation.</param>
        /// <param name="reason">The reason the validaiton failed.</param>
        /// <returns>A Failure instance representing a validation error.</returns>
        public static Failure FailedValidation(string source, string path, string reason)
            => new Failures.ValidationFailure(source, path, reason);

        /// <summary>
        /// Creates a failure result that indicates an operation could not be completed because an object failed validation.
        /// </summary>
        /// <param name="source">The type name of the object that failed validation.</param>
        /// <param name="path">The name of the property that failed validation.</param>
        /// <param name="message">An explanation of the validation failure.</param>
        /// <param name="code">A code for the validation issue.</param>
        /// <param name="validationSeverity">The severity of the validation error - either Error or Warning.</param>
        /// <param name="exception">Any <see cref="Exception"/> associated with the validation error.</param>
        /// <returns></returns>
        public static Failure FailedValidation(
            string source,
            string path,
            string message,
            string code="VALIDATION_ERROR",
            ValidationSeverity validationSeverity = ValidationSeverity.Error,
            Exception? exception = null)
            => new Failures.ValidationFailure(source, path, message, code, validationSeverity, exception);
    }
}

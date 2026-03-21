namespace Sisusa.Common.ReturnTypes.Failures;


/// <summary>
/// Represents a failure that occurs when an operation violates a defined constraint.
/// </summary>
/// <remarks>Use this class to indicate that a business rule, validation, or other constraint has been violated
/// during execution. The associated message should describe the specific constraint that was not satisfied. This
/// failure type can be used to distinguish constraint violations from other types of failures in error handling or
/// reporting scenarios.</remarks>
public class ConstraintViolationFailure : Failure
{
    /// <summary>
    /// Initializes a new instance of the ConstraintViolationFailure class with a specified error message and an
    /// optional inner exception.
    /// </summary>
    /// <param name="message">The error message that describes the constraint violation.</param>
    /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
    public ConstraintViolationFailure(string message, Exception? innerException = null)
        : base(shortCode: "ConstraintViolation", extendedDescription: message, innerException)
    {
    }
}


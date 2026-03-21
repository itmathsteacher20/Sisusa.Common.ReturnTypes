namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs when a requested operation or feature is not implemented.
/// </summary>
/// <remarks>Use this failure type to indicate that a specific functionality is not available or has not been
/// provided. This is typically used in scenarios where a method or operation is intentionally left unimplemented, such
/// as in abstract base classes or stubs. The associated message should describe the missing implementation for
/// diagnostic purposes.</remarks>
public class NotImplementedFailure : Failure
{
    /// <summary>
    /// Initializes a new instance of the NotImplementedFailure class with a specified error message and an optional
    /// inner exception.
    /// </summary>
    /// <param name="message">The message that describes the reason for the failure. This value is typically used to provide additional
    /// context about the unimplemented functionality.</param>
    /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
    public NotImplementedFailure(string message, Exception? innerException = null)
        : base(shortCode: "NotImplemented", extendedDescription: message, innerException)
    {
    }
}


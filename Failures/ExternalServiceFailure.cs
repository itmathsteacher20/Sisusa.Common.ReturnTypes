namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs when an external service is unavailable or returns an error.
/// </summary>
/// <remarks>Use this class to indicate errors resulting from dependencies on external systems, such as APIs,
/// databases, or third-party services. This type can be used to distinguish external failures from internal application
/// errors when handling or logging failures.</remarks>
public class ExternalServiceFailure : Failure
{
    /// <summary>
    /// Initializes a new instance of the ExternalServiceFailure class with a specified error message and an optional
    /// inner exception.
    /// </summary>
    /// <param name="message">The error message that describes the reason for the external service failure.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null if no inner exception is specified.</param>
    public ExternalServiceFailure(string message, Exception? innerException = null)
        : base(shortCode: "ExternalServiceFailure", extendedDescription: message, innerException)
    {
    }
}


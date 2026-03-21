namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs when an operation exceeds its allotted time limit.
/// </summary>
/// <remarks>Use this class to indicate that a timeout has caused an operation to fail. This exception type can be
/// used to distinguish timeout-related failures from other types of failures when handling errors.</remarks>
public class TimeoutFailure : Failure
{
    /// <summary>
    /// Initializes a new instance of the TimeoutFailure class with a specified error message and an optional inner
    /// exception.
    /// </summary>
    /// <param name="message">The message that describes the reason for the timeout failure.</param>
    /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
    public TimeoutFailure(string message, Exception? innerException = null)
        : base(shortCode: "Timeout", extendedDescription: message, innerException)
    {
    }
}


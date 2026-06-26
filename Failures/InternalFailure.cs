namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs due to an unexpected internal error.
/// </summary>
/// <param name="message">The error message that describes the internal failure.</param>
/// <param name="exception">The exception that caused the failure, or null if not applicable.</param>
public class InternalFailure(string message, Exception? exception = null) : Failure("InternalFailure", message, exception)
{
    
}


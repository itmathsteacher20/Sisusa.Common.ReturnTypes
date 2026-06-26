namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs due to authentication errors.
/// </summary>
/// <remarks>Use this class to indicate that an operation failed because authentication was unsuccessful. This
/// exception typically signals that credentials are missing, invalid, or expired. It can be used to distinguish
/// authentication failures from other types of errors in error handling logic.</remarks>
public class AuthenticationFailure : Failure
{
    /// <summary>
    /// Initializes a new instance of the AuthenticationFailure exception with a specified error message and an optional
    /// inner exception.
    /// </summary>
    /// <param name="message">The message that describes the reason for the authentication failure.</param>
    /// <param name="innerException">The exception that is the cause of this exception, or null if no inner exception is specified.</param>
    public AuthenticationFailure(string message, Exception? innerException = null)
        : base(shortCode: "AuthenticationFailure", extendedDescription: message, innerException)
    {
    }
}


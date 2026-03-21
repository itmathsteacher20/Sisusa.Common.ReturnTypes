namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs due to authentication errors.
/// </summary>
/// <remarks>Use this class to indicate that an operation failed because authentication was unsuccessful. This
/// exception typically signals that credentials are missing, invalid, or expired. It can be used to distinguish
/// authentication failures from other types of errors in error handling logic.</remarks>
public class AuthenticationFailure : Failure
{
    public AuthenticationFailure(string message, Exception? innerException = null)
        : base(shortCode: "AuthenticationFailure", extendedDescription: message, innerException)
    {
    }
}


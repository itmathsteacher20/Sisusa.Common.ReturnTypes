namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs when an operation is denied due to insufficient permissions.
/// </summary>
/// <remarks>Use this class to indicate that an action could not be completed because the caller does not have the
/// required permissions. This failure type is typically used in authorization scenarios to provide a standardized way
/// to report permission-related errors.</remarks>
public class PermissionDeniedFailure : Failure
{
    /// <summary>
    /// Initializes a new instance of the PermissionDeniedFailure class with a specified error message and an optional
    /// inner exception.
    /// </summary>
    /// <param name="message">The message that describes the reason for the permission denial. This value is used as the extended description
    /// of the failure.</param>
    /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
    public PermissionDeniedFailure(string message, Exception? innerException = null)
        : base(shortCode: "PermissionDenied", extendedDescription: message, innerException)
    {
    }
}


namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs when input data is invalid.
/// </summary>
/// <remarks>Use this class to indicate that an operation has failed due to invalid or malformed input provided by
/// the caller. The associated message should describe the specific input issue encountered.</remarks>
public class InvalidInputFailure : Failure
{
    /// <summary>
    /// Initializes a new instance of the InvalidInputFailure class with a specified error message and an optional inner
    /// exception.
    /// </summary>
    /// <param name="message">The error message that describes the reason for the failure. This value cannot be null.</param>
    /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
    public InvalidInputFailure(string message, Exception? innerException = null)
        : base(shortCode: "InvalidInput", extendedDescription: message, innerException)
    {
    }
}


namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs when a database connection attempt is unsuccessful.
/// </summary>
/// <remarks>Use this class to capture and convey details about database connection failures, including an
/// optional inner exception for additional context. This type is typically used in error handling scenarios to
/// distinguish database connectivity issues from other types of failures.</remarks>
public class DatabaseConnectionFailure : Failure
    {
    /// <summary>
    /// Initializes a new instance of the DatabaseConnectionFailure class with a specified error message and an optional
    /// inner exception.
    /// </summary>
    /// <param name="message">The error message that describes the reason for the database connection failure.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null if no inner exception is specified.</param>
        public DatabaseConnectionFailure(string message, Exception? innerException = null)
            : base(shortCode: "DatabaseConnectionFailure", extendedDescription: message, innerException)
        {
        }
    }


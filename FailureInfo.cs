namespace Sisusa.Common.ReturnTypes
{
    /// <summary>
    /// Represents information related to a failure, including a message, an underlying exception, and an error code.
    /// </summary>
    public class FailureInfo(string message, Exception innerException, int errorCode)
    {
        /// <summary>
        /// Gets the message associated with the failure.
        /// </summary>
        public string Message { get; private set; } = string.IsNullOrWhiteSpace(message) ? 
            throw new ArgumentNullException(paramName: nameof(message)) : message;

        /// <summary>
        /// Gets the underlying exception that caused the failure.
        /// </summary>
        public Exception InnerException { get; private set; } = innerException;

        /// <summary>
        /// Gets the error code associated with the failure.
        /// </summary>
        public int ErrorCode { get; private set; } = errorCode;

        public bool Equals(FailureInfo? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(other, null)) return false;

            return string.Equals(Message, other.Message, StringComparison.OrdinalIgnoreCase) &&
                InnerException.Equals(other.InnerException) &&
                ErrorCode == other.ErrorCode;
        }

        /// <summary>
        /// Represents information related to a failure, including a message,
        /// an underlying exception, and an error code.
        /// </summary>
        public FailureInfo(string message) : this(message, new Exception(message), 1)
        {
        }

        /// <summary>
        /// Represents information related to a failure, encapsulating a message,
        /// an underlying exception, and an error code.
        /// </summary>
        public FailureInfo(string message, Exception innerException) : this(message, innerException, 1)
        {
        }

        /// <summary>
        /// Represents information related to a failure, including a message, an underlying exception, and an error code.
        /// </summary>
        public FailureInfo(string message, int errorCode) : this(message, new Exception(message), errorCode)
        {
        }

        /// <summary>
        /// Creates a <see cref="FailureInfo"/> instance from a given exception and message.
        /// </summary>
        /// <param name="exception">The exception that caused the failure.</param>
        /// <param name="message">A descriptive message about the failure.</param>
        /// <returns>A <see cref="FailureInfo"/> object encapsulating the provided exception and message.</returns>
        public static FailureInfo FromException(Exception exception, string message)
        {
            return new(message, exception);
        }

        /// <summary>
        /// Creates a new instance of <see cref="FailureInfo"/> using the provided message.
        /// </summary>
        /// <param name="message">The message describing the failure.</param>
        /// <returns>A <see cref="FailureInfo"/> object containing the specified message.</returns>
        public static FailureInfo WithMessage(string message)
        {
            return new FailureInfo(message);
        }

        /// <summary>
        /// Associates a new exception with the current instance of <see cref="FailureInfo"/>.
        /// </summary>
        /// <param name="exception">The exception to be added as the cause of the failure.</param>
        /// <returns>The current <see cref="FailureInfo"/> instance with the updated exception.</returns>
        public FailureInfo WithException(Exception exception)
        {
            InnerException = exception;
            return this;
        }

        /// <summary>
        /// Sets the error code for this <see cref="FailureInfo"/> instance and returns the updated instance.
        /// </summary>
        /// <param name="errorCode">The error code to associate with this failure.</param>
        /// <returns>The updated <see cref="FailureInfo"/> instance with the new error code.</returns>
        public FailureInfo WithErrorCode(int errorCode)
        {
            ErrorCode = errorCode;
            return this;
        }
    }

}

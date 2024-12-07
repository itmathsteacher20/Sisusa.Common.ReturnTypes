namespace Sisusa.Common.ReturnTypes
{
    /// <summary>
    /// Represents information related to a failure, including a message, an underlying exception, and an error code.
    /// </summary>
    public class FailureInfo(string message, Exception? innerException) : IFailure
    {
        /// <summary>
        /// Gets the message associated with the failure.
        /// </summary>
        public string Message { get; init; } = string.IsNullOrWhiteSpace(message) ? 
            throw new ArgumentNullException(paramName: nameof(message)) : message;

        /// <summary>
        /// Gets the underlying exception that caused the failure.
        /// </summary>
        public Optional<Exception?> InnerException { get; private set; } = innerException is null ? 
                Optional<Exception?>.None : Optional<Exception?>.Some(innerException);

        
        public bool Equals(FailureInfo? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(other, null)) return false;

            return string.Equals(Message, other.Message, StringComparison.OrdinalIgnoreCase) &&
                   (bool)InnerException.Equals(other.InnerException);
        }

        /// <summary>
        /// Represents information related to a failure, including a message,
        /// an underlying exception, and an error code.
        /// </summary>
        public FailureInfo(string message) : this(message, new Exception(message))
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
            ArgumentNullException.ThrowIfNull(exception);
            InnerException = Optional<Exception?>.Some(exception);
            return this;
        }
        
        public static implicit operator FailureInfo(Failure failure)
        {
            return new FailureInfo(failure.Message, failure.InnerException.OrElse(default(Exception)!));
        }

        public static implicit operator Failure(FailureInfo failureInfo)
        {
            var theInnerExc = failureInfo.InnerException.OrElse(default(Exception)!);
            var excType = theInnerExc == null ? nameof(Exception) : theInnerExc.GetType().Name;
            var shortCode = $"{excType}";
            
            return new Failure(shortCode, failureInfo.Message, failureInfo.InnerException.OrElse(default(Exception)!));
        }
    }
}

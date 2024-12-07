namespace Sisusa.Common.ReturnTypes;

/// <summary>
/// Provides factory methods for creating instances of failure information such as FailureInfo and Failure.
/// </summary>
public static class FailureFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="FailureInfo"/> using the provided message.
    /// </summary>
    /// <param name="message">The message describing the failure.</param>
    /// <returns>A new <see cref="FailureInfo"/> instance containing the provided message.</returns>
    public static IFailure WithMessage(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));
            
        return new FailureInfo(message);
    }

    /// <summary>
    /// Creates an instance of <see cref="IFailure"/> representing a failure with a specified code and message.
    /// </summary>
    /// <param name="code">The code associated with the failure, identifying the type or category of failure.</param>
    /// <param name="message">A detailed message describing the failure.</param>
    /// <returns>An instance of <see cref="IFailure"/> encapsulating the provided code and message.</returns>
    public static IFailure WithCodeAndMessage(string code, string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code, nameof(code));
        ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));
            
        return new Failure(code, message, null);
    }

    /// <summary>
    /// Creates an instance of <see cref="IFailure"/> using a short code, extended description,
    /// and an inner exception.
    /// </summary>
    /// <param name="shortCode">A short code representing the failure.</param>
    /// <param name="extendedDescription">A detailed description of the failure.</param>
    /// <param name="innerException">The underlying exception associated with the failure.</param>
    /// <returns>An instance of <see cref="IFailure"/> encapsulating the provided details.</returns>
    public static IFailure WithCodeMessageAndException(string shortCode, string extendedDescription,
        Exception innerException)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(shortCode, nameof(shortCode));
        ArgumentException.ThrowIfNullOrWhiteSpace(extendedDescription, nameof(extendedDescription));
        ArgumentNullException.ThrowIfNull(innerException, nameof(innerException));
            
        return new Failure(shortCode, extendedDescription, innerException);
    }


    /// <summary>
    /// Creates a failure information instance with a specified message and an exception.
    /// </summary>
    /// <param name="message">The message describing the failure.</param>
    /// <param name="innerException">The exception that caused the failure.</param>
    /// <returns>An instance of <see cref="IFailure"/> containing the provided message and exception.</returns>
    public static IFailure WithMessageAndException(string message, Exception innerException)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));
        ArgumentNullException.ThrowIfNull(innerException, nameof(innerException));
            
        return new FailureInfo(message, innerException);
    }

    /// <summary>
    /// Creates a failure information instance with the provided message.
    /// </summary>
    /// <param name="message">The message describing the failure.</param>
    /// <returns>An instance of IFailure containing the provided message.</returns>
    public static IFailure FromMessage(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));
            
        return new FailureInfo(message);
    }

    /// <summary>
    /// Creates a new instance of FailureInfo from the provided exception, using the exception's message as the failure message.
    /// </summary>
    /// <param name="exception">The exception from which to create the failure information.</param>
    /// <returns>An IFailure instance containing the exception's message and the exception itself.</returns>
    public static IFailure FromException(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception, nameof(exception));
        return new FailureInfo(exception.Message, exception);
    }

    /// <summary>
    /// Creates an instance of <see cref="IFailure"/> based on the provided short code and extended description.
    /// </summary>
    /// <param name="shortCode">A short code representing the failure type or category. This cannot be null or whitespace.</param>
    /// <param name="extendedDescription">An extended description providing details about the failure. This cannot be null or whitespace.</param>
    /// <returns>An instance of <see cref="IFailure"/> with the specified code and description.</returns>
    public static IFailure FromCodeAndDescription(string shortCode, string extendedDescription)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(shortCode, nameof(shortCode));
        ArgumentException.ThrowIfNullOrWhiteSpace(extendedDescription, nameof(extendedDescription));
            
        return new Failure(shortCode, extendedDescription, null);
    }
}
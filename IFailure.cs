namespace Sisusa.Common.ReturnTypes;

/// <summary>
/// Defines the contract for failure information, encapsulating a failure message and an optional underlying exception.
/// </summary>
public interface IFailure
{
    /// <summary>
    /// Provides the failure message.
    /// </summary>
    string Message { get; }

    /// <summary>
    /// Gets the optional inner exception that provides more detail about the failure.
    /// </summary>
    Optional<Exception?> InnerException { get; }
}
namespace Sisusa.Common.ReturnTypes;

/// <summary>
/// Represents a failure including a short code, an extended description, and an optional inner exception.
/// </summary>
public class Failure(string shortCode, string extendedDescription, Exception? innerException) : IFailure
{
    /// <summary>
    /// Gets the short code representing the failure, ensuring it is neither null nor empty.
    /// </summary>
    public string Code { get; init; } = string.IsNullOrWhiteSpace(shortCode) ? 
        throw new ArgumentNullException(paramName: nameof(shortCode)) : shortCode.Trim();

    /// <summary>
    /// Gets the extended description associated with the failure.
    /// </summary>
    public string Description { get; init; } = string.IsNullOrWhiteSpace(extendedDescription) ? 
        throw new ArgumentNullException(paramName: nameof(extendedDescription)) : extendedDescription.Trim();

    /// <summary>
    /// Message is a combination of the Code and Description properties, providing a comprehensive message about the failure. It is formatted as "{Code}: {Description}".
    /// </summary>
    public string Message => $"{Code}: {Description}";
        
    /// <summary>
    /// Gets the optional exception that caused the current exception.
    /// </summary>
    /// <remarks>Use this property to access the underlying exception, if any, that led to the current
    /// exception. If no inner exception is present, the value will be empty.</remarks>
    public Optional<Exception?> InnerException { get; init; } = innerException is null ? Optional<Exception?>.None :
        Optional<Exception?>.Some(innerException);

    /// <summary>
    /// Determines whether the specified object is equal to the current Failure instance.
    /// </summary>
    /// <remarks>Comparison is case-insensitive for the code and description properties. The inner exception
    /// is compared using its Equals method.</remarks>
    /// <param name="obj">The object to compare with the current Failure instance.</param>
    /// <returns>true if the specified object is a Failure and has the same code, description, and inner exception as the current
    /// instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Failure flr)
            return false;
        return string.Equals(flr.Code,Code, StringComparison.OrdinalIgnoreCase) && 
               string.Equals(flr.Description, Description, StringComparison.OrdinalIgnoreCase) && 
               flr.InnerException.Equals(InnerException);
    }

    /// <summary>
    /// Gets the hash code for the current Failure instance, combining the hash codes of the code, description, and inner exception properties.
    /// </summary>
    /// <returns>The hash code for the current Failure instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Description, InnerException);
    }

    /// <summary>
    /// Converts a <see cref="Failure"/> instance to a <see cref="FailureInfo"/> instance.
    /// </summary>
    /// <remarks>This implicit conversion allows a <see cref="Failure"/> object to be used wherever a <see
    /// cref="FailureInfo"/> is expected. The conversion copies the message and inner exception from the source <see
    /// cref="Failure"/>.</remarks>
    /// <param name="failure">The <see cref="Failure"/> instance to convert.</param>
    public static implicit operator FailureInfo(Failure failure)
    {
        return new FailureInfo(failure.Message, failure.InnerException.OrElse(default(Exception)!));
    }

    /// <summary>
    /// Converts a FailureInfo instance to a Failure object, mapping its message and inner exception.
    /// </summary>
    /// <remarks>This operator enables implicit conversion from FailureInfo to Failure, allowing seamless
    /// assignment where a Failure is expected. The resulting Failure uses the type name of the inner exception, if
    /// present, as its short code.</remarks>
    /// <param name="failureInfo">The FailureInfo instance containing the error message and optional inner exception to convert.</param>
    public static implicit operator Failure(FailureInfo failureInfo)
    {
        var theInnerExc = failureInfo.InnerException.OrElse(default(Exception)!);
        var excType = theInnerExc == null ? nameof(Exception) : theInnerExc.GetType().Name;
        var shortCode = $"{excType}";
            
        return new Failure(shortCode, failureInfo.Message, failureInfo.InnerException.OrElse(default(Exception)!));
    }
}

public static class FailureExtensions
{
    public static Failure Conflict(string message, Exception? exception = null)
    {
        return new Failures.ResourceConflictFailure(message, exception);
    }

    
}
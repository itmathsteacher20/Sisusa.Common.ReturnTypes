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
        
    public string Message => $"{Code}: {Description}";
        
    public Optional<Exception?> InnerException { get; init; } = innerException is null ? Optional<Exception?>.None :
        Optional<Exception?>.Some(innerException);

    public override bool Equals(object? obj)
    {
        if (obj is not Failure flr)
            return false;
        return string.Equals(flr.Code,Code, StringComparison.OrdinalIgnoreCase) && 
               string.Equals(flr.Description, Description, StringComparison.OrdinalIgnoreCase) && 
               flr.InnerException.Equals(InnerException);
    }
        
    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Description, InnerException);
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
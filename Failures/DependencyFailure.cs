namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs due to an external dependency error.
/// </summary>
/// <remarks>Use this class to indicate that an operation failed because of an issue with an external system or
/// service, such as a database, network resource, or third-party API. This distinction can help with error handling and
/// diagnostics by identifying failures that are outside the application's direct control.</remarks>
public class DependencyFailure : Failure
{
    public DependencyFailure(string message, Exception? innerException = null)
        : base(shortCode: "DependencyFailure", extendedDescription: message, innerException)
    {
    }
}


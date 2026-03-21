namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs when a requested resource is in conflict with the current state or with another
/// resource.
/// </summary>
/// <remarks>Use this failure type to indicate situations where an operation cannot be completed due to a resource
/// conflict, such as attempting to create a resource that already exists or updating a resource in a way that violates
/// uniqueness constraints. This class is typically used in scenarios where conflict resolution or user intervention may
/// be required.</remarks>
public class ResourceConflictFailure : Failure
{
    /// <summary>
    /// Initializes a new instance of the ResourceConflictFailure class to represent a failure caused by a resource
    /// conflict.
    /// </summary>
    /// <param name="message">The message that describes the reason for the resource conflict failure.</param>
    /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
    public ResourceConflictFailure(string message, Exception? innerException = null)
        : base(shortCode: "ResourceConflict", extendedDescription: message, innerException)
    {
    }
}


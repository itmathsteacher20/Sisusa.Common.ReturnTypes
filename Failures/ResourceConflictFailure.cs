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

/// <summary>
/// Represents a failure that occurs when attempting to add a duplicate item.
/// </summary>
/// <param name="message">The error message that describes the reason for the failure.</param>
/// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
public class DuplicateItemFailure(string message, Exception? innerException = null) :
    Failure("DuplicateItem", message, innerException)
{
    //public DuplicateItemFailure()
}

/// <summary>
/// Specifies the severity level of a validation result.
/// </summary>
/// <remarks>Use Error for validation failures that prevent an operation from completing; use Warning for
/// non-blocking issues that should be addressed. Error represents a higher severity than Warning.</remarks>
public enum ValidationSeverity
{
    /// <summary>
    /// Represents an error condition.
    /// </summary>
    Error = 0,

    /// <summary>
    /// Represents a warning condition.
    /// </summary>
    Warning = 1
}

/// <summary>
/// Represents a validation failure associated with a subject and property path, carrying a severity, code, and optional
/// inner exception.
/// </summary>
/// <remarks>The constructor formats the base Failure message as "[{code}] {source}.{property} : {message}" and
/// initializes Subject, PropertyPath, and Severity. Subject and PropertyPath are required; the constructor throws
/// ArgumentNullException when either is null.</remarks>
public class ValidationFailure
    : Failure
{
    /// <summary>
    /// Gets the subject of the message.
    /// </summary>
    /// <remarks>Init-only; initialized to an empty string by default.</remarks>
    public string Subject { get; init; } = string.Empty;

    /// <summary>
    /// The property path associated with the validation failure, indicating the specific property or field that failed validation.
    /// This path can be used to identify the location of the failure within a complex object graph, allowing for more precise error reporting and handling.
    /// </summary>
    public string PropertyPath { get; init;  } = string.Empty;

    /// <summary>
    /// Specifies the severity level of the validation failure, indicating whether it is an error or a warning. The default is Error, which represents a failure that prevents an operation from completing. 
    /// Warning represents a non-blocking issue that should be addressed but does not prevent completion.
    /// </summary>
    public ValidationSeverity Severity { get; init;  } = ValidationSeverity.Error;

    /// <summary>
    /// Initializes a new instance of the ValidationFailure class with the specified source, property, message, code, severity, and optional inner exception.
    /// </summary>
    /// <param name="source">The source or subject of the validation failure.</param>
    /// <param name="property">The property path associated with the validation failure.</param>
    /// <param name="message">The error message that describes the reason for the validation failure.</param>
    /// <param name="code">The code that identifies the type of validation failure.</param>
    /// <param name="severity">The severity level of the validation failure.</param>
    /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
    /// <exception cref="ArgumentNullException">If source or property are not specified.</exception>
    public ValidationFailure(
        string source,
        string property,
        string message, 
        string code, 
        ValidationSeverity severity,
        Exception? innerException) : base(code, $"[{code}] {source}.{property} : {message}", innerException )
    {
        Subject = source ?? throw new ArgumentNullException(nameof(source));
        PropertyPath = property ?? throw new ArgumentNullException(nameof(property));
        Severity = severity;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationFailure"/> class with the specified source, property and reason
    /// </summary>
    /// <param name="source">The type name of the object associated with the validation failure</param>
    /// <param name="property">The property path associated with the validation failure</param>
    /// <param name="reason">The reason for the validation failure</param>
    public ValidationFailure(string source, string property, string reason)
        : this(source, property, $"`{property}` on `{source}` failed validation -> {reason}", "VALIDATION_ERROR", ValidationSeverity.Error, null)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationFailure"/> class with the specified source and property
    /// </summary>
    /// <param name="source">The type name of the object associated with the validation failure</param>
    /// <param name="property">The property path associated with the validation failure</param>
    public ValidationFailure(string source, string property)
        :this(source, property,$"`{property}` on `{source}` failed to validate.", "VALIDATION_ERROR", ValidationSeverity.Error, null)
    {

    }


}


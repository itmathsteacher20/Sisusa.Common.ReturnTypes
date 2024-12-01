namespace Sisusa.Common
{
    /// <summary>
    /// Represents an error that occurs during validation of a property within a system.
    /// </summary>
    public class ValidationError(string propertyName, string reasonForFail)
    {
        /// <summary>
        /// Gets the name of the property that caused the validation error.
        /// </summary>
        public string PropertyName { get; init; } = propertyName;

        /// <summary>
        /// Gets the reason for the validation error.
        /// </summary>
        public string ReasonForError { get; private set; } = reasonForFail;

        public override string ToString()
        {
            return ReasonForError;
        }

        //private void ChangeReason(string newReason)

        /// <summary>
        /// Creates a new <see cref="ValidationError"/> instance with a specified property name.
        /// </summary>
        /// <param name="name">The name of the property associated with the validation error.</param>
        /// <returns>A <see cref="ValidationError"/> object with a specified property name and a default error message.</returns>
        public static ValidationError Property(string name)
        {
            return new(name, $"A validation error on {name} occurred.");
        }

        /// <summary>
        /// Creates a <see cref="ValidationError"/> indicating that the associated property's value should be greater than a specified threshold.
        /// </summary>
        /// <param name="threshold">The threshold value that the property's value should exceed.</param>
        /// <returns>A <see cref="ValidationError"/> object detailing that the property's value is expected to be greater than the specified threshold.</returns>
        public ValidationError ShouldBeGreaterThan(IComparable threshold)
        {
            return new(PropertyName, $"{PropertyName} should be greater than {threshold}");
        }

        /// <summary>
        /// Creates a <see cref="ValidationError"/> indicating that the associated property's value should be at least a specified threshold.
        /// </summary>
        /// <param name="threshold">The minimum value that the property's value should meet or exceed.</param>
        /// <returns>A <see cref="ValidationError"/> object detailing that the property's value is expected to be at least the specified threshold.</returns>
        public ValidationError ShouldBeAtLeast(IComparable threshold)
        {
            return new(PropertyName, $"{PropertyName} should be at least {threshold}.");
        }

        /// <summary>
        /// Creates a <see cref="ValidationError"/> indicating that the associated property's value should be at most a specified threshold.
        /// </summary>
        /// <param name="threshold">The maximum allowable value for the property.</param>
        /// <returns>A <see cref="ValidationError"/> object detailing that the property's value should not exceed the specified threshold.</returns>
        public ValidationError ShouldBeAtMost(IComparable threshold)
        {
            return new(PropertyName, $"{PropertyName} should be at most {threshold}.");
        }

        /// <summary>
        /// Creates a <see cref="ValidationError"/> indicating that the associated property's value should be less than a specified threshold.
        /// </summary>
        /// <param name="threshold">The threshold value that the property's value should not exceed.</param>
        /// <returns>A <see cref="ValidationError"/> object detailing that the property's value is expected to be less than the specified threshold.</returns>
        public ValidationError ShouldBeLessThan(IComparable threshold)
        {
            return new(PropertyName, $"{PropertyName} should be less than {threshold}");
        }

        /// <summary>
        /// Creates a <see cref="ValidationError"/> indicating that the associated property's value should not be empty or null.
        /// </summary>
        /// <returns>A <see cref="ValidationError"/> object detailing that the property's value is expected to be non-empty and non-null.</returns>
        public ValidationError ShouldNotBeEmpty()
        {
            return new(PropertyName, $"{PropertyName} should not be empty or null");
        }

        /// <summary>
        /// Creates a <see cref="ValidationError"/> indicating that the associated property should not be null.
        /// </summary>
        /// <returns>A <see cref="ValidationError"/> object stating that the property's value is null and should be a valid non-null value.</returns>
        public ValidationError ShouldNotBeNull()
        {
            return new(PropertyName, $"{PropertyName} is null and should have a valid value.");
        }

        /// <summary>
        /// Creates a <see cref="ValidationError"/> indicating that the associated property's value should represent a date in the future.
        /// </summary>
        /// <returns>A <see cref="ValidationError"/> object detailing that the property's value must not be a date in the past.</returns>
        public ValidationError ShouldHaveFutureDate()
        {
            return new(PropertyName, $"{PropertyName} cannot be a date in the past.");
        }
    }

    /// <summary>
    /// Provides extension methods for the <see cref="ValidationError"/> class.
    /// </summary>
    public static class ValidationErrorExtensions
    {

        /// <summary>
        /// Concatenates the string representations of the specified <see cref="ValidationError"/> array, each separated by a new line.
        /// </summary>
        /// <param name="validationErrors">An array of <see cref="ValidationError"/> objects to flatten into a single string.</param>
        /// <returns>A single string containing each validation error message, separated by new lines.</returns>
        public static string Flatten(this ValidationError[] validationErrors)
        {
            var eMsgs = validationErrors.Select(x => x.ReasonForError);
            return string.Join(Environment.NewLine, eMsgs);
        }

        /// <summary>
        /// Converts the specified <see cref="ValidationError"/> into a <see cref="ValidationException"/>.
        /// </summary>
        /// <param name="validationError">The <see cref="ValidationError"/> instance to convert to an exception.</param>
        /// <returns>A <see cref="ValidationException"/> initialized with the error message from the <see cref="ValidationError"/>.</returns>
        public static Exception AsException(this ValidationError validationError)
        {
            ArgumentNullException.ThrowIfNull(validationError);
            
            return new ValidationException(validationError.ReasonForError ?? "A validation error occurred.");
        }
    }

    /// <summary>
    /// Represents an exception that is thrown when a validation error occurs in the system.
    /// </summary>
    public class ValidationException(string message) : Exception(message)
    {
    }
}

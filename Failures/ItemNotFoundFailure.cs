namespace Sisusa.Common.ReturnTypes.Failures;

/// <summary>
/// Represents a failure that occurs when a requested item cannot be found.
/// </summary>
/// <remarks>Use this class to indicate that an operation failed because the specified item does not
/// exist. The failure includes details about the missing item's name and, optionally, its identifier or additional
/// context. This type is typically used in error handling scenarios to provide clear feedback when an item lookup
/// fails.</remarks>
public class ItemNotFoundFailure : Failure
    {
        /// <summary>
        /// Initializes a new instance of the ItemNotFoundFailure class to represent a failure caused by a missing item.
        /// </summary>
        /// <param name="itemName">The name of the item that could not be found. Cannot be null or empty.</param>
        /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
        public ItemNotFoundFailure(string itemName, Exception? innerException = null)
            : base(shortCode: "ItemNotFound", extendedDescription: $"The requested item '{itemName}' was not found.", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ItemNotFoundFailure class to represent a failure caused by a missing item.
        /// </summary>
        /// <param name="itemName">The name of the item that was not found. Cannot be null or empty.</param>
        /// <param name="additionalInfo">Additional information that provides context about the missing item. Can be an empty string if no extra
        /// details are available.</param>
        /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
        public ItemNotFoundFailure(string itemName, string additionalInfo, Exception? innerException = null)
            : base(shortCode: "ItemNotFound", extendedDescription: $"The requested item '{itemName}' was not found. Additional info: {additionalInfo}", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ItemNotFoundFailure class to represent a failure caused by a missing item
        /// with the specified name and identifier.
        /// </summary>
        /// <param name="itemName">The name of the item that was not found. Cannot be null or empty.</param>
        /// <param name="id">The unique identifier of the item that was not found.</param>
        /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
        public ItemNotFoundFailure(string itemName, long id, Exception? innerException = null)
            : base(shortCode: "ItemNotFound", extendedDescription: $"The requested item '{itemName}' with ID '{id}' was not found.", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ItemNotFoundFailure class with details about the missing item and optional
        /// additional information.
        /// </summary>
        /// <param name="itemName">The name of the item that was not found. Cannot be null or empty.</param>
        /// <param name="id">The unique identifier of the item that was not found.</param>
        /// <param name="additionalInfo">Additional information that may help diagnose why the item was not found. Can be an empty string if no extra
        /// details are available.</param>
        /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
        public ItemNotFoundFailure(string itemName, long id, string additionalInfo, Exception? innerException = null)
            : base(shortCode: "ItemNotFound", extendedDescription: $"The requested item '{itemName}' with ID '{id}' was not found. Additional info: {additionalInfo}", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ItemNotFoundFailure class for a specific item and GUID.
        /// </summary>
        /// <param name="itemName">The name of the item that was not found.</param>
        /// <param name="guid">The unique identifier of the item that was not found.</param>
        /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
        public ItemNotFoundFailure(string itemName, Guid guid, Exception? innerException = null)
            : base(shortCode: "ItemNotFound", extendedDescription: $"The requested item '{itemName}' with GUID '{guid}' was not found.", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ItemNotFoundFailure class to represent a failure caused by a missing item
        /// identified by name and GUID.
        /// </summary>
        /// <param name="itemName">The name of the item that was not found. Cannot be null or empty.</param>
        /// <param name="guid">The unique identifier (GUID) of the item that was not found.</param>
        /// <param name="additionalInfo">Additional information that provides context about the failure. Can be an empty string if no extra details
        /// are available.</param>
        /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
        public ItemNotFoundFailure(string itemName, Guid guid, string additionalInfo, Exception? innerException = null)
            : base(shortCode: "ItemNotFound", extendedDescription: $"The requested item '{itemName}' with GUID '{guid}' was not found. Additional info: {additionalInfo}", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ItemNotFoundFailure class with the specified item name, identifier, and
        /// optional inner exception.
        /// </summary>
        /// <param name="itemName">The name of the item that could not be found. Cannot be null or empty.</param>
        /// <param name="id">The unique identifier of the item that was not found.</param>
        /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
        public ItemNotFoundFailure(string itemName, int id, Exception? innerException = null)
            : base(shortCode: "ItemNotFound", extendedDescription: $"The requested item '{itemName}' with ID '{id}' was not found.", innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the ItemNotFoundFailure class to represent a failure caused by a missing item
        /// with the specified name and ID.
        /// </summary>
        /// <param name="itemName">The name of the item that was not found. Cannot be null or empty.</param>
        /// <param name="id">The unique identifier of the item that was not found.</param>
        /// <param name="additionalInfo">Additional information that provides context about the failure. Can be an empty string if no extra details
        /// are available.</param>
        /// <param name="innerException">The exception that is the cause of this failure, or null if no inner exception is specified.</param>
        public ItemNotFoundFailure(string itemName, int id, string additionalInfo, Exception? innerException = null)
            : base(shortCode: "ItemNotFound", extendedDescription: $"The requested item '{itemName}' with ID '{id}' was not found. Additional info: {additionalInfo}", innerException)
        { }
    }


namespace Sisusa.Common.ReturnTypes
{
    /// <summary>
    /// A simple static factory for returning a "nothing" value of type FailureOrNothing. This can be used in methods that return FailureOrNothing to indicate a successful operation with no meaningful value to return.
    /// </summary>
    public static class NothingWrapper
    {
        /// <summary>
        /// Gets a value that represents the absence of a result or failure.
        /// </summary>
        /// <remarks>Use this property to indicate that an operation completed without producing a value
        /// or an error. This is typically used in scenarios where a method can succeed without returning meaningful
        /// data.</remarks>
        public static FailureOrNothing Nothing => FailureOrNothing.Nothing;
    }
}

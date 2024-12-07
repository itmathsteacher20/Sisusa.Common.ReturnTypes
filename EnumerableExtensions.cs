namespace Sisusa.Common.ReturnTypes
{
    public static class GlobalExtensions
    {
        /// <summary>
        /// Converts a value to an <see cref="Optional{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// An <see cref="Optional{T}"/> containing the value if it is not null or default; otherwise, an empty <see cref="Optional{T}"/>.
        /// </returns>
        public static Optional<T> ToOptional<T>(this T value)
        {
            if (ReferenceEquals(value, null))
                return Option.Empty<T>();

            return Equals(value, default(T)) ? Option.Empty<T>() :
                Option.Some<T>(value);
        }
    }
    
    /// <summary>
    /// Provides extension methods for <see cref="IEnumerable{T}"/> to work with Optional and FailureOr return types.
    /// </summary>
    public static class EnumerableExtensions
    {
        
        /// <summary>
        /// Returns the first element of a sequence as an <see cref="Optional{T}"/>, or an empty <see cref="Optional{T}"/> if the sequence is empty or the first element is null or default.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to retrieve the first element from.</param>
        /// <returns>
        /// An <see cref="Optional{T}"/> containing the first element if it exists and is not null or default; otherwise, an empty <see cref="Optional{T}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the source sequence is null.</exception>
        public static Optional<T> FirstOrNone<T>(this IEnumerable<T> source)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));

            var result = source.FirstOrDefault();

            if (ReferenceEquals(result, null))
                return Option.Empty<T>();

            return Equals(result, default(T)) ? Option.Empty<T>() : 
                result.ToOptional<T>();
        }

        /// <summary>
        /// Returns a single element from the sequence as a <see cref="FailureOr{T}"/>. If the sequence contains multiple elements, returns a failure result.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to retrieve a single element from.</param>
        /// <returns>
        /// A <see cref="FailureOr{T}"/> containing the single element if successful; otherwise, a failure result with an appropriate message.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the source sequence is null.</exception>
        public static FailureOr<T> FailureOrSingle<T>(this IEnumerable<T> source)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            try
            {
                var result = source.Single();

                return FailureOr<T>.Succeed(result);
            }
            catch (InvalidOperationException ex)
            {
                return FailureOr<T>.Fail(ex, "Source contains multiple elements- single element expected.");
            }
        }

        /// <summary>
        /// Determines whether the sequence is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to check for emptiness.</param>
        /// <returns><c>true</c> if the sequence is empty; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source sequence is null.</exception>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            return !source.Any();
        }
    }

    public static class DictionaryExtensions
    {
        public static Optional<TValue> GetValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
            var hasResult = dictionary.TryGetValue(key, out var result);
            
            return hasResult ? Option.Some<TValue>(result!) : Option.Empty<TValue>();
        }
    }
}

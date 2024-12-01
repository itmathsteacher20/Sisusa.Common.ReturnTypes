namespace Sisusa.Common.ReturnTypes
{
    /// <summary>
    /// Represents an optional value. An instance of <c>Optional</c> can either contain a non-null value or
    /// be empty, indicating the absence of a value.
    /// </summary>
    /// <typeparam name="T">The type of the value that might be contained within the <c>Optional</c>.</typeparam>
    public class Optional<T>
    {
        private readonly T? _value;

        private readonly bool _empty;

        //private readonly bool _isEmpty = true;

        private bool HasValue()
        {
            if (_empty) return false;
            
            return _value != null;
        }

        /// <summary>
        /// Returns the value if present; otherwise, returns the specified alternate value.
        /// </summary>
        /// <param name="otherValue">The alternate value to return if the current <c>Optional</c> instance is empty.</param>
        /// <returns>The value if present; otherwise, the specified alternate value.</returns>
        public T OrElse(T otherValue)
        {
            return HasValue() ?  _value! : otherValue;
        }

        /// <summary>
        /// Executes the specified action if the current <c>Optional</c> instance contains a value.
        /// </summary>
        /// <param name="doAction">The action to execute if a value is present.</param>
        public void IfHasValue(Action<T> doAction)
        {
            if (HasValue())
            {
                doAction(_value!);
            }
        }

        /// <summary>
        /// Returns the value if present; otherwise, computes the value using the given supplier function.
        /// </summary>
        /// <param name="doAction">A function that supplies the alternate value to return if the current <c>Optional</c> instance is empty.</param>
        /// <returns>The value if present; otherwise, the value returned by the supplier function.</returns>
        public T OrElseGet(Func<T> doAction)
        {
            return HasValue() ? _value! : doAction();
        }

        /// <summary>
        /// Returns the value if present; otherwise, throws the specified exception.
        /// </summary>
        /// <param name="exception">The exception to throw if the current <c>Optional</c> instance is empty.</param>
        /// <returns>The value stored in the current <c>Optional</c> instance if present.</returns>
        /// <exception cref="Exception">Thrown when the current <c>Optional</c> instance is empty.</exception>
        public T OrThrow(Exception exception)
        {
            if (!HasValue())
            {
                throw exception;
            }

            return _value!;
        }


        /// <summary>
        /// Applies the specified mapping function to the value if present, and returns an <c>Optional</c> containing the result.
        /// </summary>
        /// <param name="mapFunc">The function to apply to the value if present.</param>
        /// <returns>An <c>Optional</c> with the mapped value if it is present and non-null; otherwise, an empty <c>Optional</c>.</returns>
        public Optional<T> Map(Func<T> mapFunc)
        {
            if (!HasValue())
            {
                return this;
            }
            var returnValue = mapFunc();
            
            if (returnValue is null)
                return None;
            return Some(returnValue);
        }

        /// <summary>
        /// Transforms the current <c>Optional</c> instance to another form using the specified mapping function.
        /// If the current instance is empty, returns an empty <c>Optional</c>.
        /// </summary>
        /// <typeparam name="TU">The type of the value to be returned in the new <c>Optional</c>.</typeparam>
        /// <param name="mapFunc">A mapping function to apply to the value, if present.</param>
        /// <returns>A new <c>Optional</c> containing the mapped value if the current <c>Optional</c> has a value, otherwise an empty <c>Optional</c>.</returns>
        public Optional<TU> Map<TU>(Func<T, TU> mapFunc)
        {
            if (!HasValue())
            {
                return Optional<TU>.Empty();
            }
            var returnValue = mapFunc(_value!);
            
            if (returnValue is null)
                return Optional<TU>.None;
            
            return Optional<TU>.Some(returnValue);

        }

        /// <summary>
        /// Transforms the value of the current <c>Optional</c> using the provided mapping function
        /// if a value is present, and flattens the result into a single <c>Optional</c>.
        /// </summary>
        /// <param name="mapFunc">
        /// A mapping function to apply to the value, which returns an <c>Optional</c> of the new type.
        /// </param>
        /// <typeparam name="TU">The type of the value contained in the returned <c>Optional</c>.</typeparam>
        /// <returns>
        /// An <c>Optional</c> containing the result of applying the mapping function
        /// if a value is present; otherwise, an empty <c>Optional</c>.
        /// </returns>
        public Optional<TU> FlatMap<TU>(Func<T, Optional<TU>> mapFunc)
        {
            if (HasValue())
            {
                var returnValue = mapFunc(_value!);
                return returnValue;
            }
            return Optional<TU>.Empty();
        }

        /// <summary>
        /// Applies the specified function to the value if present, returning a new <c>Optional</c> of the result;
        /// otherwise, returns an empty <c>Optional</c>.
        /// </summary>
        /// <param name="doNext">A function to apply to the value if present.</param>
        /// <typeparam name="TU">The type of the result of the function.</typeparam>
        /// <returns>An <c>Optional</c> containing the result of applying the function, or an empty <c>Optional</c> if no value is present.</returns>
        public Optional<TU> Then<TU>(Func<T, Optional<TU>> doNext)
        {
            return !HasValue() ? Optional<TU>.None : doNext(_value!);
        }

        /// <summary>
        /// Executes the appropriate action based on whether the current <c>Optional</c> instance contains a value.
        /// </summary>
        /// <param name="some">The action to execute if a value is present.</param>
        /// <param name="none">The action to execute if no value is present.</param>
        public void Match(Action<T> some, Action none)
        {
            var isEmpty = !HasValue();
            //Console.WriteLine($"Empty state: {isEmpty}");
            if (isEmpty)
            {
                none();
            }
            else
            {
                some(_value!);
            }
        }

        /// <summary>
        /// Asynchronously executes the specified asynchronous function if the current <c>Optional</c> instance contains
        /// a value, or executes the provided asynchronous action if the instance is empty.
        /// </summary>
        /// <param name="some">The asynchronous function to execute if a value is present. The function takes the value as a parameter.</param>
        /// <param name="none">The asynchronous action to execute if the current <c>Optional</c> instance is empty.</param>
        /// <returns>A <c>Task</c> representing the asynchronous operation.</returns>
        public async Task MatchAsync(Func<T, Task<Action<T>>> some, Func<Task<Action>> none)
        {
            var isEmpty = !HasValue();
            //Console.WriteLine($"Empty state: {isEmpty}");
            if (isEmpty)
            {
                await none();
            }
            else
            {
                await some(_value!);
            }
        }


        /// <summary>
        /// Creates an <c>Optional</c> instance containing the specified value, if it is not null.
        /// </summary>
        /// <param name="valueToWrap">The value to wrap in an <c>Optional</c> instance. If the value is null, an empty <c>Optional</c> is returned.</param>
        /// <returns>An <c>Optional</c> containing the specified value if it is not null; otherwise, an empty <c>Optional</c>.</returns>
        public static Optional<T> Of(T? valueToWrap)
        {
            if (valueToWrap is null)
            {
                return new();
            }
            return new(valueToWrap);
        }

        /// <summary>
        /// Returns an empty <c>Optional</c> instance.
        /// </summary>
        /// <returns>An <c>Optional</c> instance representing no value.</returns>
        public static Optional<T> Empty()
        {
            return new();
        }

        /// <summary>
        /// Represents an empty <c>Optional</c> instance, indicating the absence of a value.
        /// </summary>
        public static Optional<T> None => Empty();

        /// <summary>
        /// Creates an <c>Optional</c> instance containing the specified non-null value.
        /// </summary>
        /// <param name="value">The non-null value to wrap in an <c>Optional</c>.</param>
        /// <returns>An <c>Optional</c> containing the specified value.</returns>
        public static Optional<T> Some(T value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            return Of(value);
        }


        protected Optional(T? valueToWrap)
        {
            if (valueToWrap != null)
            {
                _value = valueToWrap;
                _empty = false;
            }
        }

        protected Optional()
        {
            _value = default!;
            _empty = true;
        }
        
    }

    //public sealed class Some<T>(T value) : Optional<T>(value) where T : class;
}

namespace Sisusa.Common.ReturnTypes
{
    /// <summary>
    /// Represents a result that can either be a successful outcome with a value of type <typeparamref name="T"/> or an error state encapsulated by <see cref="FailureInfo"/>.
    /// </summary>
    /// <typeparam name="T">The type of the successful result value.</typeparam>
    public class FailureOr<T>
    {
        private readonly T _value;
        private readonly IFailure _errorInfo;

        /// <summary>
        /// Returns the encapsulated value if the current instance represents a success state;
        /// otherwise, returns the provided fallback value.
        /// </summary>
        /// <param name="fallbackValue">The value to return if the current instance
        /// represents an error state.</param>
        /// <exception cref="ArgumentNullException">If fallback value is null.
        /// The type is designed to reduce <see cref="NullReferenceException"/> errors so use of nulls is limited.
        /// </exception>
        /// <returns>The encapsulated value if successful; otherwise, the fallback value.</returns>
        public T GetOr(T fallbackValue)
        {
            ArgumentNullException.ThrowIfNull(fallbackValue, nameof(fallbackValue));
            return IsError ? fallbackValue : _value;
        }
        

        /// <summary>
        /// Processes the current instance based on its success or failure state,
        /// executing the appropriate function and returning a result.
        /// </summary>
        /// <param name="success">A function to execute and return a result if the current instance is successful.</param>
        /// <param name="failure">A function to execute and return a result if the current instance is an error.</param>
        /// <returns>The result from the executed function, depending on whether the instance is a success or an error.</returns>
        /// <typeparam name="TResult">The type of the result produced by the executed function.</typeparam>
        public TResult MatchReturn<TResult>(Func<T, TResult> success, Func<IFailure, TResult> failure)
        {
            return IsError ? failure(_errorInfo) : success(_value);
        }

        /// <summary>
        /// Asynchronously matches the current state of the instance by executing the appropriate function
        /// depending on whether the instance represents a success or an error state.
        /// </summary>
        /// <param name="success">A function to execute if the instance represents a success state.</param>
        /// <param name="failure">A function to execute if the instance represents an error state.</param>
        /// <typeparam name="TResult">The type of the result returned by the matching functions.</typeparam>
        /// <returns>A task that represents the asynchronous operation, containing the result of the executed function.</returns>
        public Task<TResult> MatchReturnAsync<TResult>(Func<T, Task<TResult>> success,
            Func<IFailure, Task<TResult>> failure)
        {
            return IsError ? failure(_errorInfo) : success(_value);
        }

        /// <summary>
        /// Executes the appropriate action based on whether the current instance is in a success or error state.
        /// Invokes the provided success action if the instance represents a successful state, otherwise,
        /// invokes the provided failure action with the failure information.
        /// </summary>
        /// <param name="success">The action to perform if the current instance represents a success state.</param>
        /// <param name="failure">The action to perform if the current instance represents an error state, receiving failure information.</param>
        public void Match(Action<T> success, Action<IFailure> failure)
        {
            if (IsError)
            {
                failure(_errorInfo);
            } else
            {
                success(_value);
            }
        }

        /// <summary>
        /// Asynchronously executes the appropriate action based on the current instance's state:
        /// success or failure.
        /// </summary>
        /// <param name="success">The function to execute when the instance represents a successful state.</param>
        /// <param name="failure">The function to execute when the instance represents a failure state,
        /// receiving an instance of <see cref="FailureInfo"/>.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task MatchAsync(Func<T, Task> success, Func<IFailure, Task> failure)
        {
            return IsError ? failure(_errorInfo) : success(_value);
        }

        /// <summary>
        /// Constructs and returns a new instance of <see cref="FailureOr{T}"/> representing a successful state.
        /// </summary>
        /// <param name="value">The value to be wrapped in a successful <see cref="FailureOr{T}"/> instance.</param>
        /// <returns>A new instance of <see cref="FailureOr{T}"/> containing the provided value, indicating a successful state.</returns>
        public static FailureOr<T> Succeed(T value)
        {
            return new(value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="FailureOr{T}"/> representing a failure state with the specified error message.
        /// </summary>
        /// <param name="message">The error message that describes the failure.</param>
        /// <returns>A <see cref="FailureOr{T}"/> instance representing a failure.</returns>
        public static FailureOr<T> Fail(string message)
        {
            return new(FailureInfo.WithMessage(message));
        }

        /// <summary>
        /// Creates an instance of <see cref="FailureOr{T}"/> representing a failure with the provided error information.
        /// </summary>
        /// <param name="errorInfo">The failure information containing details about the error.</param>
        /// <returns>An instance of <see cref="FailureOr{T}"/> initialized to a failure state with the specified error information.</returns>
        public static FailureOr<T> Fail(IFailure errorInfo)
        {
            return new FailureOr<T>(errorInfo);
        }

        /// <summary>
        /// Executes the provided function and encapsulates its result within a <see cref="FailureOr{T}"/> pass-through object.
        /// In case the function throws an exception, encapsulates the failure with the relevant information.
        /// </summary>
        /// <param name="action">The function to be executed, whose result is to be encapsulated.</param>
        /// <returns>A <see cref="FailureOr{T}"/> object containing either the successful result of the function
        /// or information about the exception if the function fails.</returns>
        /// <exception cref="NullReferenceException">Thrown if the function returns a null result.</exception>
        public static FailureOr<T> From(Func<T> action)
        {
            try
            {
                var result = action();
                return result == null
                    ? throw new NullReferenceException("Action returned null result.") :
                    Succeed(result);
            }
            catch (Exception error)
            {
                return Fail(error, $"{nameof(action)} threw an exception or returned null and was assumed to have failed.");
            }
        }

        /// <summary>
        /// Wraps the given exception into a <see cref="FailureOr{T}"/> object,
        /// using the exception's message as the failure message.
        /// </summary>
        /// <param name="error">The exception to be wrapped.</param>
        /// <param name="message">Message to describe the failure.</param>
        /// <returns>A new <see cref="FailureOr{T}"/> instance encapsulating the exception.</returns>
        public static FailureOr<T> Fail(Exception error, string message)
        {
            return new FailureOr<T>(FailureFactory.WithMessageAndException(message, error));
        }

        /// <summary>
        /// Wraps an exception into a FailureOr instance, using the exception's message as the error message.
        /// </summary>
        /// <param name="exception">The exception to be wrapped.</param>
        /// <returns>A FailureOr instance representing the wrapped exception.</returns>
        public static FailureOr<T> Fail(Exception exception)
        {
            return Fail(exception, $"Operation failed with exception: {exception.GetType().Name}.");
        }

        public static implicit operator FailureOr<T>(T value)
        {
            if (value is null)
            {
                return Fail("Cannot wrap null value. Use FailureOrNothing if you intend to use a type that is empty on success.");

            } else if (value is IFailure fVal)
            {
                return Fail(fVal);

            } else if (value is T tVal)
            {
                return Succeed(tVal);
            }
            return Fail(value.ToString()!);
        }

        public static implicit operator FailureOr<T>(Failure failure)
        {
            if (failure is null)
            {
                return Fail(new Failure("NULL", "Nothing to wrap as Error.", new NullReferenceException()));
            }
            return FailureOr<T>.Fail(failure);
        }

        public static implicit operator FailureOr<T>(FailureInfo failure)
        {
            return FailureOr<T>.Fail(failure);
        }

        public static implicit operator FailureOr<T>(Exception exception)
        {
            return FailureOr<T>.Fail(exception);
        }
        

        /// <summary>
        /// Indicates whether the current instance represents an error state.
        /// </summary>
        /// <remarks>
        /// This property returns true if the instance is in an error state,
        /// typically due to the presence of failure information. When true,
        /// the instance carries an error and does not contain a successful result.
        /// </remarks>
        protected bool IsError { get; }

        protected FailureOr(T value)
        {
            ArgumentNullException.ThrowIfNull(value, "Cannot wrap null value. Use FailureOrNothing if you intend to use a type that is empty on success.");
            _value = value;
            IsError = false;
            _errorInfo = default!;
        }

        protected FailureOr(IFailure errorInfo)
        {
            _errorInfo = errorInfo;
            IsError = true;
            _value = default!;
        }
    }
}

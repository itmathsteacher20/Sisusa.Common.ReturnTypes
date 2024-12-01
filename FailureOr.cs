namespace Sisusa.Common.ReturnTypes
{
    public class FailureOr<T>
    {
        private readonly T _value;
        private readonly FailureInfo _errorInfo;

        /// <summary>
        /// Returns the encapsulated value if the current instance represents a success state;
        /// otherwise, returns the provided fallback value.
        /// </summary>
        /// <param name="fallbackValue">The value to return if the current instance
        /// represents an error state.</param>
        /// <returns>The encapsulated value if successful; otherwise, the fallback value.</returns>
        public T GetOr(T fallbackValue)
        {
            return IsError ? fallbackValue : _value;
        }

        /// <summary>
        /// Processes the current instance by executing an appropriate action depending on its state.
        /// </summary>
        /// <param name="onSuccess">An action to invoke if the current instance represents a successful value.</param>
        /// <param name="onError">An action to invoke with the <see cref="FailureInfo"/> if the current instance represents an error.</param>
        public void Handle(Action<T> onSuccess, Action<FailureInfo> onError)
        {
            if (IsError)
            {
                onError(_errorInfo);
                return;
            }
            onSuccess(_value);
        }

        /// <summary>
        /// Processes the current instance based on its success or failure state,
        /// executing the appropriate function and returning a result.
        /// </summary>
        /// <param name="success">A function to execute and return a result if the current instance is successful.</param>
        /// <param name="failure">A function to execute and return a result if the current instance is an error.</param>
        /// <returns>The result from the executed function, depending on whether the instance is a success or an error.</returns>
        /// <typeparam name="TResult">The type of the result produced by the executed function.</typeparam>
        public TResult Match<TResult>(Func<T, TResult> success, Func<FailureInfo, TResult> failure)
        {
            if (IsError)
            {
                return failure(_errorInfo);
            }
            return success(_value);
            //return !IsError ? success(_value) : failure(_errorInfo);
        }

        /// <summary>
        /// Asynchronously matches the current state of the instance by executing the appropriate function
        /// depending on whether the instance represents a success or an error state.
        /// </summary>
        /// <param name="success">A function to execute if the instance represents a success state.</param>
        /// <param name="failure">A function to execute if the instance represents an error state.</param>
        /// <typeparam name="TResult">The type of the result returned by the matching functions.</typeparam>
        /// <returns>A task that represents the asynchronous operation, containing the result of the executed function.</returns>
        public Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> success,
            Func<FailureInfo, Task<TResult>> failure)
        {
            return IsError ? failure(_errorInfo) : success(_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        public void Match(Action<T> success, Action<FailureInfo> failure)
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
        /// Constructs and returns a new instance of <see cref="FailureOr{T}"/> representing a successful state.
        /// </summary>
        /// <param name="value">The value to be wrapped in a successful <see cref="FailureOr{T}"/> instance.</param>
        /// <returns>A new instance of <see cref="FailureOr{T}"/> containing the provided value, indicating a successful state.</returns>
        public static FailureOr<T> Success(T value)
        {
            return new(value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="FailureOr{T}"/> representing a failure state with the specified error message.
        /// </summary>
        /// <param name="message">The error message that describes the failure.</param>
        /// <returns>A <see cref="FailureOr{T}"/> instance representing a failure.</returns>
        public static FailureOr<T> Failure(string message)
        {
            return new(FailureInfo.WithMessage(message));
        }

        /// <summary>
        /// Creates an instance of <see cref="FailureOr{T}"/> representing a failure with the provided error information.
        /// </summary>
        /// <param name="errorInfo">The failure information containing details about the error.</param>
        /// <returns>An instance of <see cref="FailureOr{T}"/> initialized to a failure state with the specified error information.</returns>
        public static FailureOr<T> Failure(FailureInfo errorInfo)
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
                    Success(result);
            }
            catch (Exception error)
            {
                return Failure(
                    FailureInfo
                        .WithMessage($"{error.Message}")
                        .WithException(error)
                    );
            }
        }

        /// <summary>
        /// Wraps the given exception into a <see cref="FailureOr{T}"/> object,
        /// using the exception's message as the failure message.
        /// </summary>
        /// <param name="error">The exception to be wrapped.</param>
        /// <param name="message">Message to describe the failure.</param>
        /// <returns>A new <see cref="FailureOr{T}"/> instance encapsulating the exception.</returns>
        public static FailureOr<T> WrapException(Exception error, string message)
        {
            var errorInfo = new FailureInfo(message, error);
            return new FailureOr<T>(errorInfo);
        }

        /// <summary>
        /// Wraps an exception into a FailureOr instance, using the exception's message as the error message.
        /// </summary>
        /// <param name="exception">The exception to be wrapped.</param>
        /// <returns>A FailureOr instance representing the wrapped exception.</returns>
        public static FailureOr<T> WrapException(Exception exception)
        {
            return WrapException(exception, exception.Message);
        }


        /// <summary>
        /// Converts a <see cref="FailureInfo"/> instance into a <see cref="FailureOr{T}"/> object implicitly,
        /// encapsulating the failure state within the result.
        /// </summary>
        /// <param name="errorInfo">The failure information to be encapsulated within the result.</param>
        /// <returns>A <see cref="FailureOr{T}"/> object representing the failure state.</returns>
        public static implicit operator FailureOr<T>(FailureInfo errorInfo)
        {
            return new FailureOr<T>(errorInfo);
        }

        /// <summary>
        /// Implicitly converts an <see cref="Exception"/> to a <see cref="FailureOr{T}"/> instance,
        /// using the exception's message as the failure message.
        /// </summary>
        /// <param name="exception">The exception to be converted into a failure.</param>
        /// <returns>A new <see cref="FailureOr{T}"/> instance representing the failure.</returns>
        public static implicit operator FailureOr<T>(Exception exception)
        {
            return WrapException(exception, exception.Message);
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
            ArgumentNullException.ThrowIfNull(value, "Cannot wrap null value.");
            _value = value;
            IsError = false;
            _errorInfo = default!;
        }

        protected FailureOr(FailureInfo errorInfo)
        {
            _errorInfo = errorInfo;
            IsError = true;
            _value = default!;
        }
    }

    public static class FailureOrExtensions
    {
        /// <summary>
        /// Transforms the contained value using the provided function and returns the resulting state.
        /// Executes the function only if the current instance is successful; otherwise, returns the current error state.
        /// </summary>
        /// <param name="failureOr">The current instance of <see cref="FailureOr{T}"/> that represents a success or failure state.</param>
        /// <param name="doNext">A function to execute and transform the value if the current instance is successful. It returns a <see cref="FailureOr{TU}"/>.</param>
        /// <typeparam name="T">The type of the value contained in the current instance.</typeparam>
        /// <typeparam name="TU">The type of the value returned in the resulting instance after applying the <paramref name="doNext"/> function.</typeparam>
        /// <returns>A <see cref="FailureOr{TU}"/> representing the new state after applying the provided function if successful; otherwise, the current error state.</returns>
        public static FailureOr<TU> Then<T, TU>(this FailureOr<T> failureOr, Func<T, FailureOr<TU>> doNext)
        {
            return failureOr.Match(
                success: value => doNext(value),
                failure: failure => failure
            );
        }

        /// <summary>
        /// Asynchronously processes the encapsulated value using the provided function if the current instance
        /// represents a success state. If the instance represents a failure state, it returns a failed result with
        /// the current error information.
        /// </summary>
        /// <param name="failureOr">The instance of <c>FailureOr&lt;T&gt;</c> to evaluate for success or error state.</param>
        /// <param name="doNext">A function to execute if the current instance is successful, returning a task that yields
        /// a <c>FailureOr&lt;TU&gt;</c> result.</param>
        /// <typeparam name="T">The type of the value contained within the successful <c>FailureOr</c>.</typeparam>
        /// <typeparam name="TU">The type of the value to be contained within the resulting <c>FailureOr</c> after the transformation.</typeparam>
        /// <returns>A task representing the asynchronous operation, containing a new <c>FailureOr&lt;TU&gt;</c> which is either
        /// the result of the executed function or the current failure.</returns>
        public static Task<FailureOr<TU>> ThenAsync<T, TU>(this FailureOr<T> failureOr,
            Func<T, Task<FailureOr<TU>>> doNext)
        {
            return failureOr.MatchAsync<FailureOr<TU>>(
                success: async value => await doNext(value),
                failure: failure => Task.FromResult(FailureOr<TU>.Failure(failure))
            );
        }

        /// <summary>
        /// Applies a transformation function to the contained value if the instance is in a success state;
        /// otherwise, returns the current failure state unchanged.
        /// </summary>
        /// <param name="failureOr">The current instance of <see cref="FailureOr{T}"/> that represents a success or failure state.</param>
        /// <param name="mapFunc">The function used to transform the contained value.</param>
        /// <returns>A <see cref="FailureOr{T}"/> with the transformed value if successful, or the current failure state if not.</returns>
        public static FailureOr<T> Map<T>(this FailureOr<T> failureOr, Func<T, T> mapFunc)
        {
            return failureOr.Match(
                success: value => {
                    Console.WriteLine($"Value is {value}  mapFunc returned {mapFunc(value)}");
                    return FailureOr<T>.Success(mapFunc(value));
                },
                failure: failure => {
                    Console.WriteLine($"It's a fail {failure.Message}");
                    return failure;
                    }
            );
        }

        /// <summary>
        /// Transforms the contained value using the provided mapping function if the current instance represents a success state; otherwise, returns the current error state if it represents a failure.
        /// </summary>
        /// <param name="failureOr">The current instance.</param>
        /// <param name="mapFunc">A function that defines how to transform the contained value.</param>
        /// <returns>A <see cref="FailureOr{T}"/> containing the transformed value if successful; otherwise, the current error state.</returns>
        public static FailureOr<TU> Map<T, TU>(this FailureOr<T> failureOr, Func<T, TU> mapFunc)
        {
            return failureOr.Match(
                success: value => FailureOr<TU>.Success(mapFunc(value)),
                failure: failure => failure
            );
        }

        /// <summary>
        /// Asynchronously transforms the encapsulated value in a success state into a new value using
        /// the provided asynchronous mapping function, or retains the error state as is.
        /// </summary>
        /// <param name="failureOr">The instance of <see cref="FailureOr{T}"/> to be transformed.</param>
        /// <param name="mapFunc">The asynchronous function to apply to the encapsulated value if in a success state.</param>
        /// <typeparam name="T">The type of the encapsulated value in the original success state.</typeparam>
        /// <typeparam name="TU">The type of the encapsulated value in the resulting success state after transformation.</typeparam>
        /// <returns>A task representing the asynchronous operation, returning a new instance of <see cref="FailureOr{TU}"/>
        /// that represents either the transformed success or the original error state.</returns>
        public static Task<FailureOr<TU>> MapAsync<T, TU>(this FailureOr<T> failureOr, Func<T, Task<TU>> mapFunc)
        {
            return failureOr.MatchAsync<FailureOr<TU>>(
                success: async value => FailureOr<TU>.Success(await mapFunc(value)),
                failure: failure => Task.FromResult(FailureOr<TU>.Failure(failure))
            );
        }


        public static void Catch<T>(this FailureOr<T> failureOr, Action<FailureInfo> handler)
        {
            failureOr.Handle(value => { }, handler);
        }

    }
}

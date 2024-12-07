namespace Sisusa.Common.ReturnTypes;

/// <summary>
/// Provides extension methods for the <see cref="FailureOr{T}"/> type to facilitate
/// functional-style transformations and error handling chains.
/// </summary>
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
        return failureOr.MatchReturn<FailureOr<TU>>(
            success: doNext,
            failure: FailureOr<TU>.Fail);
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
        return failureOr.MatchReturnAsync<FailureOr<TU>>(
            success: async value => await doNext(value),
            failure: failure => Task.FromResult(FailureOr<TU>.Fail(failure))
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
        return failureOr.MatchReturn<FailureOr<T>>(
            success: value => FailureOr<T>.Succeed(mapFunc(value)),
            failure: failure => FailureOr<T>.Fail(failure));
    }

    /// <summary>
    /// Transforms the contained value using the provided mapping function if the current instance represents a success state; otherwise, returns the current error state if it represents a failure.
    /// </summary>
    /// <param name="failureOr">The current instance.</param>
    /// <param name="mapFunc">A function that defines how to transform the contained value.</param>
    /// <returns>A <see cref="FailureOr{T}"/> containing the transformed value if successful; otherwise, the current error state.</returns>
    public static FailureOr<TU> Map<T, TU>(this FailureOr<T> failureOr, Func<T, TU> mapFunc)
    {
        return failureOr.MatchReturn<FailureOr<TU>>(
            success: value => FailureOr<TU>.Succeed(mapFunc(value)),
            failure: failure => FailureOr<TU>.Fail(failure)
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
        return failureOr.MatchReturnAsync<FailureOr<TU>>(
            success: async value => FailureOr<TU>.Succeed(await mapFunc(value)),
            failure: failure => Task.FromResult(FailureOr<TU>.Fail(failure))
        );
    }


    /// <summary>
    /// Executes a specified action when the current <see cref="FailureOr{T}"/> instance represents an error state.
    /// </summary>
    /// <param name="failureOr">The <see cref="FailureOr{T}"/> instance to be checked for failure state.</param>
    /// <param name="handler">The action to execute if the instance represents a failure state.</param>
    /// <typeparam name="T">The type of the value encapsulated in the <see cref="FailureOr{T}"/>.</typeparam>
    public static void Catch<T>(this FailureOr<T> failureOr, Action<IFailure> handler)
    {
        failureOr.Match(value => { }, handler);
    }
}
namespace Sisusa.Common.ReturnTypes
{
    /// <summary>
    /// Represents a result type that can either signify success or a failure,
    /// encapsulating potential failure information.
    /// </summary>
    public class FailureOrNothing 
    {
        private bool IsError { get; set; }

        private IFailure Error { get; set; }

        private FailureOrNothing()
        {
            IsError = false;
            Error = FailureFactory.FromMessage("An error occurred and the request failed."); 
        }

        private FailureOrNothing(IFailure failureInfo)
        {
            IsError = true;
            Error = failureInfo;
        }

        /// <summary>
        /// Creates a new <see cref="FailureOrNothing"/> instance that indicates that the operation was successful.
        /// </summary>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies a success.</returns>
        public static FailureOrNothing Success => Succeed();
        
        /// <summary>
        /// Creates a new <see cref="FailureOrNothing"/> instance that signifies a failure, using a generic message
        /// as a description of the failure.
        /// </summary>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies failure.</returns>
        public static FailureOrNothing Failure => Fail("The request failed - no further information available.");
        

        /// <summary>
        /// Executes the provided action if the current instance does not signify an error,
        /// returning a new <see cref="FailureOrNothing"/> instance indicating success or failure.
        /// </summary>
        /// <param name="doAction">The action to be executed if there is no error.</param>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies success if the action executed without throwing an exception; otherwise, signifies a failure.</returns>
        public FailureOrNothing Then(Action doAction)
        {
            if (IsError)
            {
                return this;
            }

            try
            {
                doAction();
                return Succeed();
            }
            catch (Exception ex)
            {
                return Fail(FailureFactory.WithMessageAndException("Then operation failed.", ex));
            }
        }

        /// <summary>
        /// Executes an asynchronous action if the current instance does not signify an error.
        /// </summary>
        /// <param name="doAction">The asynchronous action to be executed if no error is present.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a new <see cref="FailureOrNothing"/> instance that signifies success or failure based on the action outcome.</returns>
        public Task<FailureOrNothing> ThenAsync(Func<Task> doAction)
        {
            if (IsError)
            {
                return Task.FromResult(this);
            }
            return doAction().ContinueWith(task => task.IsFaulted ?
                Fail(
                    FailureFactory.WithMessageAndException("Then threw an exception.",task.Exception!)) : 
                Succeed());
        }

        /// <summary>
        /// Executes the provided functions based on the result state of the current instance.
        /// </summary>
        /// <typeparam name="TResult">The return type of the functions to be executed.</typeparam>
        /// <param name="success">The function to execute if the result signifies success.</param>
        /// <param name="failure">The function to execute if the result signifies failure, with the failure information provided.</param>
        /// <returns>The result of the executed function, either the success function or the failure function.</returns>
        public TResult MatchReturn<TResult>(Func<TResult> success, Func<IFailure, TResult> failure)
        {
            return IsError ? failure(Error) : success();
        }

        /// <summary>
        /// Asynchronously matches the result of an operation and executes the corresponding function
        /// based on whether the operation signifies a success or a failure.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the matching functions.</typeparam>
        /// <param name="success">A function to execute if the operation signifies success, returning a <typeparamref name="TResult"/>.</param>
        /// <param name="failure">A function to execute if the operation signifies failure, returning a <typeparamref name="TResult"/> with failure information.</param>
        /// <returns>A task that represents the asynchronous operation, containing the result of type <typeparamref name="TResult"/>.</returns>
        public Task<TResult> MatchReturnAsync<TResult>(Func<Task<TResult>> success, Func<IFailure, Task<TResult>> failure)
        {
            return IsError ? failure(Error) : success();
        }

        /// <summary>
        /// Executes one of the provided actions based on the result encapsulated by the current instance.
        /// </summary>
        /// <param name="success">An action to execute if the result signifies success.</param>
        /// <param name="failure">An action to execute if the result signifies failure, taking <see cref="FailureInfo"/> as an argument.</param>
        public void Match(Action success, Action<IFailure> failure)
        {
            if (IsError)
                failure(Error);
            else
                success();
        }


        /// <summary>
        /// Executes a specified action if the current instance represents a failure,
        /// passing the failure information to the provided action.
        /// </summary>
        /// <param name="catchAction">The action to execute with the <see cref="IFailure"/> of the failure.</param>
        public void Catch(Action<IFailure> catchAction)
        {
            if (IsError)
            {
                catchAction(Error);
            }
        }
        

        /// <summary>
        /// Throws the underlying exception if the current instance represents an error.
        /// If no inner exception is present, throws a new exception with the error message.
        /// </summary>
        /// <exception cref="Exception">Thrown when the instance represents an error.</exception>
        public void ThrowAsException()
        {
            if (!IsError) return;
            
            Error.InnerException.Match(
                some: ex => throw ex!, 
                none: () => throw new Exception(Error.Message));
        }
        
        /// <summary>
        /// Creates a new <see cref="FailureOrNothing"/> instance that signifies a successful execution.
        /// </summary>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies a success.</returns>
        public static FailureOrNothing Succeed() => new();
        
        /// <summary>
        /// Creates a new <see cref="FailureOrNothing"/> instance that signifies a failure with the provided message.
        /// </summary>
        /// <param name="message">The message describing the failure details-why the operation failed.</param>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies a failure.</returns>
        public static FailureOrNothing Fail(string message) => new(FailureFactory.FromMessage(message));

        /// <summary>
        /// Creates a new <see cref="FailureOrNothing"/> instance that signifies a failure with the provided <see cref="IFailure"/> information.
        /// </summary>
        /// <param name="failure">The <see cref="IFailure"/> instance containing failure details.</param>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies a failure.</returns>
        public static FailureOrNothing Fail(IFailure failure) => new(failure);

        /// <summary>
        /// Creates a new <see cref="FailureOrNothing"/> instance that signifies a failure with the provided exception information.
        /// </summary>
        /// <param name="exception">The exception containing the error information.</param>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies a failure.</returns>
        public static FailureOrNothing Fail(Exception exception) => new(FailureFactory.FromException(exception));

        /// <summary>
        /// Creates a new <see cref="FailureOrNothing"/> instance that signifies a failure
        /// with the provided exception and message information.
        /// </summary>
        /// <param name="exception">The exception that caused the failure.</param>
        /// <param name="message">A message describing the context of the failure.</param>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies a failure.</returns>
        public static FailureOrNothing Fail(Exception exception, string message)
        {
            return new(FailureFactory.WithMessageAndException(message, exception));
        }
    }

}

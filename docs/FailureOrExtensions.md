## User Documentation: `FailureOrExtensions` Class

### Overview
The `FailureOrExtensions` class provides a set of extension methods designed to facilitate functional-style transformations and error handling for the `FailureOr<T>` type. These methods allow for seamless handling of success and failure states, enabling a more readable and maintainable approach to error propagation and value transformations.

The `FailureOr<T>` type represents a value that can either contain a success value or an error state. This class offers various methods that allow you to transform or handle these states in a chain-like manner.

### Methods

#### 1. `Then<T, TU>(this FailureOr<T> failureOr, Func<T, FailureOr<TU>> doNext)`
Transforms the contained value using a provided function and returns the resulting state. The function is executed only if the current instance is successful; otherwise, the error state is returned.

##### Parameters:
- `failureOr`: The current instance of `FailureOr<T>`, which represents either a success or failure state.
- `doNext`: A function that takes the current value (if successful) and returns a new `FailureOr<TU>`.

##### Returns:
A new `FailureOr<TU>`, which could be a success or failure, depending on the result of the transformation.

##### Usage Example:
```csharp
var result = FailureOr<int>.Succeed(5)
    .Then(value => FailureOr<int>.Succeed(value * 2)); // Result: Succeed(10)

var errorResult = FailureOr<int>.Fail(new SomeError("Invalid operation"))
    .Then(value => FailureOr<int>.Succeed(value * 2)); // Result: Fail("Invalid operation")
```

#### 2. `ThenAsync<T, TU>(this FailureOr<T> failureOr, Func<T, Task<FailureOr<TU>>> doNext)`
Asynchronously processes the encapsulated value using a provided function if the instance is in a success state. If the instance represents a failure state, it returns the current error information.

##### Parameters:
- `failureOr`: The current `FailureOr<T>` instance to evaluate for success or failure state.
- `doNext`: A function that returns a task containing a `FailureOr<TU>` value if the current state is a success.

##### Returns:
A `Task<FailureOr<TU>>` that represents the asynchronous operation, yielding either a success or failure.

##### Usage Example:
```csharp
var result = await FailureOr<int>.Succeed(5)
    .ThenAsync(async value => await Task.FromResult(FailureOr<int>.Succeed(value * 2))); // Result: Succeed(10)

var errorResult = await FailureOr<int>.Fail(new SomeError("Invalid operation"))
    .ThenAsync(async value => await Task.FromResult(FailureOr<int>.Succeed(value * 2))); // Result: Fail("Invalid operation")
```

#### 3. `Map<T>(this FailureOr<T> failureOr, Func<T, T> mapFunc)`
Applies a transformation function to the contained value if the current instance is in a success state. If the instance is in a failure state, the original error is returned unchanged.

##### Parameters:
- `failureOr`: The `FailureOr<T>` instance representing the success or failure state.
- `mapFunc`: A function that transforms the contained value (if successful).

##### Returns:
A new `FailureOr<T>`, either containing the transformed value (if successful) or the original error (if failed).

##### Usage Example:
```csharp
var result = FailureOr<int>.Succeed(5)
    .Map(value => value * 2); // Result: Succeed(10)

var errorResult = FailureOr<int>.Fail(new SomeError("Invalid operation"))
    .Map(value => value * 2); // Result: Fail("Invalid operation")
```

#### 4. `Map<T, TU>(this FailureOr<T> failureOr, Func<T, TU> mapFunc)`
Transforms the value using a mapping function, returning a `FailureOr<TU>`. If the current state is a failure, the error state is returned unchanged.

##### Parameters:
- `failureOr`: The current instance of `FailureOr<T>`.
- `mapFunc`: A function to transform the value contained within the `FailureOr<T>`.

##### Returns:
A `FailureOr<TU>` representing the transformed value if successful, or the original error state if failed.

##### Usage Example:
```csharp
var result = FailureOr<int>.Succeed(5)
    .Map(value => value.ToString()); // Result: Succeed("5")

var errorResult = FailureOr<int>.Fail(new SomeError("Invalid operation"))
    .Map(value => value.ToString()); // Result: Fail("Invalid operation")
```

#### 5. `MapAsync<T, TU>(this FailureOr<T> failureOr, Func<T, Task<TU>> mapFunc)`
Asynchronously transforms the value in a success state into a new value using the provided asynchronous function, or retains the error state.

##### Parameters:
- `failureOr`: The `FailureOr<T>` instance to be transformed.
- `mapFunc`: The asynchronous function to apply if in a success state.

##### Returns:
A `Task<FailureOr<TU>>` representing the asynchronous transformation of the value or the original failure.

##### Usage Example:
```csharp
var result = await FailureOr<int>.Succeed(5)
    .MapAsync(async value => await Task.FromResult(value * 2)); // Result: Succeed(10)

var errorResult = await FailureOr<int>.Fail(new SomeError("Invalid operation"))
    .MapAsync(async value => await Task.FromResult(value * 2)); // Result: Fail("Invalid operation")
```

#### 6. `Catch<T>(this FailureOr<T> failureOr, Action<IFailure> handler)`
Executes a specified action if the current `FailureOr<T>` instance represents an error state.

##### Parameters:
- `failureOr`: The `FailureOr<T>` instance to be checked for failure.
- `handler`: An action to execute if the instance is in a failure state.

##### Usage Example:
```csharp
FailureOr<int>.Fail(new SomeError("Something went wrong"))
    .Catch(failure => Console.WriteLine(failure.Message)); // Output: Something went wrong
```

### Real-World Use Case: Handling API Responses

Consider a scenario where we need to fetch data from an API. The data retrieval might succeed or fail based on various conditions (e.g., network issues, invalid API key, etc.). We can use `FailureOr<T>` to model these outcomes and process the results accordingly.

```csharp
public async Task<FailureOr<string>> FetchUserDataAsync(int userId)
{
    // Simulate API call
    if (userId <= 0)
        return FailureOr<string>.Fail(new ValidationError("Invalid user ID"));

    // Simulate success
    return FailureOr<string>.Succeed("User data");
}

public async Task ProcessUserData(int userId)
{
    var result = await FetchUserDataAsync(userId);

    var processedResult = await result
        .ThenAsync(async data =>
        {
            // Simulate processing
            return FailureOr<string>.Succeed(data.ToUpper());
        });

    processedResult.Catch(error =>
    {
        Console.WriteLine($"Error: {error.Message}");
    });

    Console.WriteLine(processedResult.MatchReturn(success => $"Processed data: {success}", failure => $"Error: {failure.Message}"));
}
```

### Conclusion

The `FailureOrExtensions` class provides powerful methods to simplify handling of both success and failure scenarios in a functional programming style. By utilizing these extension methods, you can handle success and failure more cleanly and concisely, leading to better error handling and code maintainability. Whether working synchronously or asynchronously, these methods allow for a robust and easy-to-read flow of operations that account for possible failures at every stage of computation.
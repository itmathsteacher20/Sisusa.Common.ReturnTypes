# FailureOr<T> Class Documentation

## Overview

The `FailureOr<T>` class provides a robust pattern for representing operations that can either succeed with a value of type `T` or fail with detailed error information. It combines the benefits of a **Result** type, **Error handling**, and **Asynchronous programming** into a single unified class, ensuring that your code handles success and failure scenarios in a structured and predictable way.

### Key Features

- **Success or Failure Representation**: Handles both success and error states explicitly, removing the need for null checks or exceptions when handling results.
- **Detailed Error Information**: Stores detailed failure information via the `FailureInfo` class, allowing for rich context and traceability of issues.
- **Fluent API**: Supports intuitive methods for processing the result, matching on success or failure, and providing fallback values or actions.
- **Asynchronous Support**: Full support for async programming, making it easy to handle operations that involve IO-bound tasks or asynchronous processing.
- **Functional Programming Style**: Functions like `MatchReturn` and `Match` enable you to deal with results in a functional programming style, enhancing clarity and reducing error-prone imperative code.

## Use Cases

### 1. **Handling Success and Failure Together**

In traditional exception-based handling, failures often disrupt the normal flow of execution. With `FailureOr<T>`, you can easily represent both success and failure in a clean, type-safe manner.

**Example:**

```csharp
var result = FailureOr<int>.From(() => SomeOperation());

result.Match(
    success: value => Console.WriteLine($"Success! The value is {value}"),
    failure: error => Console.WriteLine($"Failure: {error.Message}")
);
```

In this example, `SomeOperation` may return a successful result or throw an exception. The `FailureOr<T>` class ensures that we can handle both scenarios without throwing unexpected exceptions.

### 2. **Providing Fallback Values**

Instead of dealing with null values or checking for success manually, you can specify a fallback value to use when the operation fails.

**Example:**

```csharp
var result = FailureOr<string>.From(() => FetchDataFromDatabase());

string data = result.GetOr("Default Data");

Console.WriteLine(data);  // Will print "Default Data" if the operation failed.
```

If `FetchDataFromDatabase()` fails, the `GetOr` method ensures that `"Default Data"` is used as a fallback instead of dealing with `null`.

### 3. **Error Handling with Context**

For complex operations, it is important to track why a failure occurred. The `FailureOr<T>` class allows you to encapsulate rich error information using the `FailureInfo` class, making it easier to trace the issue.

**Example:**

```csharp
var result = FailureOr<int>.Fail("Database connection failed");

result.Match(
    success: value => Console.WriteLine($"Value: {value}"),
    failure: error => Console.WriteLine($"Error: {error.Message}, Code: {error.ErrorCode}")
);
```

In this case, you can create detailed error messages that can be logged, displayed to users, or used for debugging.

### 4. **Asynchronous Result Handling**

Asynchronous operations are commonplace in modern applications. `FailureOr<T>` integrates seamlessly with async programming, enabling you to match the result of asynchronous operations cleanly.

**Example:**

```csharp
public async Task ProcessDataAsync()
{
    var result = await FailureOr<int>.FromAsync(async () => await GetDataFromApi());

    await result.MatchAsync(
        success: value => ProcessApiData(value),
        failure: error => HandleApiError(error)
    );
}
```

Here, the result of the asynchronous operation `GetDataFromApi` is handled either as a success (to process the data) or a failure (to handle errors) in a non-blocking, asynchronous manner.

### 5. **Failing Gracefully with Exception Handling**

You may need to wrap exceptions or operations that could fail within your application. `FailureOr<T>` allows you to encapsulate exceptions and their messages, providing additional context about the error.

**Example:**

```csharp
public FailureOr<int> DivideNumbers(int numerator, int denominator)
{
    return FailureOr<int>.From(() => numerator / denominator);
}

var result = DivideNumbers(10, 0);

result.Match(
    success: value => Console.WriteLine($"Result: {value}"),
    failure: error => Console.WriteLine($"Error: {error.Message}")
);
```

This example safely handles division by zero by encapsulating the error in a `FailureOr<int>`, allowing your application to fail gracefully with the appropriate error message.

---

## API Reference

### Constructor

- **`FailureOr(T value)`**  
  Constructs a `FailureOr<T>` instance for a successful result, encapsulating the provided value.

- **`FailureOr(IFailure errorInfo)`**  
  Constructs a `FailureOr<T>` instance for an error, encapsulating the provided `IFailure` information.

### Static Methods

- **`Succeed(T value)`**  
  Returns a successful `FailureOr<T>` instance encapsulating the provided value.

- **`Fail(string message)`**  
  Returns a `FailureOr<T>` instance representing a failure with the provided error message.

- **`Fail(IFailure errorInfo)`**  
  Returns a `FailureOr<T>` instance representing a failure with the provided error information.

- **`From(Func<T> action)`**  
  Executes the provided function and encapsulates its result, returning a successful `FailureOr<T>` if the function succeeds, or a failure instance if an exception is thrown.

- **`Fail(Exception error, string message)`**  
  Wraps an exception into a failure, using the exception's message for context.

- **`Fail(Exception exception)`**  
  Wraps an exception into a failure with a default error message.

### Methods

- **`Match(Action<T> success, Action<IFailure> failure)`**  
  Executes the provided actions depending on whether the result is a success or failure.

- **`MatchReturn<TResult>(Func<T, TResult> success, Func<IFailure, TResult> failure)`**  
  Matches the result and returns a value depending on whether the result is a success or failure.

- **`MatchAsync(Func<T, Task> success, Func<IFailure, Task> failure)`**  
  Asynchronously executes the appropriate action depending on the state (success or failure).

- **`MatchReturnAsync<TResult>(Func<T, Task<TResult>> success, Func<IFailure, Task<TResult>> failure)`**  
  Asynchronously matches the result and returns a value depending on the result state.

- **`GetOr(T fallbackValue)`**  
  Returns the encapsulated value if successful, or the provided fallback value if in an error state.

### Properties

- **`IsError`**  
  A boolean indicating whether the current instance represents a failure (error) state.

## Conclusion

The `FailureOr<T>` class is a powerful tool for managing success and failure in a type-safe and predictable way. It eliminates the need for checking `null`, handling exceptions directly, and offers an elegant solution for functional programming patterns and asynchronous operations. Whether you're working with APIs, database operations, or any process that can succeed or fail, `FailureOr<T>` is the ideal choice for simplifying your error handling strategy and making your code more robust, readable, and maintainable.
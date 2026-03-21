# Sisusa.Common.ReturnTypes - README

This library is a personal implementation of the Result pattern in C#, collecting a few favorite ideas from various sources. For more details about the Result pattern, see this excellent [article](https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern).

The Sisusa.Common.ReturnTypes namespace provides utility extension methods to enhance the usability of types like Optional<T> and FailureOr<T>. These methods allow seamless handling of nullable values, default values, and error-prone operations like retrieving single elements from collections. They promote cleaner and more readable code while ensuring robust error handling.
## What is the Result Pattern?

The Result pattern provides a structured approach to handling errors and results explicitly, avoiding exceptions for normal flow control. It enables representing either success or failure in a method or operation using a `Result` type containing either a success value or error details.

### Why Use the Result Pattern?

1. **Explicit Error Handling**: The pattern promotes handling errors explicitly, making the code more predictable, testable, and debuggable.
2. **Improved Readability**: By returning a `Result` type, methods clearly convey whether they succeeded or failed and why.
3. **Streamlined Chaining**: It simplifies composing and chaining operations while preserving error context.

## Features of `Sisusa.Common.ReturnTypes`

Rather than adhering to the typical `Result` or `Either<L, R>` types found in most similar libraries, `Sisusa.Common.ReturnTypes` introduces two primary result types tailored for distinct use cases:

### 1. `FailureOrNothing`
This type is used for operations that do not return a value. It encapsulates success or failure explicitly, replacing the need to throw exceptions for errors. For example:

```csharp
FailureOrNothing SaveData(DataModel data)
{
    if (data == null)
        return FailureOrNothing.Fail(new Failure("NULL_DATA", "Data cannot be null"));

    // Save data logic here...
    return FailureOrNothing.Succeed();
}
```

### 2. `FailureOr<T>`
This type is designed for operations that return a value. It encapsulates either the result value or failure details:

```csharp
FailureOr<User> GetUserById(int userId)
{
    if (userId <= 0)
        return FailureOr<User>.Fail(new InvalidInputFailure("User ID must be greater than zero"));

    var user = database.GetUser(userId);
    if (user == null)
        return FailureOr<User>.Fail(new ItemNotFoundFailure("User", userId));

    return FailureOr<User>.Succeed(user);
    //can also be written as return user; since implicit conversion is supported
}
```

## Why Two Result Types?
By distinguishing between operations that return a value (`FailureOr<T>`) and those that do not (`FailureOrNothing`), the library ensures clarity and prevents misuse. This design aligns with the principle of explicit programming, reducing ambiguity in method signatures and promoting better error handling practices.


For more detailed documentation please read [FailureOr<T>](docs/FailureOr.md), [FailureOrNothing](docs/FailureOrNothing.md) and [Extension methods](docs/FailureOrExtensions.md)


## A note on usage
You can choose to use either `FailureOr<T>` or `FailureOrNothing` based on the context of your operation. For operations that do not return a value, `FailureOrNothing` provides a clear and concise way to represent success or failure without the overhead of handling a generic type. For operations that return a value, `FailureOr<T>` allows you to encapsulate both the result and any potential failure information in a structured manner.
For example, if you have a method that performs an action without returning a result, such as saving data or sending an email, `FailureOrNothing` would be the appropriate choice. On the other hand, if you have a method that retrieves data or performs a calculation and returns a result, `FailureOr<T>` would be more suitable.

Usage Flow is designed to be intuitive and straightforward, allowing you to easily check for success or failure and handle the results accordingly.
You can use pattern matching, extension methods, or direct property access to work with the results of your operations.

```csharp
var result = GetUserById(123);

// Using pattern matching
result.Match(
    success: user => Console.WriteLine($"User found: {user.Name}"),
    failure: failureInfo => Console.WriteLine($"Failed to get user: {failureInfo.Message}")
);

// Using extension methods
return result.MatchReturn(
    success: user => user.Name,
    failure: failureInfo => "Unknown User"
);

// Direct property access
if (result.IsFailure)
{
    Console.WriteLine($"Failed to get user: {result.FailureInfo.Message}");
}
else
{
    Console.WriteLine($"User found: {result.Value.Name}");
}
```
The same pattern applies to `FailureOrNothing`, where you can check for success or failure and handle the results accordingly.

---
# Other Types in the Library
## **1** `Optional<T>`

The `Optional<T>` class represents a value that may or may not be present, providing a safer alternative to null values. It enables functional-style handling of optional values with methods for mapping, transforming, and handling presence/absence of values.

#### Key Features:
1. **Creation**:
    - `Optional<T>.Some(value)`: Wraps a non-null value.
    - `Optional<T>.Empty()`: Creates an empty optional.

2. **Value Retrieval**:
    - `OrElse(T other)`: Returns the value or an alternative if absent.
    - `OrElseGet(Func<T> supplier)`: Computes and returns a value if absent.
    - `OrThrow(Exception ex)`: Throws an exception if no value is present.

3. **Transformations**:
    - `Map(Func<T, TU>)`: Applies a function to the value if present, returning a new `Optional<TU>`.
    - `FlatMap(Func<T, Optional<TU>> mapFunc)`: Chains transformations by returning another `Optional`.

4. **Conditionally Perform Actions**:
    - `IfHasValue(Action<T>)`: Executes an action if the value exists.
    - `Match(Action<T> some, Action none)`: Executes different actions based on the presence of a value.

5. **Advanced Features**:
    - Supports asynchronous mapping (`MapAsync`) and matching (`MatchAsync`).
    - Supports equality comparison and custom `ToString()` formatting.

#### Usage Example:
```csharp
var optionalValue = Optional<int>.Some(42);

optionalValue
    .Map(value => value * 2)
    .Match(
        some: v => Console.WriteLine($"Value: {v}"),
        none: () => Console.WriteLine("No value"));
```
The initial value of `42` is transformed to `84` using `Map`, and the presence of a value is handled with `Match`. If the optional were empty, it would execute the `none` action instead.

As with the `FailureOr` types, `Optional<T>` allows either chaining of operations or direct handling of presence/absence, making it a versatile tool for managing optional values in a clear and functional style.

```csharp

var mightBeUser = GetUserById(123);
if (mightBeUser.IsNone)
{
    Console.WriteLine("User not found.");
}
else
{
    var user = mightBeUser.Value;
    Console.WriteLine($"User found: {user.Name}");
}
```

## Usage Note:
While `Optional<T>` provides a powerful way to handle optional values, it is important to use it judiciously. 
Overusing `Optional<T>` can lead to unnecessary complexity and performance overhead. It is best suited for scenarios where the presence or absence of a value is a common occurrence and needs to be handled explicitly. For cases where nullability is sufficient, using nullable types or standard null checks may be more appropriate.


For more detailed information, refer to the [extended documentation](docs/Optional.md).

---

## **2** `IFailure`,`Failure`, `FailureInfo`, `FailureFactory`

- ### **`IFailure`**  
  Defines the contract for failure information, encapsulating a failure message and an optional underlying exception.
   - **`Message`**: A descriptive message for the failure.
   - **`InnerException`**: An optional `Exception` providing additional details about the failure.


- ### **`FailureInfo`**  
  Represents detailed information about a failure, including a message, an optional underlying exception, and support for equality checks and factory methods.
   - **Constructor**: Accepts a `message` and optional `innerException`.
   - **Properties**:
      - `Message`: Descriptive message for the failure.
      - `InnerException`: Optional `Exception` wrapped in an `Optional<>`.
   - **Methods**:
      - `FromException`: Creates an instance using an exception and a message.
      - `WithMessage`: Creates an instance with a specific message.
      - `WithException`: Adds an exception to the instance.
   - **Implicit Operators**: Converts between `FailureInfo` and `Failure`.

- ### **`Failure`**  
  Represents a failure with a short code, description, and optional exception.
   - **Constructor**: Accepts `shortCode`, `extendedDescription`, and optional `innerException`.
   - **Properties**:
      - `Code`: Short identifier for the failure.
      - `Description`: Extended failure description.
      - `Message`: Combines the code and description into a single message.
      - `InnerException`: Optional `Exception` wrapped in an `Optional<>`.
   - **Methods**:
      - Equality and hash methods for comparison.
   - **Implicit Operators**: Converts between `Failure` and `FailureInfo`.

- ### **`FailureFactory`**  
  Provides factory methods to create instances of `IFailure`.
   - **Methods**:
      - `WithMessage`: Creates a `FailureInfo` with a specific message.
      - `WithCodeAndMessage`: Creates a `Failure` with a short code and message.
      - `WithCodeMessageAndException`: Creates a `Failure` with a code, description, and exception.
      - `WithMessageAndException`: Creates a `FailureInfo` with a message and exception.
      - `FromMessage`: Creates a `FailureInfo` using a message.
      - `FromException`: Creates a `FailureInfo` from an exception.
      - `FromCodeAndDescription`: Creates a `Failure` using a code and description.

### **Key Features**:
- Encapsulation of failure details through well-structured classes.
- Strong typing with constructors, properties, and optional `Exception` handling.
- Factory methods for flexible creation of failure information.
- Support for equality and implicit type conversions between `FailureInfo` and `Failure`.

For extended user documentation, please see [extended docs](docs/Failure-Types.md)

## **Global Extensions**
Extension methods are provided to make it easier to seamlessly convert normal/standard values to instances of `Optional<T>` and also, to add `Optional<T>` or `FailureOr<T>` capabilities to standard collections.
All these are designed to ensure clear, maintainable and more robust code that is able to handle errors gracefully.

Read more about these extension methods [here](docs/Extensions.md)

## Failure Types
Predefined failure types are provided in the `Sisusa.Common.ReturnTypes` library to represent common failure scenarios in a consistent manner. These types include:
  - `ItemNotFoundFailure`: Represents a failure when a requested item cannot be found.
  - `InvalidInputFailure`: Represents a failure due to invalid input.
  - `AuthenticationFailure`: Represents a failure related to authentication issues.
  - `PermissionDeniedFailure`: Represents a failure when the caller lacks necessary permissions.
  - `DatabaseConnectionFailure`: Represents a failure when the application cannot connect to the database.
  - `ExternalServiceFailure`: Represents a failure when an external service is unavailable or returns an error.
  - `InvalidInputFailure`: Represents a failure when data fails validation checks.
  - `ConflictFailure`: Represents a failure due to a conflict, such as a duplicate record or version mismatch.
 
These predefined failure types provide a standardized way to represent common error scenarios, making it easier for developers to handle and respond to failures in a consistent manner across the application. Each failure type includes relevant information such as error codes, messages, and optional details to aid in debugging and error handling.

`return FailureOrNothing.Fail(new ItemNotFoundFailure("User", 123)); //error msg generated automatically` // Example of using a predefined failure type 

For more information about these failure types, please refer to the [Failure Types Reference](docs/failures.md).

This library offers a clean and flexible approach to handling results and failures in C#. Feedback and contributions are welcome!
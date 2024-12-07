# **Introduction**
The `Sisusa.Common.ReturnTypes` namespace provides abstractions and implementations for failure information encapsulation. It includes a factory class (`FailureFactory`) for creating instances of `Failure` and `FailureInfo` to represent and handle application failures consistently.

---

## **Key Components**

### 1. **`IFailure` Interface**
- A contract for encapsulating failure details.
- **Properties:**
    - `string Message` – A human-readable message describing the failure.
    - `Optional<Exception?> InnerException` – An optional exception associated with the failure.

### 2. **`Failure` Class**
- Represents a failure with a code, description, and optional exception.
- Implements `IFailure`.
- **Constructor Parameters:**
    - `string shortCode` – A short, unique identifier for the failure.
    - `string extendedDescription` – A detailed failure description.
    - `Exception? innerException` – The exception associated with the failure.

### 3. **`FailureInfo` Class**
- Represents failure information with a focus on the failure message and an optional exception.
- Implements `IFailure`.
- Includes static factory methods for ease of creation.

### 4. **`FailureFactory` Class**
- A static factory providing methods to create `IFailure` instances.
- **Key Methods:**
    - `WithMessage(string message)`
    - `WithCodeAndMessage(string code, string message)`
    - `WithCodeMessageAndException(string shortCode, string extendedDescription, Exception innerException)`
    - `FromException(Exception exception)`
    - `FromCodeAndDescription(string shortCode, string extendedDescription)`

---

## **Usage Examples**

**Example 1: Creating a Simple FailureInfo**
```csharp
var failure = FailureFactory.WithMessage("An unexpected error occurred.");
Console.WriteLine(failure.Message); // Output: An unexpected error occurred.
```

**Example 2: Creating a Failure with Code and Message**
```csharp
var failure = FailureFactory.WithCodeAndMessage("ERR001", "Invalid user input.");
Console.WriteLine(failure.Message); // Output: ERR001: Invalid user input.
```

**Example 3: Creating a Failure with an Exception**
```csharp
var exception = new InvalidOperationException("Operation is not valid.");
var failure = FailureFactory.WithCodeMessageAndException("ERR002", "Operation failed", exception);
Console.WriteLine(failure.Message); // Output: ERR002: Operation failed
Console.WriteLine(failure.InnerException.OrElse(null)?.Message); // Output: Operation is not valid.
```

**Example 4: Creating FailureInfo from an Exception**
```csharp
var exception = new Exception("File not found.");
var failureInfo = FailureFactory.FromException(exception);
Console.WriteLine(failureInfo.Message); // Output: File not found.
```

---

## **Use Cases**

1. **Error Logging**
    - Use `Failure` or `FailureInfo` to encapsulate error details before logging them for diagnostics.

2. **API Responses**
    - Return consistent error structures to clients in REST APIs.

3. **User Feedback**
    - Display descriptive error messages in applications using `Message` from `IFailure`.

4. **Error Wrapping**
    - Use `InnerException` to trace and represent underlying exceptions for debugging.

---

#### **Extensibility**
You can extend the system by creating custom implementations of `IFailure` for specific needs, such as localization or custom error formatting.
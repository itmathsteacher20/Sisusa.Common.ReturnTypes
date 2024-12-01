# **README Sisusa.Common.ReturnTypes**

## **Overview**

The Sisusa Common library provides a set of utility classes for handling errors and failures in a more functional and robust manner. These classes offer a cleaner and safer approach to error handling compared to traditional exception-based methods.

## **Key Components**

1. **`FailureInfo`**
    * Encapsulates information about a failure, including a message, an underlying exception, and an error code.
    * Provides static methods to create `FailureInfo` instances from exceptions or messages.
    * Allows chaining methods to add more details to the failure information.

2. **`FailureOr<T>`**
    * Represents a value that might be either a successful result of type `T` or a failure.
    * Offers functional methods for handling success and failure cases:
        * `GetOr`: Returns the value if successful, otherwise a default value.
        * `Handle`: Executes actions based on success or failure.
        * `Match`: Selects and executes a function based on success or failure.
    * Provides static methods to create `FailureOr<T>` instances from values or failures.
    * Supports chaining operations using `Then` and `Map` methods.

3. **`FailureOrNothing`**
    * Represents a result that can either be successful or a failure without an associated value.
    * Provides methods for handling success and failure cases, similar to `FailureOr<T>`.
    * Offers a `ThrowAsException` method to re-throw the underlying exception if present.

## **Usage Examples**

**Example 1: Using `FailureOr<T>`**

```csharp
var result = FailureOr<int>.From(() => {
    // Some operation that might fail
    if (someCondition) {
        throw new Exception("Operation failed");
    }
    return 42;
});

result.Match(
    success: value => Console.WriteLine($"Success: {value}"),
    failure: error => Console.WriteLine($"Error: {error.Message}")
);
```

**Traditional Approach vs. `FailureOr<T>`**

* **Traditional:**
  ```csharp
  try {
      int result = SomeOperation();
      Console.WriteLine($"Success: {result}");
  } catch (Exception ex) {
      Console.WriteLine($"Error: {ex.Message}");
  }
  ```

* **`FailureOr<T>`:**
  The `FailureOr<T>` approach provides a more concise and functional way to handle errors, avoiding the need for explicit `try-catch` blocks. It also allows for easier chaining of operations and more elegant error handling.

**Example 2: Using `FailureOrNothing`**

```csharp
var result = FailureOrNothing.From(() => {
    // Some operation that might throw an exception
    DoSomething();
});

result.Match(
    success: () => Console.WriteLine("Success"),
    failure: error => Console.WriteLine($"Error: {error.Message}")
);
```

**Traditional Approach vs. `FailureOrNothing`**

* **Traditional:**
  ```csharp
  try {
      DoSomething();
      Console.WriteLine("Success");
  } catch (Exception ex) {
      Console.WriteLine($"Error: {ex.Message}");
  }
  ```

* **`FailureOrNothing`:**
  Similar to `FailureOr<T>`, `FailureOrNothing` offers a cleaner and more functional approach to handling operations that might fail. It avoids the need for explicit `try-catch` blocks and provides a more concise syntax.

## Extended Documentation for Sisusa Common Library

### `FailureOr<T>` in Depth

**Key Features and Usage:**

* **`GetOr`**:
    - Retrieves the value if successful, otherwise returns a default value.
  ```csharp
  int result = failureOr.GetOr(0);
  ```

* **`Handle`**:
    - Executes actions based on success or failure.
  ```csharp
  failureOr.Handle(
      success: value => Console.WriteLine($"Success: {value}"),
      failure: error => Console.WriteLine($"Error: {error.Message}")
  );
  ```

* **`Match`**:
    - Selects and executes a function based on success or failure.
  ```csharp
  string result = failureOr.Match(
      success: value => $"Success: {value}",
      failure: error => $"Error: {error.Message}"
  );
  ```

* **`Then`**:
    - Chains operations, transforming the value if successful.
  ```csharp
  var result = failureOr.Then(value => 
      FailureOr<string>.Success($"Processed: {value}")
  );
  ```

* **`Map`**:
    - Transforms the value if successful.
  ```csharp
  var result = failureOr.Map(value => value * 2);
  ```

### `FailureOrNothing` in Depth

**Key Features and Usage:**

* **`Then`**:
    - Executes an action if successful.
  ```csharp
  failureOr.Then(() => Console.WriteLine("Success"));
  ```

* **`Match`**:
    - Selects and executes a function based on success or failure.
  ```csharp
  string result = failureOr.Match(
      success: () => "Success",
      failure: error => $"Error: {error.Message}"
  );
  ```

* **`Catch`**:
    - Executes an action if the result is a failure.
  ```csharp
  failureOr.Catch(error => Console.WriteLine($"Error: {error.Message}"));
  ```

### Additional Considerations

* **Error Handling and Recovery:**
    - Use `FailureOr` to handle errors gracefully and provide meaningful feedback to the user.
    - Consider using `FailureOrNothing` for operations that don't return a specific value.
    - Employ `Then` and `Map` for sequential operations and transformations.
    - Utilize `Match` to handle both success and failure cases concisely.

* **Best Practices:**
    - Prioritize functional approaches for cleaner and more concise error handling.
    - Avoid excessive nesting and use chaining techniques to improve readability.
    - Consider custom exception types for specific error scenarios.
    - Test your error handling thoroughly to ensure robustness.

By effectively utilizing these features and following best practices, you can significantly enhance the reliability and maintainability of your applications.

**Best Practices**

* Use `FailureOr<T>` and `FailureOrNothing` to handle potential failures in your code.
* Prefer functional approaches to error handling.
* Use chaining methods to combine operations and error handling.
* Consider using custom exception types to provide more specific error information.

By following these guidelines and leveraging the features of the Sisusa Common library, you can write more robust and maintainable code.

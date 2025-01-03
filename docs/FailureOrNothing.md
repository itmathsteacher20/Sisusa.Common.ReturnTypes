# **`FailureOrNothing` Class User Documentation**

## **Overview**
The `FailureOrNothing` class provides a versatile way to handle the results of operations that can either succeed or fail, encapsulating error information if a failure occurs. This class is especially useful in scenarios where explicit success/failure handling is required without returning ambiguous nulls or exceptions, leading to cleaner and more robust code.

---

## **Key Features**
- **Explicit Success and Failure Handling:** Use static methods to create success or failure instances.
- **Encapsulation of Failure Information:** Encapsulates detailed failure information using an `IFailure` instance.
- **Fluent Chaining with `Then` Methods:** Chain actions or asynchronous tasks based on the result state.
- **Pattern Matching with `Match` and `MatchReturn`:** Execute different actions or return values based on success or failure.
- **Seamless Error Handling:** Catch and handle failures gracefully or throw exceptions when necessary.
- **Customizable Failure Messages:** Provides flexibility to include custom error messages or exceptions.

---

## **Usage Examples**

### **1. Simple Success and Failure Handling**
```csharp
//success
var successResult = FailureOrNothing.Success; //obsoleted

var newSuccessResult = FailureOrNothing.Nothing; //more expressive and in line with the name

var methodSuccessResult = FailureOrNothing.Succeed(); //if prefer explicit method calls

//failure
var failureResult = FailureOrNothing.Fail("Unable to process the request.");

var failureWithException = FailureOrNothing.Fail(new InvalidOperationException("Invalid operation."));

// Check and act on result
successResult.Match(
    success: () => Console.WriteLine("Operation succeeded!"),
    failure: error => Console.WriteLine($"Operation failed: {error.Message}")
);

failureResult.Match(
    success: () => Console.WriteLine("Operation succeeded!"),
    failure: error => Console.WriteLine($"Operation failed: {error.Message}")
);

//check and return a result
var msg = successResult.MatchReturn(
    success: () => "Operation completed successfully.",
    failure: error => $"Operation failed: {error.Message}"
); 

Console.WriteLine(msg); // Output: "Operation completed successfully."
```

### Added in this version (v3.1.1)
```csharp
 FailureOrNothing AddUser(User newUser)
 {
    try
    {
         CheckValidity(newUser);
         await _userService.AddNew(newUser);
         return Nothing.Instance;
         //return FailureOrNothing.Nothing; - still works
    }
    catch (Exception e)
    {
        return e; //automatic conversion to FailureOrNothing
    }
 }
```

---

### **2. Fluent Chaining**
Chain multiple operations using `Then` to ensure subsequent actions execute only on success.
```csharp
var result = FailureOrNothing.Succeed()
    .Then(() => Console.WriteLine("Step 1 completed successfully."))
    .Then(() =>
    {
        Console.WriteLine("Step 2 completed successfully.");
        throw new InvalidOperationException("An error occurred in Step 2.");
    })
    .Then(() => Console.WriteLine("Step 3 completed successfully."));

result.Match(
    success: () => Console.WriteLine("All steps succeeded."),
    failure: error => Console.WriteLine($"Pipeline failed: {error.Message}")
);
```

---

### **3. Asynchronous Operations**
Use `ThenAsync` for chaining asynchronous actions.
```csharp
var asyncResult = await FailureOrNothing.Succeed()
    .ThenAsync(async () =>
    {
        await Task.Delay(100);
        Console.WriteLine("Async operation 1 succeeded.");
    })
    .ThenAsync(async () =>
    {
        await Task.Delay(100);
        Console.WriteLine("Async operation 2 succeeded.");
    });

asyncResult.Match(
    success: () => Console.WriteLine("All async operations succeeded."),
    failure: error => Console.WriteLine($"Async pipeline failed: {error.Message}")
);
```

---

### **4. Pattern Matching with Return Values**
Return different results based on the success or failure state.
```csharp
var result = FailureOrNothing.Fail("Data not found.");
string message = result.MatchReturn(
    success: () => "Operation completed successfully.",
    failure: error => $"Operation failed: {error.Message}"
);
Console.WriteLine(message); // Output: "Operation failed: Data not found."
```

---

### **5. Error Handling**
Use `Catch` to handle errors or `ThrowAsException` to escalate.
```csharp
var result = FailureOrNothing.Fail("Critical error occurred.");

result.Catch(error => Console.WriteLine($"Caught error: {error.Message}"));

try
{
    result.ThrowAsException();
}
catch (Exception ex)
{
    Console.WriteLine($"Exception thrown: {ex.Message}");
}
```

---

### **6. Custom Failure Information**
Leverage the `IFailure` interface to include detailed error contexts.
```csharp
var failureWithException = FailureOrNothing.Fail(new InvalidOperationException("Invalid operation"), "Custom error message");

failureWithException.Match(
    success: () => Console.WriteLine("Success."),
    failure: error =>
    {
        Console.WriteLine($"Error: {error.Message}");
        if (error.InnerException != null)
        {
            Console.WriteLine($"Inner Exception: {error.InnerException.Message}");
        }
    }
);
```
---

### **7. Real-World Example: File Processing Pipeline**
```csharp
using System;
using System.IO;
using Sisusa.Common.ReturnTypes;

public class FileReader
{
    public static FailureOrNothing ReadFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return FailureOrNothing.Fail("File path cannot be null or empty.");
        }

        try
        {
            // Attempt to read the file
            string content = File.ReadAllText(filePath);
            Console.WriteLine("File Content: ");
            Console.WriteLine(content);
            return FailureOrNothing.Nothing; // Operation succeeded
        }
        catch (Exception ex)
        {
            return FailureOrNothing.Fail(ex, $"Failed to read the file: {filePath}");
        }
    }

    public static void Main()
    {
        // Example: Valid file
        var result = ReadFile("validFilePath.txt");
        result.Match(
            success: () => Console.WriteLine("File read successfully!"),
            failure: failureInfo => Console.WriteLine($"Error: {failureInfo.Message}")
        );

        // Example: Invalid file
        var invalidResult = ReadFile("invalidFilePath.txt");
        invalidResult.Match(
            success: () => Console.WriteLine("File read successfully!"),
            failure: failureInfo => Console.WriteLine($"Error: {failureInfo.Message}")
        );
    }
}

var result = FailureOrNothing.Succeed()
    .Then(() => File.ReadAllText("input.txt"))
    .Then(content =>
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("File is empty.");
        Console.WriteLine("File read successfully.");
    });

result.Match(
    success: () => Console.WriteLine("Processing completed."),
    failure: error => Console.WriteLine($"Processing failed: {error.Message}")
);
```

---

## **Use-Cases**
1. **Error Handling in Functional Pipelines:** Use in scenarios requiring step-by-step processing where any failure halts the pipeline.
2. **Reducing Exception Noise:** Replace exception-heavy designs with explicit failure handling.
3. **Encapsulating Error Information:** Standardize error reporting across applications.
4. **Enhancing Async Code Clarity:** Chain asynchronous operations with a clear failure-handling mechanism.
5. **Custom Failure Types:** Build domain-specific failure types by implementing the `IFailure` interface.

---

## **Conclusion**
The `FailureOrNothing` class simplifies error handling and enhances code readability and robustness. Whether you're building APIs, processing pipelines, or handling business logic, this class provides a consistent and expressive way to manage success and failure scenarios.
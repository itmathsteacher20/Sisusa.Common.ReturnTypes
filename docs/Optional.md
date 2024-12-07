# User Documentation: `Optional<T>` Class

## Overview

The `Optional<T>` class provides a structured way to represent the presence or absence of a value in your application. By encapsulating values within an `Optional<T>`, you can explicitly handle scenarios where a value may or may not exist, thereby reducing the risk of `null` reference errors and improving code clarity.

## Key Features
- Encapsulates a value that may or may not be present.
- Provides fluent methods for handling values safely.
- Offers support for transformations, actions, and asynchronous operations on contained values.
- Eliminates reliance on `null` for expressing absence, replacing it with an explicit, type-safe representation.

---

## Creating an `Optional<T>`
You can create an `Optional<T>` instance in several ways:

### 1. Wrapping a Value
```csharp
var optionalValue = Optional<int>.Some(42);
```

### 2. Representing Absence
```csharp
var emptyOptional = Optional<int>.Empty();
```

### 3. Wrapping a Nullable Value
```csharp
int? nullableValue = null;
var optional = Optional<int>.Of(nullableValue);
```

---

## Key Methods and Usage Scenarios

### 1. **Check for Presence**
Determine if the optional contains a value:
```csharp
if (optionalValue.HasValue())
{
    Console.WriteLine("Value is present!");
}
```

### 2. **Provide a Default Value**
Retrieve the contained value or a fallback value if absent:
```csharp
int result = optionalValue.OrElse(10); // Returns 42 if present; otherwise 10.
```

### 3. **Transform the Value**
Apply a transformation function to the value:
```csharp
var transformed = optionalValue.Map(x => x * 2); // Returns Optional.Some(84).
```

### 4. **Execute an Action if Present**
Perform an action when a value is present:
```csharp
optionalValue.IfHasValue(x => Console.WriteLine($"Value: {x}"));
```

### 5. **Handle Absence Gracefully**
Provide a fallback mechanism using a supplier function:
```csharp
int computedValue = optionalValue.OrElseGet(() => ComputeValue());
```

### 6. **Throw an Exception if Absent**
Forcefully handle the absence of a value:
```csharp
int value = optionalValue.OrThrow(new InvalidOperationException("Value is required"));
```

---

## Advanced Features

### 1. **Asynchronous Mapping**
Transform the value asynchronously:
```csharp
var asyncTransformed = await optionalValue.MapAsync(async x => await ComputeAsync(x));
```

### 2. **Flat Mapping**
Flatten nested `Optional` results:
```csharp
var flatMapped = optionalValue.FlatMap(x => ComputeOptional(x));
```

### 3. **Conditional Matching**
Perform different actions based on presence or absence:
```csharp
optionalValue.Match(
    some: x => Console.WriteLine($"Value: {x}"),
    none: () => Console.WriteLine("No value present")
);
```

### 4. **Asynchronous Matching**
Execute asynchronous actions based on presence or absence:
```csharp
await optionalValue.MatchAsync(
    some: async x => await ProcessValueAsync(x),
    none: async () => await HandleEmptyAsync()
);
```

---

## Real-World Use Cases

### 1. **Avoiding Null Reference Exceptions**
Instead of relying on `null` checks:
```csharp
var optionalUser = GetUserById(id); // Returns Optional<User>
optionalUser.IfHasValue(user => Console.WriteLine($"User: {user.Name}"));
```

### 2. **Default Fallbacks**
Providing defaults for configuration values:
```csharp
var configValue = Optional<string>.Of(configuration["Setting"]).OrElse("DefaultValue");
```

### 3. **Chained Transformations**
Simplify transformations without worrying about `null`:
```csharp
var fullName = optionalUser
    .Map(user => user.FirstName + " " + user.LastName)
    .OrElse("Anonymous");
```

### 4. **Functional Error Handling**
Handle errors without exceptions:
```csharp
var result = ComputeValue(input).FlatMap(CheckValidity);
```

---

## Factory Methods

`Optional<T>` includes static helper methods for creation:
- **`Optional<T>.Some(value)`**: Creates an `Optional<T>` containing a value.
- **`Optional<T>.Empty()`**: Returns an empty `Optional<T>`.
- **`Optional<T>.Of(value)`**: Wraps a nullable value in an `Optional<T>`.

Additionally, use the `Option` class for easier readability:
```csharp
var someValue = Option.Some(42);
var noValue = Option.Empty<int>();
```

---

## Limitations and Notes
- **Non-nullable Types**: When working with reference types, ensure you handle null appropriately within transformations.
- **Thread Safety**: The `Optional<T>` class is not thread-safe; use with proper synchronization in concurrent scenarios.

---

This comprehensive utility can significantly improve the safety and readability of your code when handling optional values. By adopting `Optional<T>`, you explicitly model the possibility of absence, reducing potential bugs and enhancing clarity in your codebase.
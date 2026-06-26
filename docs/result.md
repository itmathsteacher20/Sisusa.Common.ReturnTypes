## Sisusa.Common.ReturnTypes.Result

# Result

The `Result` class is a convenience factory for creating success and failure results.

Instead of constructing `Failure`, `FailureOr<T>`, or `FailureOrNothing` directly, the `Result` class provides a clean, discoverable API with methods such as `Ok()`, `Success()`, `Failure()`, `NotFound()`, and `InvalidInput()`.

This keeps application code readable while ensuring failures are created consistently throughout the project.

---

## Why use `Result`?

Rather than throwing exceptions for expected outcomes (such as validation failures or missing records), methods can simply return a result describing what happened.

Instead of writing:

```csharp
return FailureOr<User>.Fail("User not found.");
```

you can write:

```csharp
return Result.NotFound("User");
```

or

```csharp
return Result.Failure<User>("User not found.");
```

The intent is immediately obvious.

---

# Success Results

## `Result.Ok()`

Creates a successful operation that doesn't return a value.

```csharp
return Result.Ok();
```

Useful for commands like:

* Delete
* Save
* Update
* Reset Password

where only success or failure matters.

### Example

```csharp
public FailureOrNothing DeleteUser(int id)
{
    repository.Delete(id);

    return Result.Ok();
}
```

---

## `Result.Ok<T>(value)`

Creates a successful result containing a value.

```csharp
return Result.Ok(user);
```

Equivalent to:

```csharp
return Result.Success(user);
```

### Example

```csharp
public FailureOr<User> GetUser(int id)
{
    var user = repository.Find(id);

    return Result.Ok(user);
}
```

---

## `Result.Success<T>(value)`

Another way to create a successful value.

```csharp
return Result.Success(user);
```

Some developers prefer `Success()`, while others find `Ok()` reads more naturally.

---

## `Result.Nothing`

Represents a successful operation with no return value.

```csharp
return Result.Nothing;
```

Equivalent to:

```csharp
return Result.Ok();
```

---

# Generic Failures

These methods create failures without assigning a specific failure category.

---

## `Result.Failure(string message)`

Returns a failed `FailureOrNothing`.

```csharp
return Result.Failure("Unable to save changes.");
```

---

## `Result.Failure<T>(string message)`

Returns a failed typed result.

```csharp
return Result.Failure<User>("User not found.");
```

---

## `Result.Failure<T>(Exception exception)`

Creates a failure directly from an exception.

```csharp
catch (Exception ex)
{
    return Result.Failure<User>(ex);
}
```

---

## `Result.Failure<T>(string message, Exception exception)`

Adds both a friendly message and the underlying exception.

```csharp
catch (SqlException ex)
{
    return Result.Failure<User>(
        "Database operation failed.",
        ex);
}
```

---

## `Result.Failure<T>(Failure failure)`

Wraps an existing `Failure`.

```csharp
Failure failure = Result.InvalidInput("Name is required.");

return Result.Failure<User>(failure);
```

---

# Standard Failure Types

The remainder of the class provides predefined failures for common situations.

Using these instead of generic failures makes application behaviour more predictable and easier to handle.

---

## NotFound

Returned when a requested resource doesn't exist.

```csharp
return Result.NotFound("User");
```

Several overloads exist for common identifier types.

```csharp
Result.NotFound("User", 15);

Result.NotFound("User", Guid.NewGuid());

Result.NotFound("User", "john.smith");

Result.NotFound("Invoice", 10025L);
```

Exceptions may also be attached.

```csharp
return Result.NotFound("User", id, ex);
```

Typical use cases

* Database record missing
* File missing
* API resource doesn't exist

---

## Conflict

Indicates that the requested operation conflicts with the current state.

```csharp
return Result.Conflict();
```

Custom message:

```csharp
return Result.Conflict(
    "The email address is already in use.");
```

Typical use cases

* Duplicate records
* Version conflicts
* Concurrent updates

---

## ConstraintViolation

Represents a violated business or database constraint.

```csharp
return Result.ConstraintViolation();
```

Example:

```csharp
return Result.ConstraintViolation(
    "Only one active subscription is allowed.");
```

---

## DependencyNotMet

Used when another requirement hasn't been satisfied.

```csharp
return Result.DependencyNotMet(
    "Customer profile must be verified.");
```

Example scenarios

* Required setup incomplete
* Missing configuration
* Required service unavailable

---

## ExternalServiceFailed

Represents failures originating from another system.

```csharp
return Result.ExternalServiceFailed(
    "Payment gateway unavailable.");
```

Useful for:

* REST APIs
* SMTP servers
* Payment providers
* Cloud services

---

## InvalidInput

Indicates invalid user or application input.

```csharp
return Result.InvalidInput(
    "Age must be greater than zero.");
```

Typical scenarios

* Validation failures
* Incorrect format
* Missing required values

---

## Unauthorized

Authentication failed.

```csharp
return Result.Unauthorized();
```

Example:

```csharp
return Result.Unauthorized(
    "Invalid access token.");
```

---

## Denied

The caller is authenticated but lacks permission.

```csharp
return Result.Denied(
    "Only administrators may delete users.");
```

---

## InternalError

Represents an unexpected internal application failure.

```csharp
catch (Exception ex)
{
    return Result.InternalError(exception: ex);
}
```

Useful when the application encounters an unexpected state.

---

## TimeOut

Operation exceeded its allowed time.

```csharp
return Result.TimeOut();
```

Example:

```csharp
return Result.TimeOut(
    "Database query exceeded 30 seconds.");
```

---

## NotImplemented

Marks functionality that hasn't yet been completed.

```csharp
return Result.NotImplemented();
```

Useful during development while features are still under construction.

---

## DatabaseConnectionFailed

Represents failures establishing or maintaining a database connection.

```csharp
return Result.DatabaseConnectionFailed(
    exception: ex);
```

---

# Validation Failures

Several overloads simplify reporting validation problems.

## Basic validation

```csharp
return Result.FailedValidation(
    "User",
    "Email");
```

---

## Validation with reason

```csharp
return Result.FailedValidation(
    "User",
    "Email",
    "Email address is invalid.");
```

---

## Full validation information

```csharp
return Result.FailedValidation(
    source: "User",
    path: "Email",
    message: "Email format is invalid.",
    code: "EMAIL_INVALID",
    validationSeverity: ValidationSeverity.Error,
    exception: ex);
```

This overload is intended for applications that expose detailed validation information to clients.

---

# Common Usage Patterns

## Returning a value

```csharp
public FailureOr<User> FindUser(int id)
{
    var user = repository.Find(id);

    if (user == null)
        return Result.NotFound("User", id);

    return Result.Ok(user);
}
```

---

## Returning success without data

```csharp
public FailureOrNothing DeleteUser(int id)
{
    repository.Delete(id);

    return Result.Ok();
}
```

---

## Validation

```csharp
if (string.IsNullOrWhiteSpace(request.Name))
{
    return Result.InvalidInput(
        "Name cannot be empty.");
}
```

---

## Exception handling

```csharp
try
{
    SaveChanges();

    return Result.Ok();
}
catch (Exception ex)
{
    return Result.InternalError(exception: ex);
}
```

---

## Returning typed failures

```csharp
public FailureOr<Order> CreateOrder(CreateOrderRequest request)
{
    if (request.Items.Count == 0)
        return Result.Failure<Order>(
            Result.InvalidInput("Order must contain at least one item."));

    // ...

    return Result.Ok(order);
}
```

---

# Design Notes

The `Result` class follows the Factory pattern by centralizing the creation of all success and failure results in a single location. Rather than exposing constructors or static methods throughout the codebase, consumers call a consistent set of factory methods.

Benefits include:

* Consistent error creation across the application.
* More expressive code (`Result.NotFound()` communicates intent better than a generic failure).
* Reduced duplication of error creation logic.
* Easier discovery through IntelliSense.
* A single place to add new standardized failure types as the library evolves.



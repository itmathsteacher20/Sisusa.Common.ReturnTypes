# **Sisusa.Common.ReturnTypes.Optional User Documentation**

## **Introduction**

The `Optional<T>` class is a generic type that represents a value that may or may not be present. It provides a functional approach to handling nullable values, reducing the need for null checks and promoting a more concise and less error-prone coding style.

## **Creating Optional Instances**

You can create `Optional` instances using the following methods:

1. **`Of(T? value)`:**
    - Creates an `Optional` with the given value if it's not null.
    - Returns an empty `Optional` if the value is null.

   ```csharp
   Optional<int> optionalInt = Optional.Of(10);
   Optional<string> optionalString = Optional.Of(null); // Empty Optional
   ```

2. **`Some(T value)`:**
    - Creates an `Optional` with the given non-null value.
    - Throws an `ArgumentNullException` if the value is null.

   ```csharp
   Optional<string> someString = Optional.Some("Hello");
   ```

3. **`Empty()` or `None`:**
    - Creates an empty `Optional` representing the absence of a value.

   ```csharp
   Optional<int> emptyOptional = Optional.Empty();
   Optional<string> noneOptional = Optional.None;
   ```

## **Working with Optional Values**

Once you have an `Optional` instance, you can use the following methods to work with its value:

1. **`HasValue()`:**
    - Returns `true` if the `Optional` contains a value, `false` otherwise.

2. **`OrElse(T otherValue)`:**
    - Returns the value if present.
    - Otherwise, returns the specified `otherValue`.

   ```csharp
   int value = optionalInt.OrElse(0);
   ```

3. **`OrElseGet(Func<T> doAction)`:**
    - Returns the value if present.
    - Otherwise, computes and returns a value using the given `doAction` function.

   ```csharp
   int value = optionalInt.OrElseGet(() => CalculateDefaultValue());
   ```

4. **`OrThrow(Exception exception)`:**
    - Returns the value if present.
    - Otherwise, throws the specified `exception`.

   ```csharp
   int value = optionalInt.OrThrow(new InvalidOperationException("Value is missing"));
   ```

5. **`IfHasValue(Action<T> doAction)`:**
    - Executes the given `doAction` with the value if present.

   ```csharp
   optionalInt.IfHasValue(value => Console.WriteLine(value));
   ```

6. **`Map(Func<T> mapFunc)`:**
    - Applies the given mapping function to the value if present.
    - Returns a new `Optional` with the mapped value or an empty `Optional` if the original is empty.

   ```csharp
   Optional<string> optionalString = optionalInt.Map(i => i.ToString());
   ```

7. **`Map<TU>(Func<T, TU> mapFunc)`:**
    - Similar to `Map`, but transforms the value to a different type.

8. **`FlatMap<TU>(Func<T, Optional<TU>> mapFunc)`:**
    - Applies the given mapping function to the value if present.
    - The mapping function should return an `Optional<TU>`.
    - Flattens the result into a single `Optional<TU>`.

9. **`Then<TU>(Func<T, Optional<TU>> doNext)`:**
    - Similar to `FlatMap`, but returns an empty `Optional` if the original is empty.

10. **`Match(Action<T> some, Action none)`:**
- Executes the `some` action if a value is present.
- Executes the `none` action if the `Optional` is empty.

## **Additional Considerations**

- The `Optional` class can be used to represent optional method parameters or return values.
- It can help avoid null pointer exceptions and improve code readability.
- Consider using `Optional` when dealing with nullable values to enhance code clarity and maintainability.

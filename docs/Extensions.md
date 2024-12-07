## **Extensions Detailed Documentation**

### **1. `GlobalExtensions` Class**

#### **Method: `ToOptional<T>(this T value)`**
Converts a value of any type to an `Optional<T>`.

##### **Parameters**:
- **`T value`**: The value to convert. Can be of any type.

##### **Returns**:
An `Optional<T>` instance:
- **`Option.Some<T>`**: If the value is neither `null` nor the default value for its type.
- **`Option.Empty<T>`**: If the value is `null` or the default value for its type.

##### **Usage Example**:

**Scenario**: You want to wrap a potentially null value into an `Optional<T>` for safer handling.

```csharp
string name = null;
var optionalName = name.ToOptional(); // Returns Option.Empty<string>()

int number = 42;
var optionalNumber = number.ToOptional(); // Returns Option.Some(42)
```

---

### **2. `EnumerableExtensions` Class**

#### **Method: `FirstOrNone<T>(this IEnumerable<T> source)`**
Retrieves the first element of a sequence as an `Optional<T>` or an empty `Optional<T>` if the sequence is empty or the first element is `null`/default.

##### **Parameters**:
- **`IEnumerable<T> source`**: The sequence to retrieve the first element from.

##### **Returns**:
- **`Option.Some<T>`**: If a non-null, non-default first element exists.
- **`Option.Empty<T>`**: If the sequence is empty or the first element is `null`/default.

##### **Usage Example**:

**Scenario**: You have a list of names and want to safely fetch the first name.

```csharp
var names = new List<string> { "Alice", "Bob", "Charlie" };
var firstName = names.FirstOrNone(); // Returns Option.Some("Alice")

var emptyList = new List<string>();
var noName = emptyList.FirstOrNone(); // Returns Option.Empty<string>()
```

---

#### **Method: `FailureOrSingle<T>(this IEnumerable<T> source)`**
Retrieves a single element from the sequence or returns a failure if the sequence contains zero or multiple elements.

##### **Parameters**:
- **`IEnumerable<T> source`**: The sequence to retrieve a single element from.

##### **Returns**:
- **`FailureOr<T>.Succeed(value)`**: If exactly one element is present.
- **`FailureOr<T>.Fail(exception, message)`**: If the sequence has zero or multiple elements.

##### **Usage Example**:

**Scenario**: A sequence contains user IDs, and you need to ensure there’s exactly one.

```csharp
var singleUserList = new List<int> { 101 };
var singleUser = singleUserList.FailureOrSingle(); // Returns Success with 101

var multipleUsersList = new List<int> { 101, 102 };
var failure = multipleUsersList.FailureOrSingle(); 
// Returns Failure with message "Source contains multiple elements- single element expected."
```

---

#### **Method: `IsEmpty<T>(this IEnumerable<T> source)`**
Checks whether a sequence is empty.

##### **Parameters**:
- **`IEnumerable<T> source`**: The sequence to check.

##### **Returns**:
- **`true`**: If the sequence is empty.
- **`false`**: If the sequence contains any elements.

##### **Usage Example**:

**Scenario**: You want to validate if a collection contains any items.

```csharp
var numbers = new List<int> { 1, 2, 3 };
bool hasNumbers = numbers.IsEmpty(); // Returns false

var emptyNumbers = new List<int>();
bool isEmpty = emptyNumbers.IsEmpty(); // Returns true
```

---

## **Real-World Use Cases**

### **1. Null-Safe Operations with `Optional<T>`**
Instead of writing defensive checks for null values, use `ToOptional<T>` to create a more expressive codebase. For example:

```csharp
var config = GetConfig() ?? new Config();
var optionalConfig = config.ToOptional();
```

### **2. Error Handling in Collections**
With `FailureOrSingle`, you can safely enforce the presence of exactly one element in collections and provide meaningful error feedback when the condition fails.

### **3. Simplified Collection Validation**
Use `IsEmpty` to simplify checks for emptiness in collections without verbose code:

```csharp
if (students.IsEmpty())
{
    Console.WriteLine("No students found.");
}
```

---

This library offers an intuitive and robust way to deal with common programming challenges, especially in scenarios involving nullable or potentially error-prone operations. The provided methods help ensure clean, maintainable, and error-resilient code.
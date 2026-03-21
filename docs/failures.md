# Failure Types Reference

## ItemNotFoundFailure
**Short code:** `ItemNotFound`

Raised when a lookup operation cannot locate the requested entity. Supports overloads for name-only, `int` ID, `long` ID, and `Guid` identifiers, each with an optional `additionalInfo` string.

**Use cases:**
- A `GET /users/{id}` endpoint finds no matching row in the database
- A file processing service cannot locate a referenced template by name
- A cache lookup misses and the fallback store also has no record

---

## InvalidInputFailure
**Short code:** `InvalidInput`

Raised when input is invalid — either structurally malformed, missing required values, or violating business rules. This is the single consolidation point for both format-level and domain-level input problems.

**Use cases:**
- A required field is null or empty string
- An enum value falls outside the defined range
- A booking request has a check-out date earlier than check-in
- An order total exceeds the user's credit limit

---

## AuthenticationFailure
**Short code:** `AuthenticationFailure`

Raised when the caller's identity cannot be established or verified. Covers both missing credentials and credentials that were provided but rejected.

**Use cases:**
- A request arrives with a missing or malformed JWT
- A session token has expired
- Username/password combination is incorrect
- An MFA challenge was not satisfied

---

## PermissionDeniedFailure
**Short code:** `PermissionDenied`

Raised when the caller is authenticated but lacks the required authorisation to perform the action. HTTP 403 analogue.

**Use cases:**
- A standard user attempts to access an admin-only endpoint
- A user tries to delete a record owned by another user
- A service account has read access but attempts a write operation

---

## DatabaseConnectionFailure
**Short code:** `DatabaseConnectionFailure`

Raised when the application cannot establish or maintain a connection to the database.

**Use cases:**
- The database server is unreachable due to a network partition
- The connection pool is exhausted under load
- Database credentials have rotated and not been updated in the application

---

## ExternalServiceFailure
**Short code:** `ExternalServiceFailure`

Raised when a call to a third-party system fails. Use when the failure is clearly outside your system boundary.

**Use cases:**
- A payment gateway returns a 500 or times out
- An SMS provider rejects a message dispatch request
- A geolocation API returns an unexpected error response

---

## ResourceConflictFailure
**Short code:** `ResourceConflict`

Raised when an operation cannot complete because the target resource is in a conflicting state. HTTP 409 analogue.

**Use cases:**
- Creating a user with an email address that already exists
- Two concurrent requests attempt to update the same record (optimistic concurrency violation)
- Trying to publish a document that is already in `Published` state

---

## TimeoutFailure
**Short code:** `Timeout`

Raised when an operation exceeds its allotted time budget.

**Use cases:**
- A database query runs beyond the configured command timeout
- An HTTP call to an external API doesn't respond within the deadline
- A long-running background job is cancelled after exceeding its SLA window

---

## DependencyFailure
**Short code:** `DependencyFailure`

Raised when a required internal dependency is unavailable or misbehaving. Use `ExternalServiceFailure` for third-party vendors; use this for systems within your own infrastructure.

**Use cases:**
- A downstream microservice in your own infrastructure is down
- A message queue broker is unreachable
- A shared cache (e.g. Redis) is unavailable, breaking a required workflow

---

## ConstraintViolationFailure
**Short code:** `ConstraintViolation`

Raised when a constraint is violated during execution — typically surfaced from the persistence layer after earlier validation has already passed.

**Use cases:**
- A foreign key constraint is violated when deleting a parent record that still has children
- A unique index violation surfaces after a race condition
- A domain invariant (e.g. "an invoice must have at least one line item") is broken at persist time

> **vs `InvalidInputFailure`:** Use `InvalidInputFailure` when the problem can be caught before attempting the operation. Use `ConstraintViolationFailure` when the violation is only discovered during execution.

---

## NotImplementedFailure
**Short code:** `NotImplemented`

Raised when a code path is intentionally not yet implemented. Should never appear in production under normal operation.

**Use cases:**
- A strategy interface has a stub implementation during development
- A feature flag routes to a code path that hasn't been built yet
- An abstract handler has a default branch that should be unreachable once all cases are covered
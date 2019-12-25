# Ajupov.Utils.All

All packages for backend applications.

## Usage

### Passwords
```
var passwordHash = Password.ToPasswordHash("SomePassword") // returns hashed password

var isVerifiedPassword = Password.IsVerifiedPassword(SomePassword", passwordHash)   // returns password is correct
```
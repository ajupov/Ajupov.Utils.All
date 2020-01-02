# Ajupov.Utils.All

All packages for backend applications.

## Usage

### Passwords
```
var passwordHash = Password.ToPasswordHash("SomePassword")

var isVerifiedPassword = Password.IsVerifiedPassword("SomePassword", passwordHash)
```

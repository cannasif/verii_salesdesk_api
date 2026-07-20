# Password reset – manual E2E verification

## Prerequisites

1. **SMTP (Mail Ayarları)**  
   In the admin UI, configure Host, Port, Username, Password, From e-mail, and SSL.  
   Send **Test mail** from the same screen; it must return success.

2. **Hangfire worker**  
   - `SalesdeskRuntime:SkipBackgroundBootstrap` must **not** be `true` on the IIS app pool / production site.  
   - On API startup, logs should contain: `Hangfire background server is enabled`.  
   - Open `/hangfire` (authorized admin) and confirm the server is connected and queues `default`, `dead-letter` are processing.  
   - Password reset e-mail is sent **synchronously** in `request-password-reset` (not via Hangfire enqueue). Other auth mails (reset completed, password changed) still use Hangfire.

3. **Frontend URL**  
   `appsettings` / environment: `FrontendSettings:BaseUrl` = `https://salesdesk.v3rii.com`, `ResetPasswordPath` = `/reset-password` (used when the client omits `resetUrl`).

## Request password reset

```http
POST /api/auth/request-password-reset
Content-Type: application/json

{
  "email": "known-user@domain.com",
  "resetUrl": "https://salesdesk.v3rii.com/reset-password"
}
```

**Expected**

| Case | HTTP | `success` | Notes |
|------|------|-----------|--------|
| Unknown e-mail | 200 | `true` | Generic success (no user enumeration) |
| Known user, SMTP OK | 200 | `true` | Inbox receives mail; link is `{resetUrl}?token=...` |
| Known user, SMTP missing/incomplete | 503 | `false` | `errorCode`: `PasswordResetEmailSendFailed`, localized message |
| Known user, SMTP OK but send fails | 503 | `false` | Same; check API logs for `UserId`, `Email`, `TraceId` |

Verify e-mail match: same request with different casing / surrounding spaces on `email` still finds the user.

## Reset password

Copy `token` from the e-mail link query string.

```http
POST /api/auth/reset-password
Content-Type: application/json

{
  "token": "<token>",
  "newPassword": "NewSecurePass123"
}
```

**Expected**

| Case | HTTP | `errorCode` |
|------|------|-------------|
| Valid token | 200 | — |
| Invalid / used / expired token | 400 | `PasswordResetTokenInvalidOrExpired` |

## Automated tests

```bash
dotnet test tests/salesdesk_api.Tests/salesdesk_api.Tests.csproj
```

Covers reset link URL building (`PasswordResetLinkBuilder`).

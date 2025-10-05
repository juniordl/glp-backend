namespace GLP.Identity.Application;

public sealed record RegisterRequest(string UserName, string Password, string? FullName);
public sealed record LoginRequest(string UserName, string Password, bool RememberMe);
public sealed record ResetPasswordRequest(Guid UserId, string Token, string NewPassword);
public sealed record MeResponse(Guid UserId, string UserName, string? FullName, bool EmailConfirmed, IEnumerable<string> Roles);
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Sienar.Security;

/// <summary>
/// Retrieves user information based on the HTTP context
/// </summary>
public class HttpContextUserAccessor : IUserAccessor
{
	private readonly HttpContext _context;

	/// <summary>
	/// Creates a new instance of <c>HttpContextUserAccessor</c>
	/// </summary>
	/// <param name="httpContextAccessor">The HTTP context accessor</param>
	public HttpContextUserAccessor(
		IHttpContextAccessor httpContextAccessor)
	{
		_context = httpContextAccessor.HttpContext!;
	}

	/// <inheritdoc />
	public bool IsSignedIn() => _context.User.Identity?.IsAuthenticated ?? false;

	/// <inheritdoc />
	public int? GetUserId()
	{
		var claim = _context.User.Claims
			.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
		return claim is null
			? null
			: int.Parse(claim.Value);
	}

	/// <inheritdoc />
	public string? GetUsername()
	{
		return _context.User.Claims
			.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
	}

	/// <inheritdoc />
	public ClaimsPrincipal GetUserClaimsPrincipal()
		=> _context.User;

	/// <inheritdoc />
	public bool UserInRole(string roleName)
		=> _context.User.IsInRole(roleName);
}
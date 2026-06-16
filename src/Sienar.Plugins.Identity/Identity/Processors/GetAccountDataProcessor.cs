#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Security.Claims;

namespace Sienar.Identity.Processors;

/// <exclude />
public class GetAccountDataProcessor : IResultProcessor<AccountDataResult>
{
	private readonly IUserAccessor _userAccessor;

	public GetAccountDataProcessor(IUserAccessor userAccessor)
	{
		_userAccessor = userAccessor;
	}

	public Task<OperationResult<AccountDataResult>> Process()
	{
		if (!_userAccessor.IsSignedIn())
		{
			return Task.FromResult(new OperationResult<AccountDataResult>(OperationStatus.Unauthorized));
		}

		var roles = (_userAccessor.GetUserClaimsPrincipal()).Claims
			.Where(c => c.Type == ClaimTypes.Role)
			.Select(c => c.Value)
			.ToList();

		var result = new AccountDataResult
		{
			Username = _userAccessor.GetUsername()!,
			Roles = roles
		};

		return Task.FromResult(new OperationResult<AccountDataResult>(result: result));
	}
}

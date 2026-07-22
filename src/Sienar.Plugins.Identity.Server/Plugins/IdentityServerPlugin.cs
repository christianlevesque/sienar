#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Sienar.Identity.Processors;

namespace Sienar.Plugins;

/// <exclude />
[AppConfigurer(typeof(IdentityServerAppConfigurer))]
public class IdentityServerPlugin<TUser> : IPlugin
	where TUser : class, ISienarIdentityUser<TUser>, new()
{
	private readonly WebApplicationBuilder _builder;

	public IdentityServerPlugin(
		WebApplicationBuilder builder)
	{
		_builder = builder;
	}

	public void Configure()
	{
		var services = _builder.Services;
		var config = _builder.Configuration;

		services.AddHttpContextAccessor();

		services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
		services.TryAddScoped<IPasswordManager<TUser>, PasswordManager<TUser>>();
		services.TryAddScoped<IUserClaimsFactory<TUser>, ServerUserClaimsFactory<TUser>>();
		services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, Identity.UserClaimsPrincipalFactory<TUser>>();
		services.TryAddScoped<IVerificationCodeManager<TUser>, VerificationCodeManager<TUser>>();

		services.TryAddScoped<IEmailSender, DefaultEmailSender>();


		/************
		 * Identity *
		 ***********/

		services.TryAddScoped<IUserAccessor, HttpContextUserAccessor>();
		services.TryAddScoped<IAccountEmailMessageFactory, AccountEmailMessageFactory>();
		services.TryAddScoped<IAccountEmailManager<TUser>, AccountEmailManager<TUser>>();
		services.TryAddScoped<IAccountUrlProvider, AccountUrlProvider>();

		// CRUD
		services
			.AddEfEntity<ViewUserDto, ViewUserMapper<TUser>, UpsertUserDto, UpsertUserMapper<TUser>, UpsertUserDto, UpsertUserMapper<TUser>, TUser, SienarUserFilterProcessor<TUser>>()
			.AddAccessValidator<UserIsAdminAccessValidator<TUser>, TUser>()
			.AddBeforeDeleteActionHook<RemoveIdentityRelationsOnUserDeleted<TUser>, TUser>()
			.AddStateValidator<EnsureUsernameUniqueOnUpsert<TUser>, TUser>()
			.AddStateValidator<EnsureEmailUniqueOnUpsert<TUser>, TUser>()
			.AddEfEntity<LockoutReasonDto, LockoutReasonToEntityMapper<TUser>, LockoutReasonToDtoMapper<TUser>, LockoutReason<TUser>, LockoutReasonFilterProcessor<TUser>>()

		// Security
			.AddGeneralProcessor<LoginProcessor<TUser>, LoginRequest, LoginResult>()
			.AddStatusProcessor<LogoutProcessor<TUser>, LogoutRequest>()
			.AddResultProcessor<PersonalDataProcessor<TUser>, PersonalDataResult>()
			.AddAccessValidator<UserIsAdminAccessValidator<AddUserToRoleRequest>, AddUserToRoleRequest>()
			.AddAccessValidator<UserIsAdminAccessValidator<RemoveUserFromRoleRequest>, RemoveUserFromRoleRequest>()
			.AddStatusProcessor<LockUserAccountProcessor<TUser>, LockUserAccountRequest>()
			.AddAccessValidator<UserIsAdminAccessValidator<LockUserAccountRequest>, LockUserAccountRequest>()
			.AddStatusProcessor<UnlockUserAccountProcessor<TUser>, UnlockUserAccountRequest>()
			.AddAccessValidator<UserIsAdminAccessValidator<UnlockUserAccountRequest>, UnlockUserAccountRequest>()
			.AddStatusProcessor<ManuallyConfirmUserAccountProcessor<TUser>, ManuallyConfirmUserAccountRequest>()
			.AddAccessValidator<UserIsAdminAccessValidator<ManuallyConfirmUserAccountRequest>, ManuallyConfirmUserAccountRequest>()
			.AddStatusProcessor<ChangePasswordProcessor<TUser>, ChangePasswordRequest>()
			.AddStatusProcessor<ForgotPasswordProcessor<TUser>, ForgotPasswordRequest>()
			.AddStatusProcessor<ResetPasswordProcessor<TUser>, ResetPasswordRequest>()
			.AddResultProcessor<GetAccountDataProcessor, AccountDataResult>()
			.AddGeneralProcessor<GetLockoutReasonsProcessor<TUser>, AccountLockoutRequest, AccountLockoutResult>()

		// Registration
			.AddStateValidator<RegistrationOpenValidator, RegisterRequest>()
			.AddStateValidator<AcceptTosValidator, RegisterRequest>()
			.AddStateValidator<EnsureUsernameUniqueOnRegister<TUser>, RegisterRequest>()
			.AddStateValidator<EnsureEmailUniqueOnRegister<TUser>, RegisterRequest>()
			.AddStatusProcessor<RegisterProcessor<TUser>, RegisterRequest>()

		// Email
			.AddStatusProcessor<ConfirmAccountProcessor<TUser>, ConfirmAccountRequest>()
			.AddStatusProcessor<InitiateEmailChangeProcessor<TUser>, InitiateEmailChangeRequest>()
			.AddStatusProcessor<PerformEmailChangeProcessor<TUser>, PerformEmailChangeRequest>()

		// Personal data
			.AddBeforeStatusActionHook<RemoveIdentityRelationsOnOwnAccountDeleted<TUser>, DeleteAccountRequest>()
			.AddStatusProcessor<DeleteAccountProcessor<TUser>, DeleteAccountRequest>();


		/********
		 * Auth *
		 *******/

		services.TryAddScoped<ISignInManager<TUser>, CookieSignInManager<TUser>>();


		/***********
		 * Options *
		 **********/

		services
			.ApplyDefaultConfiguration<SienarOptions>(config.GetSection("Sienar:Core"))
			.ApplyDefaultConfiguration<EmailSenderOptions>(config.GetSection("Sienar:Email:Sender"))
			.ApplyDefaultConfiguration<IdentityEmailSubjectOptions>(config.GetSection("Sienar:Email:IdentityEmailSubjects"))
			.ApplyDefaultConfiguration<LoginOptions>(config.GetSection("Sienar:Login"));
	}
}

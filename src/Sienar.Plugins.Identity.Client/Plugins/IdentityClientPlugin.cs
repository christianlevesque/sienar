using Sienar.Layouts;

namespace Sienar.Plugins;

/// <summary>
/// Configures the Sienar application to run the WASM client
/// </summary>
[AppConfigurer(typeof(SienarAppConfigurer))]
public class IdentityClientPlugin : IPlugin
{
	private readonly IApplicationAdapter _adapter;
	private readonly IConfiguration _configuration;
	private readonly ComponentProvider _componentProvider;
	private readonly GlobalComponentProvider _globalComponentProvider;
	private readonly MenuProvider _menuProvider;
	private readonly RoutableAssemblyProvider _routableAssemblyProvider;
	private readonly StyleProvider _styleProvider;

	/// <summary>
	/// Creates a new instance of <c>CoreClientPlugin</c>
	/// </summary>
	public IdentityClientPlugin(
		IApplicationAdapter adapter,
		IConfiguration configuration,
		ComponentProvider componentProvider,
		GlobalComponentProvider globalComponentProvider,
		MenuProvider menuProvider,
		RoutableAssemblyProvider routableAssemblyProvider,
		StyleProvider styleProvider)
	{
		_adapter = adapter;
		_configuration = configuration;
		_componentProvider = componentProvider;
		_globalComponentProvider = globalComponentProvider;
		_menuProvider = menuProvider;
		_routableAssemblyProvider = routableAssemblyProvider;
		_styleProvider = styleProvider;
	}

	/// <inheritdoc />
	public void Configure()
	{
		SetupComponents();
		SetupMenu();
		SetupRoutableAssemblies();
		SetupStyles();
		SetupServices();
	}

	private void SetupComponents()
	{
		_componentProvider
			.Access(typeof(DashboardLayout))
			.TryAddComponent<DrawerHeader>(DashboardLayoutSections.SidebarHeader)
			.TryAddComponent<DrawerFooter>(DashboardLayoutSections.SidebarFooter);

		_globalComponentProvider.DefaultMenus = [IdentityMenus.Main, IdentityMenus.Info];
	}

	private void SetupMenu()
	{
		_menuProvider
			.CreateMainMenu()
			.CreateUserSettingsMenu()
			.CreateInfoMenu()
			.CreateUserManagementMenu();
	}

	private void SetupRoutableAssemblies()
	{
		_routableAssemblyProvider.Add(typeof(IdentityClientPlugin).Assembly);
	}

	private void SetupStyles()
	{
		_styleProvider.Add("/_content/Sienar.Ui/sienar.css");
		_styleProvider.Add("/_content/Sienar.Ui/Sienar.Ui.bundle.scp.css");
	}

	private void SetupServices()
	{
		// Client only
		_adapter.AddServices(s =>
		{
			// Infrastructure
			s
				.AddBeforeStatusActionHook<LoadUserDataOnStartup, Startup>();

			s.TryAddScoped<INotifier, DefaultNotifier>();
			s.TryAddScoped<IUserClaimsFactory<ViewUserDto>, ClientUserClaimsFactory>();

			s
				// Account
				.AddAfterGeneralActionHook<LoadUserDataOnLogin, LoginRequest>()
				.AddAfterGeneralActionHook<RefreshCsrfTokenOnLogin, LoginRequest>()
				.AddAfterStatusActionHook<LogOutUiAfterLogout, LogoutRequest>()
				.AddAfterStatusActionHook<RefreshCsrfTokenOnLogout, LogoutRequest>()
				.AddStateValidator<EnsureTosAccepted, RegisterRequest>()
				.AddAfterStatusActionHook<LogOutAfterDeletingAccount, DeleteAccountRequest>();

			s.ApplyDefaultConfiguration<SienarOptions>(
				_configuration.GetSection("Sienar:Core"));
		});
	}

	private class SienarAppConfigurer : IConfigurer<SienarAppBuilder>
	{
		public void Configure(SienarAppBuilder builder)
		{
			builder
				.AddPlugin<MudBlazorPlugin>()
				.AddPlugin<CoreClientPlugin>();
		}
	}
}
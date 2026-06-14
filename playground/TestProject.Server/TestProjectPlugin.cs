using Microsoft.AspNetCore.Builder;
using Sienar.Configuration;
using Sienar.Extensions;
using Sienar.Html;
using Sienar.Infrastructure;
using Sienar.Menus;
using Sienar.Plugins;
using TestProject.Client.Extensions;
using TestProject.Data;

namespace TestProject;

[AppConfigurer(typeof(SienarAppConfigurer))]
public class TestProjectPlugin : IPlugin
{
	private readonly WebApplicationBuilder _builder;
	private readonly RoutableAssemblyProvider _routableAssemblyProvider;
	private readonly MenuProvider _menuProvider;
	private readonly StyleProvider _styleProvider;

	public TestProjectPlugin(
		WebApplicationBuilder builder,
		RoutableAssemblyProvider routableAssemblyProvider,
		MenuProvider menuProvider,
		StyleProvider styleProvider)
	{
		_builder = builder;
		_routableAssemblyProvider = routableAssemblyProvider;
		_menuProvider = menuProvider;
		_styleProvider = styleProvider;
	}

	public void Configure()
	{
		_builder.Services
			.AddDbContextForSienar<AppDbContext>(o => o.UseSienarDb());

		_routableAssemblyProvider.Add(typeof(TestProjectPlugin).Assembly);
		_menuProvider.AddMenu();

		ConfigureStyles();
	}

	private void ConfigureStyles()
	{
		_styleProvider.Add("/styles.css");
		_styleProvider.Add("/TestProject.Client.Wasm.styles.css");
	}

	private class SienarAppConfigurer : IConfigurer<SienarAppBuilder>
	{
		public void Configure(SienarAppBuilder builder)
		{
			builder.AddPlugin<IdentityPlugin<AppUser>>();
		}
	}
}

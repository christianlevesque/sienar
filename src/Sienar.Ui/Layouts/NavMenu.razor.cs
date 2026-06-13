using System.Reflection;

namespace Sienar.Layouts;

public partial class NavMenu
{
	private readonly List<List<MenuLink>> _menus = [];
	private ComponentDictionary _components = null!;

	/// <summary>
	/// The type of the layout
	/// </summary>
	[CascadingParameter(Name = SienarFound.CascadingLayoutName)]
	public Type LayoutType { get; set; } = null!;

	[CascadingParameter]
	private RouteData RouteData { get; set; } = null!;

	[CascadingParameter]
	private Task<AuthenticationState>? AuthState { get; set; }

	[Inject]
	private ComponentProvider ComponentProvider { get; set; } = null!;

	[Inject]
	private GlobalComponentProvider GlobalComponentProvider { get; set; } = null!;

	[Inject]
	private IMenuGenerator MenuGenerator { get; set; } = null!;

	[Inject]
	private NavigationManager NavManager { get; set; } = null!;

	/// <inheritdoc />
	protected override async Task OnInitializedAsync()
	{
		_components = ComponentProvider.Access(LayoutType);
		await UpdateMenuAndRender();
	}

	private async Task UpdateMenuAndRender()
	{
		_menus.Clear();
		var pageType = RouteData.PageType;
		var menuNames = pageType.GetCustomAttribute<MenusAttribute>()
			?.Names ?? GlobalComponentProvider.DefaultMenus;

		foreach (var name in menuNames)
		{
			_menus.Add(await MenuGenerator.Create(name));
		}
	}
}

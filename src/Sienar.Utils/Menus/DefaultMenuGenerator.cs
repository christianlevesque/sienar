#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Sienar.Menus;

/// <exclude />
public class DefaultMenuGenerator : IMenuGenerator
{
	private readonly IUserAccessor _userAccessor;
	private readonly MenuProvider _provider;

	public DefaultMenuGenerator(
		IUserAccessor userAccessor,
		MenuProvider menuProvider)
	{
		_userAccessor = userAccessor;
		_provider = menuProvider;
	}

	/// <inheritdoc />
	public List<MenuLink> Create(string name)
	{
		var linkDictionary = _provider.Access(name);
		return ProcessNavLinks(linkDictionary.AggregatePrioritized());
	}

	private List<MenuLink> ProcessNavLinks(List<MenuLink> navLinks)
	{
		var includedLinks = new List<MenuLink>();

		foreach (var link in navLinks)
		{
			if (!UserIsAuthorized(link))
			{
				continue;
			}

			if (link.ChildMenu is not null)
			{
				// TODO: #117
				link.Text ??= link.ChildMenu;
				link.Sublinks = Create(link.ChildMenu);
			}

			includedLinks.Add(link);
		}

		return includedLinks;
	}

	private bool UserIsAuthorized(MenuLink menuLink)
	{
		if (menuLink.RequireLoggedIn && !_userAccessor.IsSignedIn()) return false;
		if (menuLink.RequireLoggedOut && _userAccessor.IsSignedIn()) return false;
		if (menuLink.Roles is null) return true;

		foreach (var role in menuLink.Roles)
		{
			if (_userAccessor.UserInRole(role))
			{
				if (menuLink.AllRolesRequired)
				{
					continue;
				}

				return true;
			}

			if (menuLink.AllRolesRequired)
			{
				return false;
			}
		}

		return menuLink.AllRolesRequired;
	}
}
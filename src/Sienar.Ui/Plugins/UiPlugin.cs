namespace Sienar.Plugins;

/// <summary>
/// A plugin to configure the core Sienar UI
/// </summary>
public class UiPlugin : IPlugin
{
	private StyleProvider _sp;

	/// <summary>
	/// Creates a new instance of the Bulma plugin
	/// </summary>
	/// <param name="sp">The style provider</param>
	public UiPlugin(StyleProvider sp)
	{
		_sp = sp;
	}

	/// <inheritdoc />
	public void Configure()
	{
		_sp.Add("https://cdn.jsdelivr.net/npm/bulma@1.0.4/css/bulma.min.css");
	}
}

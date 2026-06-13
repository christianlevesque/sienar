namespace Sienar.Ui;

/// <summary>
/// A standard button
/// </summary>
public partial class Button
{
	private string Css => new CssBuilder()
		.AddClasses(Attributes)
		.AddClasses("button")
		.AddClasses($"is-{Color?.GetHtmlValue()}", Color.HasValue)
		.AddClasses($"is-{Variant?.GetHtmlValue()}", Variant.HasValue)
		.AddClasses($"is-{Size?.GetHtmlValue()}", Size.HasValue && Size.Value is not ThemeSize.Normal)
		.AddClasses("is-outlined", Outlined)
		.AddClasses("is-inverted", Inverted)
		.AddClasses("is-rounded", Rounded)
		.AddClasses("is-loading", Loading)
		.Build();

	/// <summary>
	/// The theme color of the button
	/// </summary>
	[Parameter]
	public ThemeColor? Color { get; set; }

	/// <summary>
	/// The theme color variant of the button
	/// </summary>
	[Parameter]
	public ThemeColorVariant? Variant { get; set; }

	/// <summary>
	/// The theme size of the button
	/// </summary>
	[Parameter]
	public ThemeSize? Size { get; set; }

	/// <summary>
	/// Whether the button should be an outlined variant
	/// </summary>
	[Parameter]
	public bool Outlined { get; set; }

	/// <summary>
	/// Whether the button should be an inverted color variant
	/// </summary>
	[Parameter]
	public bool Inverted { get; set; }

	/// <summary>
	/// Whether the button should be a rounded variant
	/// </summary>
	[Parameter]
	public bool Rounded { get; set; }

	/// <summary>
	/// Whether the button should be in a loading state
	/// </summary>
	[Parameter]
	public bool Loading { get; set; }

	/// <summary>
	/// The URL to which the button should send the user, if provided
	/// </summary>
	/// <remarks>
	/// If this value is provided, the button is rendered as an <c>&lt;a&gt;</c>. Otherwise, the button is rendered as a <c>&lt;button&gt;</c>.
	/// </remarks>
	[Parameter]
	public string? Href { get; set; }

	/// <summary>
	/// The child content
	/// </summary>
	[Parameter]
	public RenderFragment? ChildContent { get; set; }
}


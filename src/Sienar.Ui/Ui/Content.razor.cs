namespace Sienar.Ui;

/// <summary>
/// Renders plain HTML content
/// </summary>
public partial class Content
{
	private string Css => new CssBuilder()
		.AddClasses(Attributes)
		.AddClasses("content")
		.AddClasses($"is-{Size.GetHtmlValue()}", Size is not ThemeSize.Normal)
		.Build();

	/// <summary>
	/// The content size
	/// </summary>
	[Parameter]
	public ThemeSize Size { get; set; } = ThemeSize.Normal;

	/// <summary>
	/// The child content
	/// </summary>
	[Parameter]
	[EditorRequired]
	public RenderFragment ChildContent { get; set; }
}

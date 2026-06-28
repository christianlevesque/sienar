namespace Sienar.Ui.Containers;

/// <summary>
/// A content container with options for size and alignment
/// </summary>
public class ContainerTagHelper : TagHelperWithChildContent<ContainerTagHelperModel>
{
	/// <summary>
	/// The container's maximum width
	/// </summary>
	[HtmlAttributeName("max-width")]
	public Breakpoint? MaxWidth { get; set; }

	/// <summary>
	/// The container's horizontal alignment
	/// </summary>
	/// <remarks>
	/// While it is possible to set values of <see cref="Alignment.Top"/> or <see cref="Alignment.Bottom"/>, these values are not valid and will generate an exception
	/// </remarks>
	[HtmlAttributeName("alignment")]
	public Alignment? Alignment { get; set; }

	/// <summary>
	/// Whether the container should fluidly expand to its parent's full width
	/// </summary>
	[HtmlAttributeName("fluid")]
	public bool Fluid { get; set; }

	/// <summary>
	/// Creates a new instance of <c>ContainerTagHelper</c>
	/// </summary>
	/// <param name="htmlHelper">The HTML helper</param>
	public ContainerTagHelper(
		IHtmlHelper htmlHelper)
		: base(
			htmlHelper,
			"/Views/Partials/Container.cshtml") {}

	/// <inheritdoc />
	protected override ContainerTagHelperModel CreateModel(TagHelperContext context, TagHelperOutput output)
	{
		if (Alignment is Ui.Alignment.Top or Ui.Alignment.Bottom)
		{
			throw new InvalidOperationException($"The {nameof(Alignment)} attribute must not have a value of {nameof(Ui.Alignment.Top)} or {nameof(Ui.Alignment.Bottom)}");
		}

		return new ContainerTagHelperModel
		{
			Tag = "div",
			MaxWidth = MaxWidth,
			Alignment = Alignment,
			Fluid = Fluid
		};
	}
}

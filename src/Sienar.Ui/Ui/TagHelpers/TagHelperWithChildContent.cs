namespace Sienar.Ui.TagHelpers;

/// <summary>
/// A tag helper which passes its provided child content to its partial
/// </summary>
/// <typeparam name="TModel">The type of the partial's model</typeparam>
public abstract class TagHelperWithChildContent<TModel> : TagHelper
	where TModel : TagHelperWithChildContentModel, new()
{
	private readonly string _partialName;
	private readonly IHtmlHelper _htmlHelper;

	/// <summary>
	/// The HTML tag with which to render the content
	/// </summary>
	[HtmlAttributeName("tag")]
	public string? Tag { get; set; }

	/// <summary>
	/// The view execution context
	/// </summary>
	[HtmlAttributeNotBound]
	[ViewContext]
	public ViewContext ViewContext { get; set; } = null!;

	/// <summary>
	/// Creates a new instance of <c>TagHelperWithChildContent</c>
	/// </summary>
	/// <param name="htmlHelper">The HTML helper</param>
	/// <param name="partialName">The name of the partial with which to render the tag helper</param>
	protected TagHelperWithChildContent(
		IHtmlHelper htmlHelper,
		string partialName)
	{
		_htmlHelper = htmlHelper;
		_partialName = partialName;
	}

	/// <inheritdoc />
	public override async Task ProcessAsync(
		TagHelperContext context,
		TagHelperOutput output)
	{
		(_htmlHelper as IViewContextAware)!.Contextualize(ViewContext);
		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;

		var model = CreateModel(context, output);

		if (!string.IsNullOrEmpty(Tag))
		{
			model.Tag = Tag;
		}

		model.Attributes = output.Attributes;
		model.ChildContent = await output.GetChildContentAsync();

		var content = await _htmlHelper.PartialAsync(_partialName, model);
		output.Content.SetHtmlContent(content);
	}

	/// <summary>
	/// Creates the partial model associated with the tag helper
	/// </summary>
	/// <param name="context">The runtime-provided tag helper context</param>
	/// <param name="output">The runtime-provided tag helper output</param>
	/// <returns>The new model</returns>
	protected abstract TModel CreateModel(
		TagHelperContext context,
		TagHelperOutput output);
}

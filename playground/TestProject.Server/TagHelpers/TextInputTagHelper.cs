using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TestProject.TagHelpers;

public class TextInputTagHelper : TagHelper
{
	private readonly IHtmlGenerator _htmlGenerator;
	private readonly IHtmlHelper _htmlHelper;

	private readonly Func<bool, HtmlEncoder, Task<TagHelperContent>> _defaultTagHelperContent = (_, _) => Task.FromResult((TagHelperContent)new DefaultTagHelperContent());

	/// <inheritdoc />
	public TextInputTagHelper(
		IHtmlGenerator htmlGenerator,
		IHtmlHelper htmlHelper)
	{
		_htmlGenerator = htmlGenerator;
		_htmlHelper = htmlHelper;
	}

	[HtmlAttributeName("asp-for")]
	public ModelExpression For { get; set; } = null!;

	[HtmlAttributeName("asp-format")]
	public string? Format { get; set; }

	[HtmlAttributeNotBound]
	[ViewContext]
	public ViewContext ViewContext { get; set; } = null!;

	public override async Task ProcessAsync(
		TagHelperContext context,
		TagHelperOutput output)
	{
		(_htmlHelper as IViewContextAware)!.Contextualize(ViewContext);
		output.TagName = "fieldset";
		output.TagMode = TagMode.StartTagAndEndTag;

		var model = new FormTagHelperModel
		{
			LabelHtml = await CreateLabel(context, output),
			InputHtml = await CreateInput(context, output)
		};

		var content = await _htmlHelper.PartialAsync("~/Views/Shared/TextInput.cshtml", model);
		output.Content.SetHtmlContent(content);
	}

	private async Task<TagHelperOutput> CreateLabel(
		TagHelperContext context,
		TagHelperOutput output)
	{
		var label = new LabelTagHelper(_htmlGenerator)
		{
			ViewContext = ViewContext,
			For = For
		};

		var labelOutput = new TagHelperOutput(
			"label",
			new TagHelperAttributeList(),
			_defaultTagHelperContent);

		TagHelperContent labelChildContent = new DefaultTagHelperContent();
		var providedChildContent = await output.GetChildContentAsync();

		if (providedChildContent.IsEmptyOrWhiteSpace)
		{
			providedChildContent.Append(For.Metadata.GetDisplayName());
		}

		labelChildContent = labelChildContent.AppendHtml(providedChildContent);
		labelOutput.Content.SetHtmlContent(labelChildContent);

		await label.ProcessAsync(context, labelOutput);
		return labelOutput;
	}

	private async Task<TagHelperOutput> CreateInput(
		TagHelperContext context,
		TagHelperOutput output)
	{
		var attributes = new TagHelperAttributeList(output.Attributes);
		output.Attributes.Clear();

		var input = new InputTagHelper(_htmlGenerator)
		{
			ViewContext = ViewContext,
			For = For,
			Format = Format
		};

		var inputOutput = new TagHelperOutput(
			"input",
			attributes,
			_defaultTagHelperContent)
		{
			TagMode = TagMode.SelfClosing
		};

		input.Init(context);
		await input.ProcessAsync(context, inputOutput);
		return inputOutput;
	}
}

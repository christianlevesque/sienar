using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TestProject.TagHelpers;

public class FormTagHelperModel
{
	public required TagHelperOutput InputHtml { get; set; }
	public required TagHelperOutput LabelHtml { get; set; }
}

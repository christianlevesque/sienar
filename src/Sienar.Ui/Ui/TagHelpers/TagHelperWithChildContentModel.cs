namespace Sienar.Ui.TagHelpers;

/// <summary>
/// Render configuration for tag helpers with child content
/// </summary>
public class TagHelperWithChildContentModel : TagHelperModel
{
	/// <summary>
	/// The child content provided to the tag helper
	/// </summary>
	public TagHelperContent ChildContent { get; set; } = null!;
}

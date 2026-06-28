namespace Sienar.Ui.TagHelpers;

/// <summary>
/// Shared tag helper render configuration
/// </summary>
public class TagHelperModel
{
	/// <summary>
	/// The tag with which to render the tag helper content
	/// </summary>
	public string Tag { get; set; } = null!;

	/// <summary>
	/// The attributes provided to the tag helper
	/// </summary>
	public TagHelperAttributeList Attributes { get; set; } = null!;

	/// <summary>
	/// Builds HTML attributes for use in Razor files
	/// </summary>
	/// <param name="includeClass">Whether to include the class attribute</param>
	/// <returns>The final attribute string</returns>
	public IHtmlContent BuildAttributes(bool includeClass = false)
	{
		var sb = new StringBuilder();

		foreach (var attribute in Attributes)
		{
			if (!includeClass && attribute.Name == "class")
			{
				continue;
			}

			sb
				.Append(' ')
				.Append(HtmlEncoder.Default.Encode(attribute.Name));

			if (attribute.Value is not null)
			{
				sb
					.Append("=\"")
					.Append(HtmlEncoder.Default.Encode(attribute.Value.ToString()!))
					.Append('"');
			}
		}

		return new HtmlString(sb.ToString());
	}
}

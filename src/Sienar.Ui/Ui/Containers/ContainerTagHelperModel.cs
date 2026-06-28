using Sienar.Ui.TagHelpers;

namespace Sienar.Ui.Containers;

/// <summary>
/// Render configuration for the <c>&lt;container&gt;</c> tag helper
/// </summary>
public class ContainerTagHelperModel : TagHelperWithChildContentModel
{
	/// <summary>
	/// The container's maximum width
	/// </summary>
	public Breakpoint? MaxWidth { get; set; }

	/// <summary>
	/// The container's alignment
	/// </summary>
	public Alignment? Alignment { get; set; }

	/// <summary>
	/// Whether the container should be fluid
	/// </summary>
	public bool Fluid { get; set; }
}

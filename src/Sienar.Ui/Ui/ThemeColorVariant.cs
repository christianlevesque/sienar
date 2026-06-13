namespace Sienar.Ui;

/// <summary>
/// Sienar-supported theme color variants
/// </summary>
public enum ThemeColorVariant
{
	/// <summary>
	/// The light theme color variant
	/// </summary>
	[HtmlValue("light")]
	Light,

	/// <summary>
	/// The dark theme color variant
	/// </summary>
	[HtmlValue("dark")]
	Dark,

	/// <summary>
	/// The soft theme color variant
	/// </summary>
	[HtmlValue("soft")]
	Soft,

	/// <summary>
	/// The bold theme color variant
	/// </summary>
	[HtmlValue("bold")]
	Bold,

	/// <summary>
	/// The on-scheme theme color variant
	/// </summary>
	[HtmlValue("on-scheme")]
	OnScheme
}

using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Sienar;

/// <summary>
/// 
/// </summary>
public static class CorePluginConstants
{
	/// <summary>
	/// Contains string constants pointing to specific <see cref="ViewDataDictionary"/> keys
	/// </summary>
	public static class ViewData
	{
		/// <summary>
		/// Corresponds to <c>ViewData['Menu"]</c>
		/// </summary>
		public const string Menu = nameof(Menu);

		/// <summary>
		/// Corresponds to <c>ViewData["Title"]</c>
		/// </summary>
		public const string Title = nameof(Title);
	}

	/// <summary>
	/// Contains string constants pointing to specific <see cref="PartialProvider"/> keys
	/// </summary>
	public static class Partials
	{
		/// <summary>
		/// Corresponds to <c>ViewData["OffcanvasHeader"]</c>
		/// </summary>
		public const string OffcanvasHeader = nameof(OffcanvasHeader);

		/// <summary>
		/// Corresponds to <c>ViewData["OffcanvasFooter"]</c>
		/// </summary>
		public const string OffcanvasFooter = nameof(OffcanvasFooter);
	}
}

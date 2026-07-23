using System.ComponentModel;
using System.Reflection;

namespace Sienar.Extensions;

/// <summary>
/// Contains utilities that work on enums
/// </summary>
public static class EnumExtensions
{
	/// <summary>
	/// Gets the value of the <see cref="DescriptionAttribute"/> if defined, or else the name of the enum member as a string
	/// </summary>
	/// <param name="self">the enum field</param>
	/// <returns>the description if defined, else the stringified name of the enum member</returns>
	public static string GetDescription(this Enum self)
	{
		var stringified = self.ToString();
		var a = self
			.GetType()
			.GetField(stringified)
			?.GetCustomAttribute<DescriptionAttribute>();
		return a?.Description ?? stringified;
	}
}
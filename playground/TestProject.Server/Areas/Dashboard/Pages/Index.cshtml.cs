using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sienar.Views;

public class IndexModel : PageModel
{
	[BindProperty]
	[Required]
	[StringLength(12, MinimumLength = 6)]
	public string? Name { get; set; }
}

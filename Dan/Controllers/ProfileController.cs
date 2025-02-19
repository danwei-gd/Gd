using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dan;

public class ProfileController : Controller
{
	public async Task<IActionResult> Index()
	{
		return View();
	}
}

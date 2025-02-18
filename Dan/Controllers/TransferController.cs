using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dan;

public class TransferController : Controller
{
	public async Task<IActionResult> Create()
	{
		return View();
	}
}

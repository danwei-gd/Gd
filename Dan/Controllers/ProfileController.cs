using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Dan;

public class ProfileController : Controller
{
	public async Task<IActionResult> Index()
	{
		using var conn = new SqlConnection("Server=dv-gss-eus-gft-001-sql.database.windows.net; Database=GlobalFundTransfer;  Authentication=Active Directory Default; Encrypt=True; Column Encryption Setting=Enabled;Connect Timeout=30;");
		var model = await conn.QueryAsync(@"
				select 
				* 
				from GlobalFundTransferProfile(nolock) 
				order by GlobalFundTransferProfileKey 
				offset 10 rows fetch next 10 rows only
				", commandTimeout: 3600);
		return View(model);
	}
}

using CRUD_ADO.NET.Services;
using SearchableDropdown.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class UserController : Controller
{
    private readonly UserDAL _userDAL;

    public UserController(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("cs");
        _userDAL = new UserDAL(connectionString);
    }

    [HttpGet]
    public ActionResult ShowList()
    {
        var data = _userDAL.ShowList();
        ViewBag.List = new SelectList(data, "AccountClassId", "AccountClassTitle");
        return View();
    }

    [HttpPost]
    public IActionResult AddName(string newName)
    {
        if (!string.IsNullOrWhiteSpace(newName))
        {
            _userDAL.AddName(newName);
        }

        return RedirectToAction("ShowList");
    }
}

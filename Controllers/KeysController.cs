using Microsoft.AspNetCore.Mvc;
using Localizer.Models;

namespace Localizer.Controllers;

public class KeysController : Controller
{
    public IActionResult Create(int? appId)
    {
        return View("Form", new KeyFormViewModel { AppId = appId });
    }

    public IActionResult Edit(int id)
    {
        var key = MockData.Keys.FirstOrDefault(k => k.Id == id);
        if (key == null) return NotFound();
        return View("Form", new KeyFormViewModel { Existing = key, AppId = key.AppId });
    }

    public IActionResult Upload(int? appId, string? lang)
    {
        ViewData["AppId"] = appId;
        ViewData["Lang"] = lang;
        return View();
    }
}

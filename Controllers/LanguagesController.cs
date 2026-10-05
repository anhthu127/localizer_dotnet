using Microsoft.AspNetCore.Mvc;
using Localizer.Models;

namespace Localizer.Controllers;

public class LanguagesController : Controller
{
    public IActionResult Index()
    {
        return View(new LanguagesViewModel
        {
            Languages = MockData.Languages.Select(MockData.OverviewFor).ToList(),
            Comtors = MockData.Users.Where(u => u.Role == UserRole.Comtor && u.Status != UserStatus.Disabled).OrderBy(u => u.Name).ToList(),
        });
    }

    // Drawer content on the Languages list, loaded on demand.
    public IActionResult Panel(string id)
    {
        var lang = MockData.Languages.FirstOrDefault(l => l.Code == id);
        if (lang == null || lang.Code == MockData.SourceLanguage) return NotFound();

        var overview = MockData.OverviewFor(lang);
        var apps = overview.Apps
            .Select(a => MockData.ProgressFor(a, lang.Code))
            .OrderBy(p => p.Coverage).ThenBy(p => p.App.Name)
            .ToList();

        return PartialView("_LanguageDrawer", new LanguagePanelViewModel { Overview = overview, Apps = apps });
    }
}

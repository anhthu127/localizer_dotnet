using Microsoft.AspNetCore.Mvc;
using Localizer.Models;

namespace Localizer.Controllers;

public class ApplicationsController : Controller
{
    private const int PageSize = 25;

    public IActionResult Index()
    {
        return View(MockData.Apps.Select(MockData.StatsFor).ToList());
    }

    // Drawer content on the Applications list, loaded on demand.
    public IActionResult Panel(int id)
    {
        var app = MockData.App(id);
        if (app == null) return NotFound();
        return PartialView("_AppDrawer", new AppPanelViewModel(MockData.StatsFor(app), MockData.LanguageStatsFor([app])));
    }

    public IActionResult Details(int id, string? lang, string? module, string status = "all", string? q = null, int page = 1)
    {
        var app = MockData.App(id);
        if (app == null) return NotFound();

        // The grid always shows English next to one target language; default to the first one.
        var targetLangs = app.Languages.Where(l => l != MockData.SourceLanguage).ToList();
        if (lang == null || !targetLangs.Contains(lang)) lang = targetLangs.FirstOrDefault();
        if (module != null && !app.Modules.Contains(module)) module = null;

        var appKeys = MockData.Keys.Where(k => k.AppId == id).ToList();
        var modules = app.Modules.Select(m =>
        {
            var mk = appKeys.Where(k => k.Module == m).ToList();
            return new ModuleStat(m, mk.Count, mk.Count(k => k.IsNew), lang == null ? 0 : mk.Count(k => k.IsMissing(lang)));
        }).ToList();

        IEnumerable<TranslationKey> keys = appKeys;
        if (module != null) keys = keys.Where(k => k.Module == module);

        if (!string.IsNullOrWhiteSpace(q))
            keys = keys.Where(k => k.Key.Contains(q, StringComparison.OrdinalIgnoreCase)
                                   || k.Values.Any(v => (v.Key == MockData.SourceLanguage || v.Key == lang) && v.Value != null && v.Value.Contains(q, StringComparison.OrdinalIgnoreCase)));

        keys = status switch
        {
            "missing" => keys.Where(k => lang != null && k.IsMissing(lang)),
            "translated" => keys.Where(k => lang == null || !k.IsMissing(lang)),
            "new" => keys.Where(k => k.IsNew),
            _ => keys,
        };

        var filtered = keys.OrderByDescending(k => k.IsNew).ThenBy(k => k.Key).ToList();
        var pageCount = Math.Max(1, (int)Math.Ceiling(filtered.Count / (double)PageSize));
        page = Math.Clamp(page, 1, pageCount);

        return View(new AppDetailsViewModel
        {
            Stats = MockData.StatsFor(app),
            Keys = filtered.Skip((page - 1) * PageSize).Take(PageSize).ToList(),
            TotalMatches = filtered.Count,
            Page = page,
            PageCount = pageCount,
            Lang = lang,
            Status = status,
            Query = q,
            Module = module,
            Modules = modules,
            Languages = MockData.LanguageStatsFor([app]),
            History = MockData.PublishHistory.Where(p => p.AppId == id).ToList(),
        });
    }
}

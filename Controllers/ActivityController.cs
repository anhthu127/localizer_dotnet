using Microsoft.AspNetCore.Mvc;
using Localizer.Models;

namespace Localizer.Controllers;

public class ActivityController : Controller
{
    private const int PageSize = 40;

    /// <param name="days">Period to show: 1, 7 or 30 days (0 = everything).</param>
    public IActionResult Index(int? appId, string? lang, string type = "all", int days = 30, int page = 1)
    {
        if (lang != null && MockData.Languages.All(l => l.Code != lang)) lang = null;
        if (appId != null && MockData.App(appId.Value) == null) appId = null;

        // App / language / period filters apply to everything, including the per-type counts.
        IEnumerable<ActivityItem> scoped = MockData.Activities;
        if (appId != null) scoped = scoped.Where(a => a.AppId == appId);
        if (lang != null) scoped = scoped.Where(a => a.Lang == lang);
        if (days > 0) scoped = scoped.Where(a => a.When >= MockData.Now.AddDays(-days));
        var scopedList = scoped.ToList();

        var filtered = type == "all" ? scopedList : scopedList.Where(a => Ui.ActivityGroup(a.Kind) == type).ToList();
        var pageCount = Math.Max(1, (int)Math.Ceiling(filtered.Count / (double)PageSize));
        page = Math.Clamp(page, 1, pageCount);

        return View(new ActivityViewModel
        {
            Items = filtered.Skip((page - 1) * PageSize).Take(PageSize).ToList(),
            Total = filtered.Count,
            Page = page,
            PageCount = pageCount,
            AppId = appId,
            Lang = lang,
            Type = type,
            Days = days,
            TypeCounts = scopedList.GroupBy(a => Ui.ActivityGroup(a.Kind)).ToDictionary(g => g.Key, g => g.Count()),
            StringsTranslated = filtered.Where(a => a.Kind is "translate" or "edit").Sum(a => a.Count),
            Publishes = filtered.Count(a => a.Kind is "publish-test" or "publish-live"),
            Contributors = filtered.Select(a => a.User).Distinct().Count(),
        });
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Localizer.Models;

namespace Localizer.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var apps = MockData.Apps.Select(MockData.StatsFor).ToList();
        var today = MockData.Now.Date;
        var daily = Enumerable.Range(0, 14)
            .Select(i => today.AddDays(i - 13))
            .Select(day => new DailyCount(day, MockData.Keys.Count(k => k.CreatedAt.Date == day)))
            .ToList();

        var slots = apps.Sum(a => a.TotalKeys * (a.App.Languages.Count - 1));
        var missing = apps.Sum(a => a.Untranslated);

        return View(new DashboardViewModel
        {
            TotalKeys = apps.Sum(a => a.TotalKeys),
            NewKeys = apps.Sum(a => a.NewKeys),
            NewKeysPrevious = MockData.Keys.Count(k => k.CreatedAt < MockData.Now.AddDays(-7) && k.CreatedAt >= MockData.Now.AddDays(-14)),
            Untranslated = missing,
            Coverage = slots == 0 ? 100 : (slots - missing) * 100.0 / slots,
            Apps = apps,
            Languages = MockData.LanguageStatsFor(MockData.Apps).OrderByDescending(l => l.Missing).ToList(),
            DailyNewKeys = daily,
            LockedApps = MockData.Apps.Where(a => a.IsLocked || a.IsLockScheduled)
                .OrderBy(a => a.IsLockScheduled).ThenBy(a => a.LockedUntil ?? DateTime.MaxValue).ToList(),
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using Microsoft.AspNetCore.Mvc;
using Localizer.Models;

namespace Localizer.Controllers;

public class SearchController : Controller
{
    private const int MaxResults = 100;

    public IActionResult Index(string? q, string scope = "all", int? appId = null, string? lang = null)
    {
        var results = new List<SearchResult>();
        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            var searchKey = scope is "all" or "key";
            var searchText = scope is "all" or "text";

            foreach (var key in MockData.Keys.Where(k => appId == null || k.AppId == appId))
            {
                var keyMatched = searchKey && key.Key.Contains(q, StringComparison.OrdinalIgnoreCase);
                var matches = searchText
                    ? key.Values
                        .Where(v => (lang == null || v.Key == lang) && v.Value != null && v.Value.Contains(q, StringComparison.OrdinalIgnoreCase))
                        .Select(v => new SearchMatch(v.Key, v.Value!))
                        .ToList()
                    : [];

                if (!keyMatched && matches.Count == 0) continue;
                if (matches.Count == 0)
                    matches.Add(new SearchMatch(lang ?? MockData.SourceLanguage, key.Get(lang ?? MockData.SourceLanguage) ?? ""));

                results.Add(new SearchResult(key, MockData.App(key.AppId)!, keyMatched, matches));
            }
        }

        return View(new SearchViewModel
        {
            Query = q,
            Scope = scope,
            AppId = appId,
            Lang = lang,
            TotalMatches = results.Count,
            Results = results.Take(MaxResults).ToList(),
        });
    }
}

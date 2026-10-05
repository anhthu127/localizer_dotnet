using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace Localizer.Models;

/// <summary>Small formatting helpers shared by the views.</summary>
public static class Ui
{
    public static string Relative(DateTime when)
    {
        var span = MockData.Now - when;
        if (span.TotalMinutes < 1) return "just now";
        if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} min ago";
        if (span.TotalHours < 24) return $"{(int)span.TotalHours} hours ago".Replace("1 hours", "1 hour");
        if (span.TotalDays < 2) return "yesterday";
        if (span.TotalDays < 30) return $"{(int)span.TotalDays} days ago";
        var months = (int)(span.TotalDays / 30);
        return months == 1 ? "1 month ago" : $"{months} months ago";
    }

    public static string Exact(DateTime when) => when.ToString("dd MMM yyyy, HH:mm");

    public static string Day(DateTime when) => when.ToString("dd MMM yyyy");

    /// <summary>One line describing an app's lock: who, since when and until when (empty when open).</summary>
    public static string LockSummary(AppInfo app) => app switch
    {
        { IsLockScheduled: true } => $"Scheduled from {Day(app.LockedAt!.Value)} to {Day(app.LockedUntil!.Value)} by {app.LockedBy}",
        { IsLocked: true, LockedUntil: DateTime until } => $"Locked by {app.LockedBy} · until {Day(until)}",
        { IsLocked: true } => $"Locked by {app.LockedBy} · {Relative(app.LockedAt!.Value)} · until unlocked",
        _ => "",
    };

    public static string Initials(string name)
    {
        var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length == 1 ? parts[0][..1] : $"{parts[0][0]}{parts[^1][0]}".ToUpperInvariant();
    }

    public static string CoverageTone(double coverage) => coverage switch
    {
        >= 95 => "good",
        >= 80 => "warn",
        _ => "bad",
    };

    public static bool IsStale(DateTime lastTranslated) => (MockData.Now - lastTranslated).TotalDays > 14;

    /// <summary>HTML-encodes <paramref name="text"/> and wraps case-insensitive matches of <paramref name="query"/> in &lt;mark&gt;.</summary>
    /// <summary>Strips tags and collapses whitespace, for one-line previews of HTML content.</summary>
    public static string? PlainText(string? html) =>
        html == null ? null : System.Net.WebUtility.HtmlDecode(
            System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", " "), @"\s+", " ")).Trim();

    public static IHtmlContent Highlight(string? text, string? query)
    {
        if (string.IsNullOrEmpty(text)) return HtmlString.Empty;
        var enc = HtmlEncoder.Default;
        if (string.IsNullOrWhiteSpace(query)) return new HtmlString(enc.Encode(text));

        var sb = new System.Text.StringBuilder();
        var i = 0;
        while (true)
        {
            var hit = text.IndexOf(query, i, StringComparison.OrdinalIgnoreCase);
            if (hit < 0) break;
            sb.Append(enc.Encode(text[i..hit])).Append("<mark>").Append(enc.Encode(text.Substring(hit, query.Length))).Append("</mark>");
            i = hit + query.Length;
        }
        sb.Append(enc.Encode(text[i..]));
        return new HtmlString(sb.ToString());
    }

    public static string RoleClass(UserRole role) => role switch
    {
        UserRole.Developer => "role-dev",
        UserRole.QA => "role-qa",
        _ => "role-comtor",
    };

    // ---------- Activity feed ----------

    /// <summary>Filter groups shown on the Activity page, in display order.</summary>
    public static readonly (string Key, string Label)[] ActivityGroups =
        [("translation", "Translations"), ("upload", "Uploads"), ("keys", "Keys"), ("publish", "Publishing"), ("settings", "Settings")];

    public static string ActivityGroup(string kind) => kind switch
    {
        "translate" or "edit" => "translation",
        "upload" => "upload",
        "add" => "keys",
        "publish-test" or "publish-live" or "verify" => "publish",
        _ => "settings",
    };

    public static string ActivityIcon(string kind) => kind switch
    {
        "translate" => "translate",
        "edit" => "pencil",
        "upload" => "cloud-arrow-up",
        "add" => "plus-lg",
        "publish-test" => "send",
        "publish-live" => "rocket-takeoff",
        "verify" => "patch-check",
        "lock" => "lock-fill",
        "unlock" => "unlock",
        _ => "gear",
    };

    public static string DayLabel(DateTime day) =>
        day == MockData.Now.Date ? "Today"
        : day == MockData.Now.Date.AddDays(-1) ? "Yesterday"
        : day.ToString("dddd, dd MMM");
}

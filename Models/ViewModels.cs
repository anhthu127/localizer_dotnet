namespace Localizer.Models;

public record AppStats(AppInfo App, int TotalKeys, int NewKeys, int Untranslated, double Coverage, int PendingTest, int PendingLive, int UntranslatedKeys);

public record LanguageStat(Language Language, int Total, int Translated, int Missing, DateTime? LastUpdated)
{
    public double Coverage => Total == 0 ? 100 : Translated * 100.0 / Total;
}

public record DailyCount(DateTime Day, int Count);

public class DashboardViewModel
{
    public int TotalKeys { get; init; }
    public int NewKeys { get; init; }
    public int NewKeysPrevious { get; init; }
    public int Untranslated { get; init; }
    public double Coverage { get; init; }
    public List<AppStats> Apps { get; init; } = [];
    public List<LanguageStat> Languages { get; init; } = [];
    public List<DailyCount> DailyNewKeys { get; init; } = [];
    /// <summary>Apps that are locked now or have a lock scheduled; locked first, soonest to end first.</summary>
    public List<AppInfo> LockedApps { get; init; } = [];
}

/// <summary>Key counts of one module; Missing is for the language shown in the grid.</summary>
public record ModuleStat(string Name, int Total, int New, int Missing);

public class AppDetailsViewModel
{
    public AppStats Stats { get; init; } = null!;
    public AppInfo App => Stats.App;
    public List<TranslationKey> Keys { get; init; } = [];
    public int TotalMatches { get; init; }
    public int Page { get; init; }
    public int PageCount { get; init; }
    /// <summary>Language whose translations are shown next to English; null when the app has no target language.</summary>
    public string? Lang { get; init; }
    public string Status { get; init; } = "all";
    public string? Query { get; init; }
    public string? Module { get; init; }
    public List<ModuleStat> Modules { get; init; } = [];
    public bool HasFilters => !string.IsNullOrWhiteSpace(Query) || Status != "all";
    /// <summary>Keys are listed once a module is picked, or across all modules while searching / filtering.</summary>
    public bool ShowKeys => Module != null || HasFilters;
    public List<LanguageStat> Languages { get; init; } = [];
    public List<PublishRecord> History { get; init; } = [];
}

public class KeyFormViewModel
{
    public TranslationKey? Existing { get; init; }
    public int? AppId { get; init; }
    public bool IsEdit => Existing != null;
}

public record SearchMatch(string Lang, string Text);

public record SearchResult(TranslationKey Key, AppInfo App, bool KeyMatched, List<SearchMatch> Matches);

public class SearchViewModel
{
    public string? Query { get; init; }
    public string Scope { get; init; } = "all";
    public int? AppId { get; init; }
    public string? Lang { get; init; }
    public List<SearchResult> Results { get; init; } = [];
    public int TotalMatches { get; init; }
}

public class UsersViewModel
{
    public List<User> Users { get; init; } = [];
    public string? Role { get; init; }
    public string? Query { get; init; }
    public Dictionary<UserRole, int> RoleCounts { get; init; } = [];
    public int Total { get; init; }
}

public record PublishModalModel(AppStats Stats, string Environment);

public record AppPanelViewModel(AppStats Stats, List<LanguageStat> Languages);

public class ActivityViewModel
{
    public List<ActivityItem> Items { get; init; } = [];
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageCount { get; init; }
    public int? AppId { get; init; }
    public string? Lang { get; init; }
    public string Type { get; init; } = "all";
    public int Days { get; init; }
    public Dictionary<string, int> TypeCounts { get; init; } = [];
    public int StringsTranslated { get; init; }
    public int Publishes { get; init; }
    public int Contributors { get; init; }
    public bool HasFilters => AppId != null || Lang != null || Type != "all" || Days != 30;
}

/// <summary>Translation progress of one language inside one application.</summary>
public record AppLanguageProgress(AppInfo App, int Total, int Translated, DateTime? LastUpdated)
{
    public int Missing => Total - Translated;
    public double Coverage => Total == 0 ? 100 : Translated * 100.0 / Total;
}

public record LanguageOverview(Language Language, LanguageStat? Stat, List<AppInfo> Apps, List<User> Translators)
{
    public bool IsSource => Language.Code == MockData.SourceLanguage;
}

public class LanguagesViewModel
{
    public List<LanguageOverview> Languages { get; init; } = [];
    public List<User> Comtors { get; init; } = [];
}

public class LanguagePanelViewModel
{
    public LanguageOverview Overview { get; init; } = null!;
    public Language Language => Overview.Language;
    /// <summary>Progress of every app that uses the language, least translated first.</summary>
    public List<AppLanguageProgress> Apps { get; init; } = [];
}

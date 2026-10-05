namespace Localizer.Models;

public record Language(string Code, string Name, string NativeName, string Flag);

public record PublishInfo(string Version, DateTime PublishedAt, string PublishedBy);

public class AppInfo
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string Code { get; init; } = "";
    public string Platform { get; init; } = "";
    public string Description { get; init; } = "";
    public string Color { get; init; } = "#2563eb";
    public string Icon { get; init; } = "app";
    public List<string> Languages { get; init; } = [];
    public List<string> Modules { get; init; } = [];
    public DateTime LastTranslatedAt { get; init; }
    public string LastTranslatedBy { get; init; } = "";
    public PublishInfo Test { get; init; } = null!;
    public PublishInfo Live { get; init; } = null!;

    // A locked app is frozen: comtors can no longer edit or upload translations.
    // The lock starts at LockedAt and lasts until someone unlocks it, or – for a
    // scheduled lock – through the end of the LockedUntil day.
    public string? LockedBy { get; init; }
    public DateTime? LockedAt { get; init; }
    public DateTime? LockedUntil { get; init; }

    public bool IsLockedAt(DateTime when) =>
        LockedAt is DateTime from && from <= when && (LockedUntil is not DateTime until || when < until.Date.AddDays(1));
    public bool IsLocked => IsLockedAt(MockData.Now);
    public bool IsLockScheduled => LockedAt > MockData.Now;

    // Mock-data generation knobs
    public int NewKeyCount { get; init; }
    public double MissingRate { get; init; }
}

/// <summary>Label: short UI text (the vast majority). LongText: terms, policies, emails… often containing HTML.</summary>
public enum KeyFormat { Label, LongText }

public class TranslationKey
{
    public int Id { get; init; }
    public int AppId { get; init; }
    public string Key { get; init; } = "";
    public string Module { get; init; } = "";
    public string Description { get; init; } = "";
    public KeyFormat Format { get; init; }
    public DateTime CreatedAt { get; init; }
    public string CreatedBy { get; init; } = "";
    public DateTime UpdatedAt { get; init; }
    public string UpdatedBy { get; init; } = "";
    public Dictionary<string, string?> Values { get; init; } = [];

    public bool IsNew => CreatedAt >= MockData.Now.AddDays(-7);
    public string? Get(string lang) => Values.TryGetValue(lang, out var v) ? v : null;
    public bool IsMissing(string lang) => string.IsNullOrEmpty(Get(lang));
}

public enum UserRole { Developer, QA, Comtor }

public enum UserStatus { Active, Invited, Disabled }

public class User
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string Email { get; init; } = "";
    public UserRole Role { get; init; }
    public UserStatus Status { get; init; }
    public List<string> Languages { get; init; } = [];
    public List<int> AppIds { get; init; } = [];
    public DateTime LastActive { get; init; }
}

/// <param name="Kind">translate, edit, upload, add, publish-test, publish-live, verify, lock, unlock, settings</param>
/// <param name="Lang">Language the activity touched, if any (used by the language filter).</param>
public record ActivityItem(DateTime When, string User, string Kind, string Text, int? AppId, string? Lang = null, int Count = 0);

public record PublishRecord(int AppId, string Environment, string Version, DateTime At, string By, int Changes, bool Success, string Note);

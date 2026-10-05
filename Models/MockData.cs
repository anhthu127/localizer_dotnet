namespace Localizer.Models;

/// <summary>
/// In-memory sample data so the UI can be built and reviewed without a backend.
/// Generated deterministically (fixed seed) relative to app start time.
/// </summary>
public static class MockData
{
    public static readonly DateTime Now = DateTime.Now;

    public static IReadOnlyList<Language> Languages { get; } =
    [
        new("en", "English", "English", "🇬🇧"),
        new("vi", "Vietnamese", "Tiếng Việt", "🇻🇳"),
        new("ja", "Japanese", "日本語", "🇯🇵"),
        new("ko", "Korean", "한국어", "🇰🇷"),
        new("zh-CN", "Chinese (Simplified)", "简体中文", "🇨🇳"),
        new("th", "Thai", "ไทย", "🇹🇭"),
        new("fr", "French", "Français", "🇫🇷"),
        new("de", "German", "Deutsch", "🇩🇪"),
        new("es", "Spanish", "Español", "🇪🇸"),
        new("id", "Indonesian", "Bahasa Indonesia", "🇮🇩"),
    ];

    public const string SourceLanguage = "en";

    public static Language Lang(string code) => Languages.First(l => l.Code == code);

    // key, description, then values in the same order as Languages
    private static readonly string[][] Catalog =
    [
        ["button.save", "Primary save button", "Save", "Lưu", "保存", "저장", "保存", "บันทึก", "Enregistrer", "Speichern", "Guardar", "Simpan"],
        ["button.cancel", "Dismiss / cancel button", "Cancel", "Hủy", "キャンセル", "취소", "取消", "ยกเลิก", "Annuler", "Abbrechen", "Cancelar", "Batal"],
        ["button.confirm", "Confirm action button", "Confirm", "Xác nhận", "確認", "확인", "确认", "ยืนยัน", "Confirmer", "Bestätigen", "Confirmar", "Konfirmasi"],
        ["button.continue", "Go to next step", "Continue", "Tiếp tục", "続ける", "계속", "继续", "ดำเนินการต่อ", "Continuer", "Weiter", "Continuar", "Lanjutkan"],
        ["button.back", "Back navigation", "Back", "Quay lại", "戻る", "뒤로", "返回", "ย้อนกลับ", "Retour", "Zurück", "Atrás", "Kembali"],
        ["button.retry", "Retry after an error", "Try again", "Thử lại", "再試行", "다시 시도", "重试", "ลองอีกครั้ง", "Réessayer", "Erneut versuchen", "Reintentar", "Coba lagi"],
        ["title.welcome", "Greeting on the first screen", "Welcome back!", "Chào mừng trở lại!", "おかえりなさい！", "다시 오신 것을 환영합니다!", "欢迎回来！", "ยินดีต้อนรับกลับ!", "Bon retour !", "Willkommen zurück!", "¡Bienvenido de nuevo!", "Selamat datang kembali!"],
        ["title.settings", "Screen title", "Settings", "Cài đặt", "設定", "설정", "设置", "การตั้งค่า", "Paramètres", "Einstellungen", "Configuración", "Pengaturan"],
        ["title.notifications", "Screen title", "Notifications", "Thông báo", "通知", "알림", "通知", "การแจ้งเตือน", "Notifications", "Benachrichtigungen", "Notificaciones", "Notifikasi"],
        ["title.profile", "Screen title", "My profile", "Hồ sơ của tôi", "マイプロフィール", "내 프로필", "我的资料", "โปรไฟล์ของฉัน", "Mon profil", "Mein Profil", "Mi perfil", "Profil saya"],
        ["label.email", "Form field label", "Email address", "Địa chỉ email", "メールアドレス", "이메일 주소", "电子邮件地址", "ที่อยู่อีเมล", "Adresse e-mail", "E-Mail-Adresse", "Correo electrónico", "Alamat email"],
        ["label.password", "Form field label", "Password", "Mật khẩu", "パスワード", "비밀번호", "密码", "รหัสผ่าน", "Mot de passe", "Passwort", "Contraseña", "Kata sandi"],
        ["label.phone", "Form field label", "Phone number", "Số điện thoại", "電話番号", "전화번호", "电话号码", "หมายเลขโทรศัพท์", "Numéro de téléphone", "Telefonnummer", "Número de teléfono", "Nomor telepon"],
        ["label.search", "Search box placeholder", "Search…", "Tìm kiếm…", "検索…", "검색…", "搜索…", "ค้นหา…", "Rechercher…", "Suchen…", "Buscar…", "Cari…"],
        ["label.language", "Language picker label", "Language", "Ngôn ngữ", "言語", "언어", "语言", "ภาษา", "Langue", "Sprache", "Idioma", "Bahasa"],
        ["message.saved", "Toast after saving", "Your changes have been saved.", "Thay đổi của bạn đã được lưu.", "変更が保存されました。", "변경 사항이 저장되었습니다.", "您的更改已保存。", "บันทึกการเปลี่ยนแปลงแล้ว", "Vos modifications ont été enregistrées.", "Ihre Änderungen wurden gespeichert.", "Tus cambios se han guardado.", "Perubahan Anda telah disimpan."],
        ["message.network_error", "Shown when offline", "No internet connection. Please check your network.", "Không có kết nối internet. Vui lòng kiểm tra mạng.", "インターネットに接続されていません。ネットワークを確認してください。", "인터넷에 연결되어 있지 않습니다. 네트워크를 확인하세요.", "无网络连接，请检查您的网络。", "ไม่มีการเชื่อมต่ออินเทอร์เน็ต โปรดตรวจสอบเครือข่ายของคุณ", "Pas de connexion Internet. Vérifiez votre réseau.", "Keine Internetverbindung. Bitte prüfe dein Netzwerk.", "Sin conexión a internet. Revisa tu red.", "Tidak ada koneksi internet. Periksa jaringan Anda."],
        ["message.empty", "Empty list state", "Nothing here yet", "Chưa có gì ở đây", "まだ何もありません", "아직 아무것도 없습니다", "这里还没有内容", "ยังไม่มีอะไรที่นี่", "Rien pour le moment", "Noch nichts vorhanden", "Aún no hay nada aquí", "Belum ada apa-apa"],
        ["message.session_expired", "Shown when the token expires", "Your session has expired. Please sign in again.", "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.", "セッションの有効期限が切れました。再度ログインしてください。", "세션이 만료되었습니다. 다시 로그인하세요.", "您的会话已过期，请重新登录。", "เซสชันของคุณหมดอายุแล้ว โปรดเข้าสู่ระบบอีกครั้ง", "Votre session a expiré. Veuillez vous reconnecter.", "Deine Sitzung ist abgelaufen. Bitte melde dich erneut an.", "Tu sesión ha caducado. Inicia sesión de nuevo.", "Sesi Anda telah berakhir. Silakan masuk lagi."],
        ["action.sign_in", "Sign-in button", "Sign in", "Đăng nhập", "ログイン", "로그인", "登录", "เข้าสู่ระบบ", "Se connecter", "Anmelden", "Iniciar sesión", "Masuk"],
        ["action.sign_out", "Sign-out menu item", "Sign out", "Đăng xuất", "ログアウト", "로그아웃", "退出登录", "ออกจากระบบ", "Se déconnecter", "Abmelden", "Cerrar sesión", "Keluar"],
        ["action.forgot_password", "Link under the password field", "Forgot password?", "Quên mật khẩu?", "パスワードをお忘れですか？", "비밀번호를 잊으셨나요?", "忘记密码？", "ลืมรหัสผ่าน?", "Mot de passe oublié ?", "Passwort vergessen?", "¿Olvidaste tu contraseña?", "Lupa kata sandi?"],
        ["action.share", "Share action", "Share", "Chia sẻ", "共有", "공유", "分享", "แชร์", "Partager", "Teilen", "Compartir", "Bagikan"],
        ["action.delete", "Destructive action", "Delete", "Xóa", "削除", "삭제", "删除", "ลบ", "Supprimer", "Löschen", "Eliminar", "Hapus"],
        ["action.view_all", "Link to the full list", "View all", "Xem tất cả", "すべて表示", "모두 보기", "查看全部", "ดูทั้งหมด", "Tout voir", "Alle anzeigen", "Ver todo", "Lihat semua"],
        ["status.pending", "Status badge", "Pending", "Đang chờ", "保留中", "대기 중", "待处理", "รอดำเนินการ", "En attente", "Ausstehend", "Pendiente", "Tertunda"],
        ["status.completed", "Status badge", "Completed", "Hoàn tất", "完了", "완료", "已完成", "เสร็จสมบูรณ์", "Terminé", "Abgeschlossen", "Completado", "Selesai"],
        ["status.failed", "Status badge", "Failed", "Thất bại", "失敗", "실패", "失败", "ล้มเหลว", "Échec", "Fehlgeschlagen", "Fallido", "Gagal"],
        ["hint.otp", "Helper text on the OTP screen", "Enter the 6-digit code sent to your phone", "Nhập mã 6 số đã gửi đến điện thoại của bạn", "携帯電話に送信された6桁のコードを入力してください", "휴대폰으로 전송된 6자리 코드를 입력하세요", "请输入发送到您手机的6位验证码", "กรอกรหัส 6 หลักที่ส่งไปยังโทรศัพท์ของคุณ", "Saisissez le code à 6 chiffres envoyé sur votre téléphone", "Gib den 6-stelligen Code ein, der an dein Handy gesendet wurde", "Introduce el código de 6 dígitos enviado a tu teléfono", "Masukkan kode 6 digit yang dikirim ke ponsel Anda"],
        ["hint.items_count", "Plural-aware item counter", "{count} items", "{count} mục", "{count} 件", "{count}개 항목", "{count} 项", "{count} รายการ", "{count} éléments", "{count} Elemente", "{count} elementos", "{count} item"],
    ];

    private static readonly string[] AllLangs = ["en", "vi", "ja", "ko", "zh-CN", "th", "fr", "de", "es", "id"];

    private static readonly string[] People =
        ["Logan Le", "Mai Nguyen", "Kenji Sato", "Ji-woo Park", "Linh Tran", "Somchai K.", "Claire Martin", "Jonas Weber", "Lucia Gomez", "Putri Ayu", "Huy Pham", "Anh Vo"];

    public static IReadOnlyList<AppInfo> Apps { get; } =
    [
        new() { Id = 1, Name = "School site", Code = "school-site", Platform = "Web", Icon = "building", Color = "#2563eb",
            Description = "School administration portal for classes, students and staff.", Languages = [..AllLangs],
            Modules = ["auth", "dashboard", "classes", "students", "attendance", "settings"], NewKeyCount = 14, MissingRate = 0.04,
            LastTranslatedAt = Now.AddHours(-2), LastTranslatedBy = "Mai Nguyen",
            Test = new("3.12.0", Now.AddHours(-20), "Logan Le"), Live = new("3.11.2", Now.AddDays(-6), "Huy Pham") },
        new() { Id = 2, Name = "Parent site", Code = "parent-site", Platform = "Web", Icon = "people", Color = "#ea580c",
            Description = "Parent portal to follow children's progress, messages and fees.", Languages = ["en", "vi", "th", "id", "ja", "ko"],
            Modules = ["auth", "home", "children", "messages", "payments", "profile"], NewKeyCount = 9, MissingRate = 0.08,
            LastTranslatedAt = Now.AddDays(-1).AddHours(-3), LastTranslatedBy = "Somchai K.",
            Test = new("2.8.0", Now.AddDays(-2), "Anh Vo"), Live = new("2.7.4", Now.AddDays(-9), "Huy Pham") },
        new() { Id = 3, Name = "Report site", Code = "report-site", Platform = "Web", Icon = "bar-chart-line", Color = "#0891b2",
            Description = "Academic and operational reports for schools and districts.", Languages = [..AllLangs],
            Modules = ["auth", "dashboard", "reports", "filters", "export", "settings"], NewKeyCount = 21, MissingRate = 0.12,
            LastTranslatedAt = Now.AddDays(-3), LastTranslatedBy = "Claire Martin",
            LockedBy = "Logan Le", LockedAt = Now.Date.AddDays(4), LockedUntil = Now.Date.AddDays(10),
            Test = new("5.2.1", Now.AddDays(-1), "Logan Le"), Live = new("5.2.0", Now.AddDays(-12), "Linh Tran") },
        new() { Id = 4, Name = "Content site", Code = "content-site", Platform = "Web", Icon = "collection-play", Color = "#db2777",
            Description = "Authoring tool for lessons, videos and learning materials.", Languages = ["en", "vi", "ja", "fr", "de", "es"],
            Modules = ["auth", "library", "editor", "media", "publish"], NewKeyCount = 5, MissingRate = 0.03,
            LastTranslatedAt = Now.AddDays(-6), LastTranslatedBy = "Kenji Sato",
            Test = new("1.9.3", Now.AddDays(-5), "Anh Vo"), Live = new("1.9.3", Now.AddDays(-4), "Linh Tran") },
        new() { Id = 5, Name = "Curriculum site", Code = "curriculum-site", Platform = "Web", Icon = "journal-bookmark", Color = "#7c3aed",
            Description = "Curriculum planning: courses, lessons and assessments by grade.", Languages = ["en", "vi", "ko", "zh-CN", "th", "id"],
            Modules = ["auth", "courses", "lessons", "assessments", "settings", "onboarding"], NewKeyCount = 0, MissingRate = 0.2,
            LastTranslatedAt = Now.AddDays(-23), LastTranslatedBy = "Ji-woo Park",
            LockedBy = "Huy Pham", LockedAt = Now.AddDays(-20),
            Test = new("4.0.0-rc2", Now.AddDays(-15), "Logan Le"), Live = new("3.9.8", Now.AddDays(-30), "Huy Pham") },
        new() { Id = 6, Name = "Notification site", Code = "notification-site", Platform = "Web", Icon = "bell", Color = "#16a34a",
            Description = "Compose and schedule announcements, push and email notifications.", Languages = ["en", "vi", "th"],
            Modules = ["auth", "campaigns", "templates", "schedule", "settings"], NewKeyCount = 6, MissingRate = 0.06,
            LastTranslatedAt = Now.AddDays(-2).AddHours(-5), LastTranslatedBy = "Somchai K.",
            Test = new("7.3.0", Now.AddDays(-1).AddHours(-4), "Anh Vo"), Live = new("7.2.5", Now.AddDays(-8), "Linh Tran") },
        new() { Id = 7, Name = "Connect site", Code = "connect-site", Platform = "Web", Icon = "chat-dots", Color = "#0d9488",
            Description = "Messaging between teachers, parents and students.", Languages = [..AllLangs],
            Modules = ["auth", "home", "chat", "groups", "calls", "search", "profile"], NewKeyCount = 17, MissingRate = 0.1,
            LastTranslatedAt = Now.AddHours(-7), LastTranslatedBy = "Lucia Gomez",
            Test = new("10.4.0", Now.AddHours(-30), "Logan Le"), Live = new("10.3.1", Now.AddDays(-5), "Huy Pham") },
        new() { Id = 8, Name = "Student app", Code = "student-app", Platform = "iOS · Android", Icon = "backpack", Color = "#1d4ed8",
            Description = "Mobile app for students: timetable, homework and grades.", Languages = ["en", "vi", "ja", "ko", "zh-CN"],
            Modules = ["auth", "home", "timetable", "homework", "grades", "profile"], NewKeyCount = 3, MissingRate = 0.02,
            LastTranslatedAt = Now.AddDays(-9), LastTranslatedBy = "Linh Tran",
            LockedBy = "Linh Tran", LockedAt = Now.AddDays(-2), LockedUntil = Now.Date.AddDays(5),
            Test = new("6.1.2", Now.AddDays(-7), "Anh Vo"), Live = new("6.1.1", Now.AddDays(-14), "Linh Tran") },
        new() { Id = 9, Name = "Little App", Code = "little-app", Platform = "iOS · Android", Icon = "puzzle", Color = "#ca8a04",
            Description = "Learning games and stories for kindergarten kids.", Languages = ["en", "vi", "ja", "ko", "fr", "de", "es"],
            Modules = ["onboarding", "home", "games", "stories", "settings"], NewKeyCount = 4, MissingRate = 0.15,
            LastTranslatedAt = Now.AddDays(-41), LastTranslatedBy = "Jonas Weber",
            Test = new("2.2.0", Now.AddDays(-3), "Logan Le"), Live = new("2.1.0", Now.AddDays(-45), "Huy Pham") },
        new() { Id = 10, Name = "Nexus app", Code = "nexus-app", Platform = "iOS · Android", Icon = "hexagon", Color = "#4f46e5",
            Description = "All-in-one school community app with feed and chat.", Languages = [..AllLangs],
            Modules = ["auth", "home", "feed", "chat", "settings", "profile"], NewKeyCount = 11, MissingRate = 0.07,
            LastTranslatedAt = Now.AddDays(-4), LastTranslatedBy = "Mai Nguyen",
            Test = new("1.30.0", Now.AddDays(-2), "Anh Vo"), Live = new("1.29.3", Now.AddDays(-7), "Linh Tran") },
        new() { Id = 11, Name = "Baby app", Code = "baby-app", Platform = "iOS · Android", Icon = "balloon-heart", Color = "#e11d48",
            Description = "Daily care journal for nursery: meals, naps and milestones.", Languages = ["en", "vi", "th", "id"],
            Modules = ["onboarding", "home", "tracker", "journal", "settings"], NewKeyCount = 8, MissingRate = 0.09,
            LastTranslatedAt = Now.AddDays(-1).AddHours(-8), LastTranslatedBy = "Thu Ha",
            Test = new("1.4.0", Now.AddDays(-1).AddHours(-2), "Anh Vo"), Live = new("1.3.2", Now.AddDays(-10), "Linh Tran") },
        new() { Id = 12, Name = "Nexus landing page", Code = "nexus-landing", Platform = "Web", Icon = "window", Color = "#9333ea",
            Description = "Public marketing site for the Nexus app.", Languages = [..AllLangs],
            Modules = ["home", "features", "pricing", "signup", "footer"], NewKeyCount = 7, MissingRate = 0.05,
            LastTranslatedAt = Now.AddHours(-11), LastTranslatedBy = "Claire Martin",
            Test = new("2.5.0", Now.AddDays(-1).AddHours(-10), "Logan Le"), Live = new("2.4.1", Now.AddDays(-3), "Huy Pham") },
        new() { Id = 13, Name = "Training site", Code = "training-site", Platform = "Web", Icon = "easel", Color = "#65a30d",
            Description = "Teacher training courses, quizzes and certificates.", Languages = ["en", "vi", "ja", "ko"],
            Modules = ["auth", "courses", "quizzes", "certificates", "profile"], NewKeyCount = 2, MissingRate = 0.11,
            LastTranslatedAt = Now.AddDays(-15), LastTranslatedBy = "Kenji Sato",
            Test = new("1.2.0", Now.AddDays(-4), "Anh Vo"), Live = new("1.1.5", Now.AddDays(-18), "Huy Pham") },
    ];

    public static AppInfo? App(int id) => Apps.FirstOrDefault(a => a.Id == id);

    public static IReadOnlyList<TranslationKey> Keys { get; } = GenerateKeys();

    private static List<TranslationKey> GenerateKeys()
    {
        var rnd = new Random(42);
        var keys = new List<TranslationKey>();
        var id = 1;

        foreach (var app in Apps)
        {
            var entries = new List<(string Module, string Key, string[] Entry)>();
            foreach (var module in app.Modules)
            foreach (var entry in Catalog)
                if (rnd.NextDouble() < 0.8)
                    entries.Add((module, $"{module}.{entry[0]}", entry));

            for (var i = 0; i < entries.Count; i++)
            {
                var (module, key, entry) = entries[i];
                var isNew = i >= entries.Count - app.NewKeyCount;
                var created = isNew
                    ? Now.AddHours(-rnd.Next(1, 7 * 24))
                    : rnd.NextDouble() < 0.06 ? Now.AddDays(-rnd.Next(8, 14)).AddHours(-rnd.Next(0, 23)) : Now.AddDays(-rnd.Next(15, 300));

                var values = new Dictionary<string, string?>();
                foreach (var lang in app.Languages)
                {
                    var text = entry[2 + Array.IndexOf(AllLangs, lang)];
                    var missing = lang != SourceLanguage && rnd.NextDouble() < (isNew ? 0.55 : app.MissingRate);
                    values[lang] = missing ? null : text;
                }

                var updated = created.AddHours(rnd.Next(0, (int)Math.Max(1, (Now - created).TotalHours)));
                keys.Add(new TranslationKey
                {
                    Id = id++, AppId = app.Id, Key = key, Module = module, Description = entry[1], Values = values,
                    CreatedAt = created, CreatedBy = rnd.NextDouble() < 0.6 ? "Logan Le" : "Anh Vo",
                    UpdatedAt = updated, UpdatedBy = People[rnd.Next(People.Length)],
                });
            }
        }

        // One long-text key (HTML) so the long editor can be reviewed. Added after the generated
        // keys so it doesn't shift the random sequence above.
        keys.Add(new TranslationKey
        {
            Id = id, AppId = 12, Key = "footer.legal.terms_of_use", Module = "footer", Format = KeyFormat.LongText,
            Description = "Terms of use page, rendered as HTML. Keep the tags and links unchanged.",
            Values = new() { ["en"] = TermsEn, ["vi"] = TermsVi },
            CreatedAt = Now.AddDays(-40), CreatedBy = "Logan Le", UpdatedAt = Now.AddDays(-2), UpdatedBy = "Thu Ha",
        });

        return keys;
    }

    private const string TermsEn = """
        <h2>Terms of Use</h2>
        <p>Welcome to Nexus. By creating an account or using the app you agree to these terms.</p>
        <h3>1. Your account</h3>
        <p>You are responsible for keeping your password safe and for all activity under your account.</p>
        <h3>2. Acceptable use</h3>
        <ul>
          <li>Do not share content that is unlawful, harmful or infringes the rights of others.</li>
          <li>Do not try to access other users' data or disrupt the service.</li>
        </ul>
        <h3>3. Privacy</h3>
        <p>We process personal data as described in our <a href="https://nexus.example/privacy">Privacy Policy</a>.</p>
        <p>Questions? Contact <a href="mailto:support@nexus.example">support@nexus.example</a>.</p>
        """;

    private const string TermsVi = """
        <h2>Điều khoản sử dụng</h2>
        <p>Chào mừng bạn đến với Nexus. Khi tạo tài khoản hoặc sử dụng ứng dụng, bạn đồng ý với các điều khoản này.</p>
        <h3>1. Tài khoản của bạn</h3>
        <p>Bạn có trách nhiệm giữ an toàn mật khẩu và chịu trách nhiệm về mọi hoạt động trong tài khoản của mình.</p>
        <h3>2. Sử dụng hợp lệ</h3>
        <ul>
          <li>Không chia sẻ nội dung vi phạm pháp luật, gây hại hoặc xâm phạm quyền của người khác.</li>
          <li>Không cố truy cập dữ liệu của người dùng khác hoặc làm gián đoạn dịch vụ.</li>
        </ul>
        <h3>3. Quyền riêng tư</h3>
        <p>Chúng tôi xử lý dữ liệu cá nhân như mô tả trong <a href="https://nexus.example/privacy">Chính sách quyền riêng tư</a>.</p>
        <p>Có câu hỏi? Liên hệ <a href="mailto:support@nexus.example">support@nexus.example</a>.</p>
        """;

    public static IReadOnlyList<User> Users { get; } =
    [
        new() { Id = 1, Name = "Logan Le", Email = "logan.le@localizer.dev", Role = UserRole.Developer, Status = UserStatus.Active, AppIds = [1, 3, 5, 7, 9, 12], LastActive = Now.AddMinutes(-3) },
        new() { Id = 2, Name = "Anh Vo", Email = "anh.vo@localizer.dev", Role = UserRole.Developer, Status = UserStatus.Active, AppIds = [2, 4, 6, 8, 10, 11, 13], LastActive = Now.AddHours(-1) },
        new() { Id = 3, Name = "Duc Hoang", Email = "duc.hoang@localizer.dev", Role = UserRole.Developer, Status = UserStatus.Active, AppIds = [1, 2], LastActive = Now.AddDays(-2) },
        new() { Id = 4, Name = "Emily Chen", Email = "emily.chen@localizer.dev", Role = UserRole.Developer, Status = UserStatus.Invited, AppIds = [7], LastActive = Now.AddDays(-1) },
        new() { Id = 5, Name = "Huy Pham", Email = "huy.pham@localizer.dev", Role = UserRole.QA, Status = UserStatus.Active, AppIds = [1, 2, 3, 5, 7, 9, 12, 13], LastActive = Now.AddHours(-5) },
        new() { Id = 6, Name = "Linh Tran", Email = "linh.tran@localizer.dev", Role = UserRole.QA, Status = UserStatus.Active, AppIds = [3, 4, 6, 8, 10, 11], LastActive = Now.AddHours(-9) },
        new() { Id = 7, Name = "Tuan Bui", Email = "tuan.bui@localizer.dev", Role = UserRole.QA, Status = UserStatus.Disabled, AppIds = [5], LastActive = Now.AddDays(-64) },
        new() { Id = 8, Name = "Mai Nguyen", Email = "mai.nguyen@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Active, Languages = ["vi", "ja"], AppIds = [1, 2, 3, 7, 10, 12], LastActive = Now.AddHours(-2) },
        new() { Id = 17, Name = "Thu Ha", Email = "thu.ha@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Active, Languages = ["vi"], AppIds = [2, 4, 6, 8, 9, 11], LastActive = Now.AddHours(-3) },
        new() { Id = 18, Name = "Chuong Tran", Email = "chuong.tran@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Active, Languages = ["vi"], AppIds = [3, 5, 10, 13], LastActive = Now.AddHours(-6) },
        new() { Id = 9, Name = "Kenji Sato", Email = "kenji.sato@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Active, Languages = ["ja"], AppIds = [1, 3, 4, 8, 9, 12, 13], LastActive = Now.AddDays(-1) },
        new() { Id = 10, Name = "Ji-woo Park", Email = "jiwoo.park@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Active, Languages = ["ko"], AppIds = [1, 2, 5, 8, 9, 13], LastActive = Now.AddDays(-3) },
        new() { Id = 11, Name = "Wei Zhang", Email = "wei.zhang@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Active, Languages = ["zh-CN"], AppIds = [1, 3, 5, 8, 10, 12], LastActive = Now.AddDays(-2) },
        new() { Id = 12, Name = "Somchai K.", Email = "somchai.k@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Active, Languages = ["th"], AppIds = [2, 5, 6, 7, 11], LastActive = Now.AddDays(-1).AddHours(-3) },
        new() { Id = 13, Name = "Claire Martin", Email = "claire.martin@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Active, Languages = ["fr"], AppIds = [3, 4, 7, 9, 10, 12], LastActive = Now.AddDays(-3) },
        new() { Id = 14, Name = "Jonas Weber", Email = "jonas.weber@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Active, Languages = ["de"], AppIds = [3, 4, 7, 9], LastActive = Now.AddDays(-12) },
        new() { Id = 15, Name = "Lucia Gomez", Email = "lucia.gomez@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Active, Languages = ["es"], AppIds = [3, 4, 7, 9, 10, 12], LastActive = Now.AddHours(-7) },
        new() { Id = 16, Name = "Putri Ayu", Email = "putri.ayu@localizer.dev", Role = UserRole.Comtor, Status = UserStatus.Invited, Languages = ["id"], AppIds = [2, 5, 7, 11], LastActive = Now.AddDays(-2) },
    ];

    public static IReadOnlyList<ActivityItem> Activities { get; } = GenerateActivities();

    private static List<ActivityItem> GenerateActivities()
    {
        // Hand-written recent events that match the app data (versions, locks, language changes)…
        var list = new List<ActivityItem>
        {
            new(Now.AddHours(-2), "Mai Nguyen", "translate", "translated 24 keys to Vietnamese", 1, "vi", 24),
            new(Now.AddHours(-4), "Logan Le", "add", "added 6 new keys", 7, null, 6),
            new(Now.AddHours(-7), "Lucia Gomez", "upload", "uploaded es.json (182 keys)", 7, "es", 182),
            new(Now.AddHours(-20), "Logan Le", "publish-test", $"published v{Apps[0].Test.Version} to TEST", 1, null, 18),
            new(Now.AddDays(-1).AddHours(-3), "Somchai K.", "translate", "translated 31 keys to Thai", 2, "th", 31),
            new(Now.AddDays(-1).AddHours(-6), "Huy Pham", "verify", $"verified TEST build v{Apps[6].Test.Version}", 7),
            new(Now.AddDays(-2), "Anh Vo", "publish-test", $"published v{Apps[1].Test.Version} to TEST", 2, null, 22),
            new(Now.AddDays(-2).AddHours(-1), "Linh Tran", "lock", $"locked the application until {Ui.Day(Apps[7].LockedUntil!.Value)} for the release freeze", 8),
            new(Now.AddDays(-3), "Claire Martin", "upload", "uploaded fr.json (240 keys)", 3, "fr", 240),
            new(Now.AddDays(-3).AddHours(-2), "Logan Le", "lock", $"scheduled a lock from {Ui.Day(Apps[2].LockedAt!.Value)} to {Ui.Day(Apps[2].LockedUntil!.Value)}", 3),
            new(Now.AddDays(-4), "Linh Tran", "publish-live", $"published v{Apps[3].Live.Version} to LIVE", 4, null, 35),
            new(Now.AddDays(-5).AddHours(-2), "Anh Vo", "settings", "removed Indonesian from the languages to translate", 6, "id"),
            new(Now.AddDays(-20), "Huy Pham", "lock", "locked the application", 5),
            new(Now.AddHours(-11), "Claire Martin", "translate", "translated 12 keys to French", 12, "fr", 12),
            new(Now.AddDays(-1).AddHours(-2), "Anh Vo", "publish-test", $"published v{Apps[10].Test.Version} to TEST", 11, null, 15),
            new(Now.AddDays(-1).AddHours(-8), "Thu Ha", "translate", "translated 19 keys to Vietnamese", 11, "vi", 19),
            new(Now.AddDays(-3), "Huy Pham", "publish-live", $"published v{Apps[11].Live.Version} to LIVE", 12, null, 28),
        };

        // …plus generated background activity for the last 30 days.
        var rnd = new Random(11);
        var comtors = Users.Where(u => u.Role == UserRole.Comtor).ToList();
        var devs = Users.Where(u => u.Role == UserRole.Developer && u.Status == UserStatus.Active).ToList();
        var qas = Users.Where(u => u.Role == UserRole.QA && u.Status == UserStatus.Active).ToList();
        T Pick<T>(IList<T> items) => items[rnd.Next(items.Count)];

        for (var i = 0; i < 220; i++)
        {
            var when = Now.AddMinutes(-rnd.Next(60, 30 * 24 * 60));
            var roll = rnd.NextDouble();
            if (roll < 0.62)
            {
                var c = Pick(comtors);
                var options = c.AppIds.Select(id => App(id)!)
                    .Where(a => !a.IsLockedAt(when))
                    .SelectMany(a => c.Languages.Where(a.Languages.Contains).Select(l => (a, l))).ToList();
                if (options.Count == 0) continue;
                var (app, lang) = Pick(options);
                var n = rnd.Next(2, 45);
                var langName = Lang(lang).Name;
                list.Add(rnd.NextDouble() switch
                {
                    < 0.65 => new(when, c.Name, "translate", $"translated {n} keys to {langName}", app.Id, lang, n),
                    < 0.85 => new(when, c.Name, "edit", $"revised {n} {langName} translations", app.Id, lang, n),
                    _ => new(when, c.Name, "upload", $"uploaded {lang}.json ({n * 4} keys)", app.Id, lang, n * 4),
                });
            }
            else if (roll < 0.82)
            {
                var d = Pick(devs);
                var app = App(Pick(d.AppIds))!;
                var n = rnd.Next(1, 14);
                list.Add(rnd.NextDouble() < 0.7
                    ? new(when, d.Name, "add", $"added {n} new {(n == 1 ? "key" : "keys")}", app.Id, null, n)
                    : new(when, d.Name, "publish-test", "published to TEST", app.Id, null, rnd.Next(4, 40)));
            }
            else
            {
                var q = Pick(qas);
                var app = App(Pick(q.AppIds))!;
                list.Add(rnd.NextDouble() < 0.6
                    ? new(when, q.Name, "verify", "verified the TEST build", app.Id)
                    : new(when, q.Name, "publish-live", "published to LIVE", app.Id, null, rnd.Next(10, 80)));
            }
        }

        return list.OrderByDescending(a => a.When).ToList();
    }

    public static IReadOnlyList<PublishRecord> PublishHistory { get; } = GenerateHistory();

    private static List<PublishRecord> GenerateHistory()
    {
        var rnd = new Random(7);
        var notes = new[] { "Sprint release", "Hotfix: checkout copy", "New onboarding flow", "Typo fixes from QA", "Promotion campaign texts", "Translation sync" };
        var list = new List<PublishRecord>();
        foreach (var app in Apps)
        {
            list.Add(new(app.Id, "TEST", app.Test.Version, app.Test.PublishedAt, app.Test.PublishedBy, rnd.Next(4, 40), true, notes[rnd.Next(notes.Length)]));
            list.Add(new(app.Id, "LIVE", app.Live.Version, app.Live.PublishedAt, app.Live.PublishedBy, rnd.Next(10, 80), true, notes[rnd.Next(notes.Length)]));
            var at = app.Live.PublishedAt;
            for (var i = 0; i < 6; i++)
            {
                at = at.AddDays(-rnd.Next(3, 12));
                var env = i % 3 == 2 ? "LIVE" : "TEST";
                list.Add(new(app.Id, env, $"{app.Live.Version.Split('.')[0]}.{Math.Max(0, 9 - i)}.{rnd.Next(0, 5)}", at,
                    People[rnd.Next(People.Length)], rnd.Next(3, 60), rnd.NextDouble() > 0.1, notes[rnd.Next(notes.Length)]));
            }
        }
        return list.OrderByDescending(p => p.At).ToList();
    }

    // ---------- Aggregations used by the views ----------

    public static AppStats StatsFor(AppInfo app)
    {
        var keys = Keys.Where(k => k.AppId == app.Id).ToList();
        var targetLangs = app.Languages.Where(l => l != SourceLanguage).ToList();
        var slots = keys.Count * targetLangs.Count;
        var missing = keys.Sum(k => targetLangs.Count(k.IsMissing));
        return new AppStats(app, keys.Count, keys.Count(k => k.IsNew), missing,
            slots == 0 ? 100 : (slots - missing) * 100.0 / slots,
            keys.Count(k => k.CreatedAt > app.Test.PublishedAt || k.UpdatedAt > app.Test.PublishedAt),
            keys.Count(k => k.CreatedAt > app.Live.PublishedAt || k.UpdatedAt > app.Live.PublishedAt),
            keys.Count(k => targetLangs.Any(k.IsMissing)));
    }

    public static List<LanguageStat> LanguageStatsFor(IEnumerable<AppInfo> apps)
    {
        var appList = apps.ToList();
        return Languages.Where(l => l.Code != SourceLanguage).Select(lang =>
        {
            var keys = Keys.Where(k => appList.Any(a => a.Id == k.AppId && a.Languages.Contains(lang.Code))).ToList();
            var missing = keys.Count(k => k.IsMissing(lang.Code));
            return new LanguageStat(lang, keys.Count, keys.Count - missing, missing,
                keys.Where(k => !k.IsMissing(lang.Code)).Select(k => (DateTime?)k.UpdatedAt).DefaultIfEmpty().Max());
        }).Where(s => s.Total > 0).ToList();
    }

    // ---------- Languages ----------

    /// <summary>A language is translated by 1 to <see cref="MaxTranslators"/> comtors.</summary>
    public const int MaxTranslators = 3;

    public static List<User> TranslatorsFor(string lang) =>
        Users.Where(u => u.Role == UserRole.Comtor && u.Languages.Contains(lang)).OrderBy(u => u.Name).ToList();

    public static AppLanguageProgress ProgressFor(AppInfo app, string lang)
    {
        var keys = Keys.Where(k => k.AppId == app.Id).ToList();
        var done = keys.Where(k => !k.IsMissing(lang)).ToList();
        return new AppLanguageProgress(app, keys.Count, done.Count, done.Select(k => (DateTime?)k.UpdatedAt).DefaultIfEmpty().Max());
    }

    public static LanguageOverview OverviewFor(Language lang)
    {
        var apps = Apps.Where(a => a.Languages.Contains(lang.Code)).ToList();
        var stat = lang.Code == SourceLanguage ? null : LanguageStatsFor(apps).FirstOrDefault(s => s.Language.Code == lang.Code);
        return new LanguageOverview(lang, stat, apps, lang.Code == SourceLanguage ? [] : TranslatorsFor(lang.Code));
    }
}

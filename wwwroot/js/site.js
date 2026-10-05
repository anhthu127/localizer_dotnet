// Localizer UI behaviour. Everything here is front-end only: actions that would hit
// an API (save, publish, download, import…) just show a toast.
(function () {
  const $ = (sel, root = document) => root.querySelector(sel);
  const $$ = (sel, root = document) => Array.from(root.querySelectorAll(sel));

  // ---------- Toast ----------
  const toastEl = $('#lzToast');
  const toast = toastEl ? bootstrap.Toast.getOrCreateInstance(toastEl, { delay: 3200 }) : null;
  window.lzToast = function (message) {
    if (!toast) return;
    $('.lz-toast-body', toastEl).textContent = message;
    toast.show();
  };

  document.addEventListener('click', (e) => {
    const t = e.target.closest('[data-toast]');
    if (t) window.lzToast(t.dataset.toast);

    const c = e.target.closest('[data-copy]');
    if (c) {
      navigator.clipboard?.writeText(c.dataset.copy);
      window.lzToast(`Copied “${c.dataset.copy}”`);
    }
  });

  // Forms that would post to a backend: validate, show a toast, close the modal.
  // Delegated so it also covers content loaded later (e.g. the app drawer).
  document.addEventListener('submit', (e) => {
    const form = e.target.closest('form[data-demo-submit]');
    if (!form) return;
    e.preventDefault();
    if (!form.checkValidity()) { form.reportValidity(); return; }
    const modal = form.closest('.modal');
    if (modal) bootstrap.Modal.getInstance(modal)?.hide();
    window.lzToast(form.dataset.demoSubmit);
  });

  // ---------- Sidebar (mobile) ----------
  const shell = $('.lz-shell');
  $$('[data-sidebar-toggle]').forEach((b) => b.addEventListener('click', () => shell.classList.toggle('sidebar-open')));
  $$('[data-sidebar-close]').forEach((b) => b.addEventListener('click', () => shell.classList.remove('sidebar-open')));

  // "/" focuses the global search
  document.addEventListener('keydown', (e) => {
    if (e.key === '/' && !['INPUT', 'TEXTAREA', 'SELECT'].includes(document.activeElement.tagName)) {
      e.preventDefault();
      $('.lz-topsearch input')?.focus();
    }
  });

  // ---------- Filters that submit on change ----------
  $$('[data-autosubmit]').forEach((el) => el.addEventListener('change', () => el.form.submit()));

  // ---------- "Select all" switches ----------
  function syncGroup(group) {
    const boxes = $$(`[data-group="${group}"]`);
    const checked = boxes.filter((b) => b.checked).length;
    const master = $(`[data-check-all="${group}"]`);
    if (master) {
      master.checked = checked === boxes.length;
      master.indeterminate = checked > 0 && checked < boxes.length;
    }
    const max = Number($(`[data-max-group="${group}"]`)?.dataset.max);
    if (max) boxes.forEach((b) => (b.disabled = !b.checked && checked >= max));
    $$(`[data-count-for="${group}"]`).forEach((el) => (el.textContent = checked));
    $$(`[data-requires-group="${group}"]`).forEach((el) => (el.disabled = checked === 0));
  }
  document.addEventListener('change', (e) => {
    const master = e.target.closest('[data-check-all]');
    if (master) {
      const group = master.dataset.checkAll;
      $$(`[data-group="${group}"]`).forEach((b) => (b.checked = master.checked));
      syncGroup(group);
    } else if (e.target.dataset.group) {
      syncGroup(e.target.dataset.group);
    }
  });

  // Checkbox that enables a button (e.g. "QA verified" before publishing to LIVE)
  $$('[data-enables]').forEach((cb) => {
    const target = $(cb.dataset.enables);
    cb.addEventListener('change', () => (target.disabled = !cb.checked));
  });

  // Open the download dialog when arriving with #download
  if (location.hash === '#download' && $('#downloadModal')) {
    bootstrap.Modal.getOrCreateInstance($('#downloadModal')).show();
    history.replaceState(null, '', location.pathname + location.search);
  }

  $$('[data-bs-toggle="tooltip"]').forEach((el) => new bootstrap.Tooltip(el));

  // ---------- Inline translation editing (application grid) ----------
  // The pencil turns the row's translation cell into a textarea. Falls back to the
  // full edit page (the link's href) when the row has no translation cell.
  const escapeHtml = (s) => String(s).replace(/[&<>"]/g, (c) => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;' }[c]));
  const missingHtml = '<span class="lz-missing"><i class="bi bi-exclamation-circle"></i> Not translated</span>';
  const autosizeArea = (ta) => { ta.style.height = 'auto'; ta.style.height = ta.scrollHeight + 2 + 'px'; };

  const openInlineEditor = (cell) => {
    if (cell.classList.contains('is-editing')) { $('textarea', cell).focus(); return; }
    cell.classList.add('is-editing');
    const editor = document.createElement('div');
    editor.className = 'lz-inline-editor';
    editor.innerHTML = `
      <textarea class="form-control form-control-sm" rows="1" aria-label="${escapeHtml(cell.dataset.langName)} translation"></textarea>
      <div class="lz-inline-actions">
        <span class="lz-inline-hint">Ctrl+Enter to save · Esc to cancel</span>
        <button type="button" class="btn btn-sm btn-light" data-inline-cancel>Cancel</button>
        <button type="button" class="btn btn-sm btn-primary" data-inline-save>Save</button>
      </div>`;
    const ta = $('textarea', editor);
    ta.value = cell.dataset.value || '';
    ta.placeholder = `${cell.dataset.langName} translation…`;
    cell.append(editor);
    autosizeArea(ta);
    ta.focus();
    ta.setSelectionRange(ta.value.length, ta.value.length);
  };

  const closeInlineEditor = (cell) => {
    cell.classList.remove('is-editing');
    $('.lz-inline-editor', cell)?.remove();
  };

  const saveInlineEditor = (cell) => {
    const value = $('textarea', cell).value.trim();
    const changed = value !== (cell.dataset.value || '');
    cell.dataset.value = value;
    $('.lz-cell-view', cell).innerHTML = value ? escapeHtml(value) : missingHtml;
    closeInlineEditor(cell);
    if (changed) window.lzToast(`Saved ${cell.dataset.langName} translation for “${cell.dataset.key}” (demo only)`);
  };

  document.addEventListener('click', (e) => {
    const edit = e.target.closest('[data-inline-edit]');
    const editCell = edit?.closest('tr')?.querySelector('[data-translation-cell]');
    if (editCell) { e.preventDefault(); openInlineEditor(editCell); return; }

    const cell = e.target.closest('[data-translation-cell]');
    if (!cell) return;
    if (e.target.closest('[data-inline-save]')) saveInlineEditor(cell);
    else if (e.target.closest('[data-inline-cancel]')) closeInlineEditor(cell);
  });

  document.addEventListener('keydown', (e) => {
    const ta = e.target.closest('.lz-inline-editor textarea');
    if (!ta) return;
    const cell = ta.closest('[data-translation-cell]');
    if (e.key === 'Escape') { e.preventDefault(); closeInlineEditor(cell); cell.closest('tr').querySelector('[data-inline-edit]')?.focus(); }
    else if (e.key === 'Enter' && (e.ctrlKey || e.metaKey)) { e.preventDefault(); saveInlineEditor(cell); }
  });

  document.addEventListener('input', (e) => {
    const ta = e.target.closest('.lz-inline-editor textarea');
    if (ta) autosizeArea(ta);
  });

  // ---------- Add / edit key form ----------
  const appSelect = $('#appSelect');
  if (appSelect) {
    const rows = $$('[data-lang-row]');
    const fields = $$('[data-translation]');
    const updateLangs = () => {
      const langs = appSelect.selectedOptions[0]?.dataset.langs?.split(',');
      rows.forEach((r) => (r.style.display = !langs || langs.includes(r.dataset.langRow) ? '' : 'none'));
      updateCount();
    };
    const updateCount = () => {
      const visible = fields.filter((f) => f.closest('[data-lang-row]').style.display !== 'none');
      $('[data-filled-count]').textContent = visible.filter((f) => f.value.trim()).length;
      $('[data-lang-total]').textContent = visible.length;
    };
    const autosize = (ta) => { ta.style.height = 'auto'; ta.style.height = ta.scrollHeight + 2 + 'px'; };

    // Content type: short labels autosize in place; long text shows one language at a
    // time (tabs) in a tall editor with the English source and an HTML preview.
    const card = $('[data-translations]');
    const tabs = $$('[data-lang-tab]');
    const sourceRow = $('.lz-lang-field.source');
    const sourceField = $('[data-translation]', sourceRow);
    const isLong = () => card.classList.contains('is-long');
    const rowOf = (code) => rows.find((r) => r.dataset.langRow === code);
    let active = sourceRow.dataset.langRow;

    const previewDoc = (text) => {
      const html = /<[a-z][\s\S]*>/i.test(text);
      return `<!doctype html><meta charset="utf-8"><base target="_blank">
        <style>body{font:14px/1.6 system-ui,-apple-system,"Segoe UI",sans-serif;color:#0f1b2d;margin:14px 16px;${html ? '' : 'white-space:pre-wrap;'}}
        h1,h2,h3{line-height:1.3}a{color:#2563eb}</style>${html ? text : escapeHtml(text)}`;
    };
    const showView = (row, view) => {
      const ta = $('[data-translation]', row);
      const frame = $('[data-preview]', row);
      if (view === 'preview') {
        frame.style.height = Math.max(ta.offsetHeight, 360) + 'px';
        frame.srcdoc = previewDoc(ta.value);
      }
      row.classList.toggle('is-preview', view === 'preview');
      $(`[data-view][value="${view}"]`, row).checked = true;
    };
    const updateRowInfo = (f) => {
      const row = f.closest('[data-lang-row]');
      const n = f.value.length;
      $('[data-char-count]', row).textContent = `${n.toLocaleString()} ${n === 1 ? 'character' : 'characters'}`;
      $('.dot', tabs.find((t) => t.dataset.langTab === row.dataset.langRow))?.classList.toggle('filled', !!f.value.trim());
    };
    const activate = (code) => {
      active = code;
      rows.forEach((r) => r.classList.toggle('is-active', r.dataset.langRow === code));
      tabs.forEach((t) => {
        t.classList.toggle('active', t.dataset.langTab === code);
        t.setAttribute('aria-selected', t.dataset.langTab === code);
      });
      const ref = $('[data-source-ref]', rowOf(code));
      if (ref) ref.textContent = sourceField.value.trim() ? sourceField.value : 'No English source text yet.';
      if (rowOf(code).classList.contains('is-preview')) showView(rowOf(code), 'preview');
    };
    const setFormat = (format) => {
      const long = format === 'LongText';
      card.classList.toggle('is-long', long);
      $$('[data-format-only]').forEach((el) => el.classList.toggle('d-none', el.dataset.formatOnly !== format));
      if (long) { fields.forEach((f) => (f.style.height = '')); activate(active); }
      else { rows.forEach((r) => showView(r, 'write')); fields.forEach(autosize); }
    };

    appSelect.addEventListener('change', () => {
      updateLangs();
      const langs = appSelect.selectedOptions[0]?.dataset.langs?.split(',');
      tabs.forEach((t) => (t.style.display = !langs || langs.includes(t.dataset.langTab) ? '' : 'none'));
      if (langs && !langs.includes(active)) activate(sourceRow.dataset.langRow);
    });
    fields.forEach((f) => {
      f.addEventListener('input', () => { updateCount(); updateRowInfo(f); if (!isLong()) autosize(f); });
      // A required field hidden behind another tab can't show its message: bring it up first.
      f.addEventListener('invalid', () => {
        if (!isLong()) return;
        const row = f.closest('[data-lang-row]');
        activate(row.dataset.langRow);
        showView(row, 'write');
      });
      updateRowInfo(f);
    });
    tabs.forEach((t) => t.addEventListener('click', () => activate(t.dataset.langTab)));
    $$('input[name="format"]').forEach((r) => r.addEventListener('change', () => setFormat(r.value)));
    card.addEventListener('change', (e) => {
      if (e.target.matches('[data-view]')) showView(e.target.closest('[data-lang-row]'), e.target.value);
    });
    card.addEventListener('click', (e) => {
      const btn = e.target.closest('[data-copy-source]');
      if (!btn) return;
      const row = btn.closest('[data-lang-row]');
      const ta = $('[data-translation]', row);
      if (!sourceField.value.trim()) { window.lzToast('Write the English source first'); return; }
      if (ta.value.trim() && !confirm('Replace the current translation with the English source?')) return;
      ta.value = sourceField.value;
      ta.dispatchEvent(new Event('input'));
      showView(row, 'write');
      ta.focus();
      ta.setSelectionRange(0, 0);
    });

    setFormat($('input[name="format"]:checked').value);
    updateLangs();
  }

  // ---------- Upload JSON ----------
  const fileInput = $('#upFile');
  if (fileInput) {
    const dropzone = $('#dropzone');
    const langSelect = $('#upLang');
    const appSel = $('#upApp');
    const importBtn = $('#importBtn');
    const errorBox = $('#fileError');
    let parsed = null;

    const flatten = (obj, prefix = '', out = {}) => {
      for (const [k, v] of Object.entries(obj)) {
        const key = prefix ? `${prefix}.${k}` : k;
        if (v && typeof v === 'object' && !Array.isArray(v)) flatten(v, key, out);
        else out[key] = v;
      }
      return out;
    };
    const escape = (s) => String(s).replace(/[&<>"]/g, (c) => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;' }[c]));
    const refresh = () => (importBtn.disabled = !(parsed && appSel.value && langSelect.value));

    const showError = (msg) => {
      errorBox.classList.toggle('d-none', !msg);
      $('div', errorBox).textContent = msg || '';
    };

    const handleFile = (file) => {
      parsed = null;
      showError(null);
      $('#previewCard').classList.add('d-none');
      if (!file) { $('#fileChip').classList.add('d-none'); refresh(); return; }

      $('#fileChip').classList.remove('d-none');
      $('#fileName').textContent = file.name;
      $('#fileMeta').textContent = `${(file.size / 1024).toFixed(1)} KB`;

      // Guess the language from the file name: vi.json, app_vi.json, zh-CN.json…
      const base = file.name.replace(/\.json$/i, '');
      const guess = Array.from(langSelect.options).find((o) => o.value && (base === o.value || base.endsWith('_' + o.value) || base.endsWith('-' + o.value) || base.endsWith('.' + o.value)));
      if (guess) langSelect.value = guess.value;

      const reader = new FileReader();
      reader.onload = () => {
        try {
          const json = JSON.parse(reader.result);
          if (!json || typeof json !== 'object' || Array.isArray(json)) throw new Error('The root of the file must be an object of key → text.');
          const flat = flatten(json);
          const entries = Object.entries(flat);
          const invalid = entries.filter(([, v]) => typeof v !== 'string').length;
          parsed = flat;
          $('#fileMeta').textContent = `${(file.size / 1024).toFixed(1)} KB · ${entries.length} keys`;
          $('#previewSub').textContent = `${entries.length} keys found` + (invalid ? ` · ${invalid} non-text values will be skipped` : '') + (entries.length > 200 ? ' · showing first 200' : '');
          $('#previewBody').innerHTML = entries.slice(0, 200).map(([k, v]) =>
            `<tr><td class="lz-key">${escape(k)}</td><td>${typeof v === 'string' ? escape(v) : '<span class="lz-missing">skipped – not text</span>'}</td></tr>`).join('');
          $('#previewCard').classList.remove('d-none');
        } catch (err) {
          showError(`Could not read this file: ${err.message}`);
        }
        refresh();
      };
      reader.readAsText(file);
    };

    fileInput.addEventListener('change', () => handleFile(fileInput.files[0]));
    ['dragenter', 'dragover'].forEach((ev) => dropzone.addEventListener(ev, () => dropzone.classList.add('drag')));
    ['dragleave', 'drop'].forEach((ev) => dropzone.addEventListener(ev, () => dropzone.classList.remove('drag')));
    $('#fileRemove').addEventListener('click', () => { fileInput.value = ''; handleFile(null); });
    [appSel, langSelect].forEach((s) => s.addEventListener('change', refresh));
  }

  // ---------- Applications: lock / unlock ----------
  // Every element tied to an app carries data-lock-scope="{id}" and gets .is-locked
  // (or .is-lock-scheduled for a lock that starts later); all switches and drawer
  // controls for that app stay in sync. Changes are remembered so a drawer loaded
  // later can be brought up to date.
  const lockStates = {};
  const lockLabels = { open: 'Open', locked: 'Locked', scheduled: 'Scheduled' };
  const me = $('.lz-user-name')?.textContent.trim() ?? 'you';
  const pad = (n) => String(n).padStart(2, '0');
  const isoToday = () => { const d = new Date(); return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}`; };
  const MONTHS = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
  const day = (iso) => { const [y, m, d] = iso.split('-'); return `${d} ${MONTHS[m - 1]} ${y}`; };

  const setLockUi = (id, state, detail = '') => {
    lockStates[id] = { state, detail };
    $$(`[data-lock-toggle="${id}"]`).forEach((cb) => (cb.checked = state === 'locked'));
    $$(`[data-lock-scope="${id}"]`).forEach((el) => {
      el.classList.toggle('is-locked', state === 'locked');
      el.classList.toggle('is-lock-scheduled', state === 'scheduled');
    });
    $$(`[data-lock-label="${id}"]`).forEach((el) => (el.textContent = lockLabels[state]));
    $$(`[data-lock-detail="${id}"]`).forEach((el) => (el.textContent = detail));
    $$(`[data-lock-tip="${id}"]`).forEach((el) => (el.title = detail || 'Comtors can translate'));
    updateLockCounts();
    applyAppFilter();
  };
  const updateLockCounts = () => {
    const rows = $$('[data-app-row]');
    const locked = rows.filter((r) => r.classList.contains('is-locked')).length;
    $$('[data-lock-count="locked"]').forEach((el) => (el.textContent = locked));
    $$('[data-lock-count="open"]').forEach((el) => (el.textContent = rows.length - locked));
  };
  const lockNow = (id, name) => {
    setLockUi(id, 'locked', `Locked by ${me} · just now · until unlocked`);
    window.lzToast(`${name} locked – comtors can no longer edit translations (demo only)`);
  };
  const unlock = (id, name, wasScheduled) => {
    setLockUi(id, 'open');
    window.lzToast(wasScheduled
      ? `Scheduled lock for ${name} cancelled (demo only)`
      : `${name} unlocked – translation is open again (demo only)`);
  };

  // Table switch: on = lock now until unlocked, off = unlock (also drops a schedule).
  document.addEventListener('change', (e) => {
    const cb = e.target.closest('[data-lock-toggle]');
    if (!cb) return;
    if (cb.checked) lockNow(cb.dataset.lockToggle, cb.dataset.appName);
    else unlock(cb.dataset.lockToggle, cb.dataset.appName, false);
  });

  // Drawer: Unlock / Cancel schedule
  document.addEventListener('click', (e) => {
    const b = e.target.closest('[data-lock-clear]');
    if (!b) return;
    const scheduled = b.closest('[data-lock-scope]').classList.contains('is-lock-scheduled');
    unlock(b.dataset.lockClear, b.dataset.appName, scheduled);
  });

  // Drawer: "Lock now" vs "Schedule" (start + end date)
  document.addEventListener('change', (e) => {
    const form = e.target.form;
    if (!form?.matches('[data-lock-form]')) return;
    if (e.target.name === 'lockMode') {
      const schedule = e.target.value === 'schedule';
      $$('[data-lock-mode-hint]', form).forEach((el) => el.classList.toggle('d-none', el.dataset.lockModeHint !== e.target.value));
      $('[data-lock-submit] span', form).textContent = schedule ? 'Schedule lock' : 'Lock now';
      $('[data-lock-submit] i', form).className = schedule ? 'bi bi-calendar-check' : 'bi bi-lock-fill';
    }
    if (e.target.name === 'from') form.elements.until.min = form.elements.from.value || isoToday();
    if (['from', 'until'].includes(e.target.name)) e.target.classList.remove('is-invalid');
  });
  document.addEventListener('submit', (e) => {
    const form = e.target.closest('form[data-lock-form]');
    if (!form) return;
    e.preventDefault();
    const id = form.dataset.lockForm;
    const name = form.dataset.appName;
    if (form.elements.lockMode.value === 'now') { lockNow(id, name); return; }

    const { from, until } = form.elements;
    const today = isoToday();
    from.classList.toggle('is-invalid', !from.value || from.value < today);
    until.classList.toggle('is-invalid', !until.value || until.value < from.value || until.value < today);
    if ($('.is-invalid', form)) return;

    if (from.value <= today) {
      setLockUi(id, 'locked', `Locked by ${me} · until ${day(until.value)}`);
      window.lzToast(`${name} locked until ${day(until.value)} (demo only)`);
    } else {
      setLockUi(id, 'scheduled', `Scheduled from ${day(from.value)} to ${day(until.value)} by ${me}`);
      window.lzToast(`${name} will be locked from ${day(from.value)} to ${day(until.value)} (demo only)`);
    }
  });

  // ---------- Applications: filter ----------
  const filterText = $('[data-app-filter-text]');
  function applyAppFilter() {
    if (!filterText) return;
    const q = filterText.value.trim().toLowerCase();
    const lock = $('[data-app-filter-lock]:checked')?.value ?? 'all';
    let visible = 0;
    $$('[data-app-row]').forEach((row) => {
      const locked = row.classList.contains('is-locked');
      const show = row.dataset.name.toLowerCase().includes(q)
        && (lock === 'all' || (lock === 'locked') === locked);
      row.classList.toggle('d-none', !show);
      if (show) visible++;
    });
    $('[data-app-empty]')?.classList.toggle('d-none', visible > 0);
  }
  if (filterText) {
    filterText.addEventListener('input', applyAppFilter);
    $$('[data-app-filter-lock]').forEach((r) => r.addEventListener('change', applyAppFilter));
  }

  // ---------- Detail drawer (Applications, Languages) ----------
  // Rows carry data-drawer-url; the drawer loads that partial on click.
  const drawerEl = $('#appDrawer, #langDrawer');
  if (drawerEl) {
    const drawer = bootstrap.Offcanvas.getOrCreateInstance(drawerEl);
    const loadingHtml = drawerEl.innerHTML;
    let openRow = null;

    const openDrawer = async (row, section) => {
      openRow?.classList.remove('is-drawer-open');
      openRow = row;
      row.classList.add('is-drawer-open');
      drawerEl.innerHTML = loadingHtml;
      drawer.show();
      try {
        const res = await fetch(row.dataset.drawerUrl);
        if (!res.ok) throw new Error(res.statusText);
        drawerEl.innerHTML = await res.text();
      } catch (err) {
        drawerEl.innerHTML = `<div class="lz-empty"><i class="bi bi-wifi-off"></i><h3>Could not load details</h3><p>${err.message}</p></div>`;
        return;
      }
      // The lock may have changed since page load – the latest change wins.
      const id = row.dataset.appRow;
      if (lockStates[id]) setLockUi(id, lockStates[id].state, lockStates[id].detail);
      if (section === 'languages') $('#drawer-languages', drawerEl)?.scrollIntoView({ block: 'start' });
    };

    document.addEventListener('click', (e) => {
      const trigger = e.target.closest('[data-open-drawer]');
      const row = e.target.closest('[data-drawer-url]');
      if (!row?.dataset.drawerUrl) return; // e.g. the source language has no drawer
      if (trigger) { openDrawer(row, trigger.dataset.openDrawer); return; }
      // Clicks on the row's own controls (switch, links, menu) keep their normal behaviour.
      if (e.target.closest('a, button, input, label, .dropdown-menu')) return;
      openDrawer(row);
    });
    document.addEventListener('keydown', (e) => {
      const row = e.target.closest?.('[data-drawer-url]');
      if (row?.dataset.drawerUrl && e.target === row && e.key === 'Enter') openDrawer(row);
    });
    drawerEl.addEventListener('hidden.bs.offcanvas', () => openRow?.classList.remove('is-drawer-open'));

    // Warn when a language that currently has translations gets unticked.
    drawerEl.addEventListener('change', (e) => {
      if (e.target.name !== 'langs') return;
      const removed = $$('input[name="langs"]', drawerEl).some((c) => c.dataset.wasChecked === 'true' && !c.checked);
      $('[data-removed-warning]', drawerEl)?.classList.toggle('d-none', !removed);
    });
  }

  // ---------- Invite / edit user modal ----------
  const userModal = $('#userModal');
  if (userModal) {
    const form = $('form', userModal);
    const comtorOnly = $('[data-comtor-only]', userModal);
    const syncRole = () => {
      const role = $('input[name="role"]:checked', form)?.value;
      comtorOnly.style.display = role === 'Comtor' ? '' : 'none';
    };
    $$('input[name="role"]', form).forEach((r) => r.addEventListener('change', syncRole));

    userModal.addEventListener('show.bs.modal', (e) => {
      const data = e.relatedTarget?.dataset.user ? JSON.parse(e.relatedTarget.dataset.user) : null;
      form.reset();
      $('[data-user-title]', userModal).textContent = data ? `Edit ${data.Name}` : 'Invite user';
      $('[data-user-submit]', userModal).textContent = data ? 'Save changes' : 'Send invite';
      $('#uName').value = data?.Name ?? '';
      $('#uEmail').value = data?.Email ?? '';
      if (data) $(`input[name="role"][value="${data.Role}"]`, form).checked = true;
      $$('input[name="langs"]', form).forEach((c) => (c.checked = !!data?.Languages.includes(c.value)));
      $$('input[name="apps"]', form).forEach((c) => (c.checked = !!data?.AppIds.includes(Number(c.value))));
      syncGroup('u-apps');
      syncRole();
    });
  }

  // ---------- Languages ----------
  const langFilter = $('[data-lang-filter]');
  if (langFilter) {
    langFilter.addEventListener('input', () => {
      const q = langFilter.value.trim().toLowerCase();
      let visible = 0;
      $$('[data-lang-row]').forEach((row) => {
        const show = row.dataset.name.toLowerCase().includes(q);
        row.classList.toggle('d-none', !show);
        if (show) visible++;
      });
      $('[data-lang-empty]')?.classList.toggle('d-none', visible > 0);
    });
  }

  // Add / edit language modal; also opened from the language drawer.
  const langModal = $('#langModal');
  if (langModal) {
    const form = $('form', langModal);
    langModal.addEventListener('show.bs.modal', (e) => {
      const data = e.relatedTarget?.dataset.lang ? JSON.parse(e.relatedTarget.dataset.lang) : null;
      const isSource = data?.Code === 'en';
      // Opened from the drawer: close it so the two focus traps don't fight.
      const drawer = e.relatedTarget?.closest('.offcanvas');
      if (drawer) bootstrap.Offcanvas.getInstance(drawer)?.hide();
      form.reset();
      $('[data-lang-title]', langModal).textContent = data ? `Edit ${data.Name}` : 'Add language';
      $('[data-lang-submit]', langModal).textContent = data ? 'Save changes' : 'Add language';
      form.dataset.demoSubmit = data ? `${data.Name} saved (demo only)` : 'Language added (demo only)';
      $('#lCode').value = data?.Code ?? '';
      $('#lCode').readOnly = !!data; // the code is used in keys and file names
      $('#lName').value = data?.Name ?? '';
      $('#lNative').value = data?.NativeName ?? '';
      $('#lFlag').value = data?.Flag ?? '';
      // The source language is written by developers, so it has no translators.
      $('[data-translators-field]', langModal).style.display = isSource ? 'none' : '';
      $$('input[name="translators"]', form).forEach((c) => (c.checked = !!data?.Translators.includes(Number(c.value))));
      syncGroup('lang-translators');
      if (isSource) $('[data-lang-submit]', langModal).disabled = false;
    });
  }

  const langDeleteModal = $('#langDeleteModal');
  if (langDeleteModal) {
    langDeleteModal.addEventListener('show.bs.modal', (e) => {
      const d = e.relatedTarget.dataset;
      const apps = Number(d.langApps);
      $('[data-del-name]', langDeleteModal).textContent = d.langName;
      $('[data-del-apps]', langDeleteModal).textContent = `${apps} ${apps === 1 ? 'application' : 'applications'}`;
      $('[data-del-strings]', langDeleteModal).textContent = Number(d.langStrings).toLocaleString();
      $('form', langDeleteModal).dataset.demoSubmit = `${d.langName} deleted (demo only)`;
    });
  }
})();

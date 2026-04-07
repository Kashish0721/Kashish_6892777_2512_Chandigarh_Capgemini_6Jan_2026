// ── Auto-dismiss alerts ────────────────────────────────────────────────────
document.addEventListener('DOMContentLoaded', function () {
    // Auto-dismiss success alerts after 4 seconds
    var alerts = document.querySelectorAll('.alert-success, .alert-info');
    alerts.forEach(function (alert) {
        setTimeout(function () {
            var bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
            if (bsAlert) bsAlert.close();
        }, 4000);
    });

    // ── Confirm delete buttons ─────────────────────────────────────────────
    document.querySelectorAll('[data-confirm]').forEach(function (el) {
        el.addEventListener('click', function (e) {
            if (!confirm(el.getAttribute('data-confirm'))) {
                e.preventDefault();
                e.stopPropagation();
            }
        });
    });

    // ── Client-side form validation feedback ──────────────────────────────
    var forms = document.querySelectorAll('form[novalidate]');
    forms.forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        });
    });

    // ── Token expiry check (if expiry stored in sessionStorage) ───────────
    var expiry = sessionStorage.getItem('tokenExpiry');
    if (expiry) {
        var expiryDate = new Date(expiry);
        if (expiryDate < new Date()) {
            sessionStorage.removeItem('tokenExpiry');
            // Could redirect to refresh or login
        }
    }

    // ── Tooltip initialization ─────────────────────────────────────────────
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.forEach(function (el) {
        new bootstrap.Tooltip(el);
    });

    // ── Active nav highlighting (already handled via Razor, but fallback) ──
    var path = window.location.pathname.toLowerCase();
    document.querySelectorAll('.sidebar-link').forEach(function (link) {
        var href = link.getAttribute('href')?.toLowerCase();
        if (href && href !== '/' && path.startsWith(href)) {
            link.classList.add('active');
        }
    });
});

// ── Utility: format currency ─────────────────────────────────────────────────
function formatCurrency(amount) {
    return '₹' + parseFloat(amount).toLocaleString('en-IN');
}

// ── Utility: show loading state on buttons ───────────────────────────────────
function showLoading(btn, text) {
    btn.disabled = true;
    btn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>' + (text || 'Loading...');
}

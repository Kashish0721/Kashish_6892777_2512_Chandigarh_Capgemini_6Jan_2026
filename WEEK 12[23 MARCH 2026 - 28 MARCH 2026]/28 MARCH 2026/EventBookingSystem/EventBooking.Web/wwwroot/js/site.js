/* ── EventHub — site.js ─────────────────────────────────────────────────── */

// ── Auth helpers ────────────────────────────────────────────────────────────
const Auth = {
    getToken: () => sessionStorage.getItem('jwt_token'),
    setToken: (t) => sessionStorage.setItem('jwt_token', t),
    clearToken: () => sessionStorage.removeItem('jwt_token'),
    getUser: () => {
        const raw = sessionStorage.getItem('auth_user');
        return raw ? JSON.parse(raw) : null;
    },
    setUser: (u) => sessionStorage.setItem('auth_user', JSON.stringify(u)),
    clearUser: () => sessionStorage.removeItem('auth_user'),
    isLoggedIn: () => !!sessionStorage.getItem('jwt_token'),
    logout: () => {
        Auth.clearToken();
        Auth.clearUser();
    }
};

// ── API helpers ─────────────────────────────────────────────────────────────
const API_BASE = window.API_BASE_URL || 'https://localhost:7000';

async function apiFetch(path, options = {}) {
    const token = Auth.getToken();
    const headers = {
        'Content-Type': 'application/json',
        ...(token ? { 'Authorization': `Bearer ${token}` } : {}),
        ...(options.headers || {})
    };

    const res = await fetch(`${API_BASE}${path}`, { ...options, headers });

    if (res.status === 401) {
        Auth.logout();
        window.location.href = '/Account/Login?expired=1';
        return null;
    }

    return res;
}

// ── Toast notifications ─────────────────────────────────────────────────────
function showToast(message, type = 'success') {
    const iconMap = {
        success: 'bi-check-circle-fill',
        error: 'bi-x-circle-fill',
        warning: 'bi-exclamation-triangle-fill',
        info: 'bi-info-circle-fill'
    };
    const colorMap = {
        success: 'bg-success',
        error: 'bg-danger',
        warning: 'bg-warning',
        info: 'bg-primary'
    };

    let container = document.getElementById('toastContainer');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toastContainer';
        container.className = 'toast-container position-fixed bottom-0 end-0 p-3';
        document.body.appendChild(container);
    }

    const id = 'toast_' + Date.now();
    const html = `
    <div id="${id}" class="toast custom-toast align-items-center text-white ${colorMap[type]} border-0" role="alert">
      <div class="d-flex">
        <div class="toast-body d-flex align-items-center gap-2">
          <i class="bi ${iconMap[type]}"></i>
          <span>${message}</span>
        </div>
        <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
      </div>
    </div>`;

    container.insertAdjacentHTML('beforeend', html);
    const el = document.getElementById(id);
    const toast = new bootstrap.Toast(el, { delay: 4000 });
    toast.show();
    el.addEventListener('hidden.bs.toast', () => el.remove());
}

// ── Form validation helpers ─────────────────────────────────────────────────
function markValid(input) {
    input.classList.remove('is-invalid');
    input.classList.add('is-valid');
}

function markInvalid(input, message) {
    input.classList.remove('is-valid');
    input.classList.add('is-invalid');
    const fb = input.nextElementSibling;
    if (fb && fb.classList.contains('invalid-feedback')) {
        fb.textContent = message;
    }
}

function clearValidation(form) {
    form.querySelectorAll('.is-valid, .is-invalid').forEach(el => {
        el.classList.remove('is-valid', 'is-invalid');
    });
}

// ── Category badge color ────────────────────────────────────────────────────
function getCategoryClass(category) {
    const map = {
        'Technology': 'badge-tech',
        'Music': 'badge-music',
        'Sports': 'badge-sports',
        'Business': 'badge-business'
    };
    return map[category] || 'badge-general';
}

// ── Format currency ─────────────────────────────────────────────────────────
function formatPrice(amount) {
    if (amount === 0) return '<span class="price-tag free">FREE</span>';
    return `<span class="price-tag">₹${Number(amount).toLocaleString('en-IN')}</span>`;
}

// ── Format date ─────────────────────────────────────────────────────────────
function formatDate(dateStr) {
    const d = new Date(dateStr);
    return d.toLocaleDateString('en-IN', {
        weekday: 'short', year: 'numeric',
        month: 'short', day: 'numeric',
        hour: '2-digit', minute: '2-digit'
    });
}

// ── Seats availability bar ──────────────────────────────────────────────────
function seatsBar(available, booked) {
    const remaining = available - booked;
    const pct = available > 0 ? Math.round((remaining / available) * 100) : 0;
    const cls = pct < 25 ? 'low' : '';
    return `
    <div class="d-flex justify-content-between mb-1">
      <small class="text-muted">${remaining} seats left</small>
      <small class="text-muted">${pct}%</small>
    </div>
    <div class="seats-bar">
      <div class="seats-bar-fill ${cls}" style="width:${pct}%"></div>
    </div>`;
}

export function hideSections() {
    const mainSection = document.querySelector('#wrapper > main');

    Array.from(mainSection.children).forEach(section => section.style.display = 'none');
}

export function redirect(url) {
    // pushState doesn't trigger popstate...
    history.pushState(null, null, url);

    // Manually dispatch popstate event
    window.dispatchEvent(new CustomEvent('popstate'));
}


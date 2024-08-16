setupLinks();

function setupLinks() {
    const siteWrapperEl = document.getElementById('wrapper');

    siteWrapperEl.addEventListener('click', (e) => {
        if (e.target.tagName === 'A') {
            e.preventDefault();

            const url = new URL();

            history.pushState(null, null, url.pathname);
        }
    })
}

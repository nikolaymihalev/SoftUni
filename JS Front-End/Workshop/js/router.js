import {
    renderCreate,
    renderDetails,
    renderEdit,
    renderHome,
    renderLogin,
    renderRegister,
    renderSolutions,
    renderLogout,
    renderHedaer,
    renderDelete,
} from './views.js';

import { hideSections, redirect } from './router-utils.js';

// Route mapping
export const routes = {
    '/': renderHome,
    '/solutions': renderSolutions,
    '/register': renderRegister,
    '/login': renderLogin,
    '/solutions/add': renderCreate,
    '/solutions/:solutionId/edit': renderEdit,
    '/solutions/:solutionId/delete': renderDelete,
    '/solutions/:solutionId/details': renderDetails,
    '/logout': renderLogout,
};

function matchRoute(path) {
    for (const route in routes) {
        const routeSegments = route.split('/');
        const pathSegments = path.split('/');

        if (routeSegments.length !== pathSegments.length) {
            continue;
        }
        
        let params = {};
        let match = true;

        for (let i = 0; i < routeSegments.length; i++) {
            if (routeSegments[i].startsWith(':')) {
                params[routeSegments[i].slice(1)] = pathSegments[i];
            } else if(routeSegments[i] !== pathSegments[i]) {
                match = false;
                break;
            }
        }

        if (match) {
            return routes[route].bind(null, params); // Think about this :)
        }
    }

    // IF no match return not found render function 404 page
}

export function setupRouter() {
    hideSections();
    renderHedaer();

    // Load current page on refresh
    matchRoute(location.pathname)();

    // Handle url change
    window.addEventListener('popstate', (e) => {
        hideSections();
        renderHedaer();

        // Check if pathname exists inside routes!!
        matchRoute(location.pathname)();
    });
}

export function setupLinks() {
    const siteWrapperElement = document.getElementById('wrapper');

    siteWrapperElement.addEventListener('click', (e) => {
        if (e.target.tagName === 'A') {
            e.preventDefault();

            const url = new URL(e.target.href)

            redirect(url.pathname);
        }
    });
}

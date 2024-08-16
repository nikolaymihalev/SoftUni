const baseUrl = 'http://localhost:3030/users';

export async function login(email, password) {
    const response = await fetch(`${baseUrl}/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, password })
    });

    const result = await response.json();

    // Save auth data to localstorage
    saveAuthData(result);

    return result;
}

export async function logout() {
    // TODO: send logout request

    removeAuthData();
}

export function isAuthenticated() {
    return !!localStorage.getItem('email')
}

export function getAuth() {
    const userId = localStorage.getItem('userId');
    const email = localStorage.getItem('email');
    const accessToken = localStorage.getItem('accessToken');

    const authData = {
        userId,
        email,
        accessToken,
    };

    return authData;
}

function saveAuthData(authData) {
    localStorage.setItem('userId', authData._id);
    localStorage.setItem('email', authData.email);
    localStorage.setItem('accessToken', authData.accessToken);
}

function removeAuthData() {
    localStorage.removeItem('userId');
    localStorage.removeItem('email');
    localStorage.removeItem('accessToken');
}

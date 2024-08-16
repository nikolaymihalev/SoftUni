import { getAuth } from "./authService.js";

const baseUrl = 'http://localhost:3030/data/solutions';

export async function getAll() {
    const response = await fetch(baseUrl);

    const result = await response.json();

    return result;
}

export async function create(type, imageUrl, description, learnMore) {
    const solutionData = {
        type,
        imageUrl,
        description,
        learnMore,
    };

    const response = await fetch(baseUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Authorization': getAuth().accessToken,
        },
        body: JSON.stringify(solutionData),
    });

    const result = await response.json();

    return result;
}

export async function getOne(solutionId) {
    const response = await fetch(`${baseUrl}/${solutionId}`);
    const result = await response.json();

    return result;
}

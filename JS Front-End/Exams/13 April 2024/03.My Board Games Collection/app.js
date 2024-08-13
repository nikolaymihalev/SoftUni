const baseUrl = "http://localhost:3030/jsonstore/games/";

const loadBtn = document.getElementById('load-games');
const gameList = document.getElementById('games-list');
const addBtn = document.getElementById('add-game');
const editBtn = document.getElementById('edit-game');

const nameInput = document.getElementById('g-name');
const typeInput = document.getElementById('type');
const playersInput = document.getElementById('players');

const formEl = document.querySelector("#form form");

loadBtn.addEventListener('click', loadGames);
addBtn.addEventListener('click', addGame);
editBtn.addEventListener('click', editGame);


async function loadGames() {
    gameList.innerHTML = '';

    const response = await fetch(baseUrl);
    const result = await response.json();
    const games = Object.values(result);

    const gameElements = games.map(game => createGameElement(game.name, game.type, game.players, game._id));

    gameList.append(...gameElements);

}

function createGameElement(name, type, players, _id) {
    const pNameEl = document.createElement('p');
    pNameEl.textContent = name;

    const pTypeEl = document.createElement('p');
    pTypeEl.textContent = type;

    const pMaxPlayersEl = document.createElement('p');
    pMaxPlayersEl.textContent = players;

    const divContentEl = document.createElement('div');
    divContentEl.classList.add('content');

    divContentEl.appendChild(pNameEl);
    divContentEl.appendChild(pTypeEl);
    divContentEl.appendChild(pMaxPlayersEl);

    const changeBtnEl = document.createElement('button');
    changeBtnEl.classList.add('change-btn');
    changeBtnEl.textContent = 'Change';
    changeBtnEl.addEventListener('click', () => {
        nameInput.value = name;
        typeInput.value = type;
        playersInput.value = players;

        editBtn.removeAttribute('disabled');
        addBtn.setAttribute('disabled', 'disabled');

        formEl.setAttribute('data-game-id', _id)
    })

    const deleteBtnEl = document.createElement('button');
    deleteBtnEl.classList.add('delete-btn');
    deleteBtnEl.textContent = 'Delete';
    deleteBtnEl.addEventListener('click', async () => {
        await fetch(`${baseUrl}/${_id}`, {
            method: 'DELETE',
        });

        await loadGames();
    })

    const btnsContainer = document.createElement('div');
    btnsContainer.classList.add('buttons-container');

    btnsContainer.appendChild(changeBtnEl);
    btnsContainer.appendChild(deleteBtnEl);

    const gameDivEl = document.createElement('div');
    gameDivEl.classList.add('board-game');

    gameDivEl.appendChild(divContentEl);
    gameDivEl.appendChild(btnsContainer);

    return gameDivEl;
}

function clearInputs() {
    nameInput.value = '';
    typeInput.value = '';
    playersInput.value = '';
}

async function addGame() {
    const name = nameInput.value;
    const type = typeInput.value;
    const players = playersInput.value;
    clearInputs();

    const response = await fetch(baseUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ name, type, players })
    }
    )

    await loadGames();
}

async function editGame() {
    const name = nameInput.value;
    const type = typeInput.value;
    const players = playersInput.value;

    clearInputs();

    const _id = formEl.getAttribute('data-game-id');

    await fetch(`${baseUrl}/${_id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ name, type, players, _id })
    });

    await loadGames();

    editBtn.setAttribute('disabled', 'disabled');
    addBtn.removeAttribute('disabled');

    formEl.removeAttribute('data-game-id');
}
const BASE_URL = 'http://localhost:3030/jsonstore/matches';

const endpoints = {
    update: (id) => `${BASE_URL}/${id}`,
    delete: (id) => `${BASE_URL}/${id}`,
};

const list = document.getElementById('list');

const loadBtn = document.getElementById("load-matches");
const addBtn = document.getElementById('add-match');
const editBtn = document.getElementById('edit-match');

const hostInput = document.getElementById('host');
const scoreInput = document.getElementById('score');
const guestInput = document.getElementById('guest');

let selectedTaskId = null;

function attachEvents() {
    loadBtn.addEventListener('click', loadBoardEventHandler);
    addBtn.addEventListener('click', createTaskEventHandler);
    editBtn.addEventListener('click', editTaskEventHandler);
}

async function loadBoardEventHandler() {
    clearAllSections();
    try {
        const res = await fetch(BASE_URL);
        const allMatches = await res.json();
        Object.values(allMatches).forEach((match) => {
            const container = document.createElement('li');
            container.classList.add('match');

            const infoContainer = document.createElement('div');
            infoContainer.classList.add('info');

            const pNameEl = document.createElement('p');
            pNameEl.textContent = match.host;

            const pScoreEl = document.createElement('p');
            pScoreEl.textContent = match.score;

            const pGuestEl = document.createElement('p');
            pGuestEl.textContent = match.guest;

            infoContainer.appendChild(pNameEl);
            infoContainer.appendChild(pScoreEl);
            infoContainer.appendChild(pGuestEl);

            const wrapperContainer = document.createElement('div')
            wrapperContainer.classList.add("btn-wrapper");

            const changeBtn = document.createElement('button');
            changeBtn.textContent = 'Change';
            changeBtn.classList.add("change-btn");

            const deleteBtn = document.createElement('button');
            deleteBtn.textContent = 'Delete';
            deleteBtn.classList.add("delete-btn");

            wrapperContainer.appendChild(changeBtn);
            wrapperContainer.appendChild(deleteBtn);

            container.appendChild(infoContainer);
            container.appendChild(wrapperContainer);

            list.appendChild(container);
        });
        attachEventListeners();
    } catch (err) {
        console.error(err);
    }
}

function attachEventListeners() {
    const changeButtons = document.querySelectorAll('.change-btn');
    const deleteButtons = document.querySelectorAll('.delete-btn');

    changeButtons.forEach((changeButton) => {
        changeButton.addEventListener('click', (event) => {
            const taskElement = event.target.closest('.match');
            const host = taskElement.querySelector('p').textContent;
            const score = taskElement.querySelector('p:nth-child(2)').textContent;
            const guest = taskElement.querySelector('p:nth-child(3)').textContent;
            editTask(host, score, guest);
            enableEditBtn();
        });
    });


    deleteButtons.forEach((deleteButton) => {
        deleteButton.addEventListener('click', (event) => {
            const taskElement = event.target.closest('.match');
            const host = taskElement.querySelector('p').textContent;
            deleteTask(host);
        });
    });

}


function clearAllSections() {
    list.innerHTML = '';
}

async function editTask(taskHost, taskScore, taskGuest) {
    selectedTaskId = await getIdByLocation(taskHost);
    hostInput.value = taskHost;
    scoreInput.value = taskScore;
    guestInput.value = taskGuest;
}

function getIdByLocation(task) {
    return fetch(BASE_URL)
        .then(res => res.json())
        .then(res => Object.entries(res).find(e => e[1].host == task)[1]._id);
}

function enableEditBtn() {
    addBtn.disabled = true;
    editBtn.disabled = false;
}

function deleteTask(taskHost) {
    getIdByLocation(taskHost)
        .then((id) =>
            fetch(endpoints.delete(id), {
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json' },
            })
        )
        .then(() => {
            clearAllSections();
            loadBoardEventHandler();
            selectedTaskId = null;
            enableAddBtn();
        })
        .catch(console.error);
}

function clearAllInputs() {
    hostInput.value = '';
    scoreInput.value = '';
    guestInput.value = '';
}

function enableAddBtn() {
    addBtn.disabled = false;
    editBtn.disabled = true;
}

function createTaskEventHandler(ev) {
    ev.preventDefault();
    if (hostInput.value !== '' && scoreInput.value !== '' && guestInput.value !== '') {
        const headers = {
            method: 'POST',
            body: JSON.stringify({
                host: hostInput.value,
                score: scoreInput.value,
                guest: guestInput.value,
            }),
        };

        fetch(BASE_URL, headers)
            .then(loadBoardEventHandler)
            .catch(console.error);

        clearAllInputs();
    }
}


function editTaskEventHandler(ev) {
    ev.preventDefault();
    const taskLocation = hostInput.value;
    const data = {
        host: hostInput.value,
        score: scoreInput.value,
        guest: guestInput.value,
        _id: selectedTaskId,
    };

    fetch(endpoints.update(data._id), {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })
        .then(() => {
            clearAllInputs();
            loadBoardEventHandler();
            selectedTaskId = null;
            enableAddBtn();
        })
        .catch(console.error);
}

attachEvents();
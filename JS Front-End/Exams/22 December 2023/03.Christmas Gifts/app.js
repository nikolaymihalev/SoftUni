const BASE_URL = 'http://localhost:3030/jsonstore/gifts';

const endpoints = {
    update: (id) => `${BASE_URL}/${id}`,
    delete: (id) => `${BASE_URL}/${id}`,
};

const giftElement = document.getElementById("gift");
const priceElement = document.getElementById("price");
const forElement = document.getElementById("for");

const presentsList = document.getElementById("presents");
const list = document.getElementById('gift-list');

const addBtn = document.getElementById("add-present");
const editBtn = document.getElementById("edit-present");
const loadBtn = document.getElementById("load-presents");
const clearBtn = document.querySelector(".clear-btn");

let selectedTaskId = null;

function attachEvents() {
    loadBtn.addEventListener('click', loadBoardEventHandler);
    addBtn.addEventListener('click', createTaskEventHandler);
    editBtn.addEventListener('click', editTaskEventHandler);
}

function getIdByGift(gift) {
    return fetch(BASE_URL)
        .then(res => res.json())
        .then(res => Object.entries(res).find(e => e[1].gift == gift)[1]._id);
}

async function loadBoardEventHandler() {
    clearAllSections();
    try {
        const res = await fetch(BASE_URL);
        const allGifts = await res.json();
        Object.values(allGifts).forEach((gift) => {
            const container = document.createElement('div');
            container.className = 'gift-sock';

            const content = document.createElement('div');
            content.className = 'content';

            const figtElement = document.createElement('p');
            figtElement.textContent = gift.gift;

            const priceElement = document.createElement('p');
            priceElement.textContent = gift.price;

            const forElement = document.createElement('p');
            forElement.textContent = gift.for;
           

            const buttonsContainer = document.createElement('div'); 
            buttonsContainer.className = 'buttons-container';

            const changeBtn = document.createElement('button');
            changeBtn.className = 'change-btn';
            changeBtn.textContent = 'Change';

            const doneBtn = document.createElement('button');
            doneBtn.className = 'delete-btn';
            doneBtn.textContent = 'Delete';

            buttonsContainer.appendChild(changeBtn); 
            buttonsContainer.appendChild(doneBtn);

            content.appendChild(figtElement);
            content.appendChild(priceElement);
            content.appendChild(forElement);

            container.appendChild(content);
            container.appendChild(buttonsContainer); 

            list.appendChild(container);
        });
        attachEventListeners();
    } catch (err) {
        console.error(err);
    }
}

function attachEventListeners() {
    const changeButtons = document.querySelectorAll('.change-btn');
    const doneButtons = document.querySelectorAll('.delete-btn');

    changeButtons.forEach((changeButton) => {
        changeButton.addEventListener('click', (event) => {
            const taskElement = event.target.closest('.gift-sock');
            const gift = taskElement.querySelector('p').textContent;
            const price = taskElement.querySelector('p:nth-child(2)').textContent;
            const forE = taskElement.querySelector('p:nth-child(3)').textContent;
            editTask(gift, price, forE);
            enableEditBtn();
        });
    });
    

    doneButtons.forEach((doneButton) => {
        doneButton.addEventListener('click', (event) => {
            const taskElement = event.target.closest('.gift-sock');
            const gift = taskElement.querySelector('p').textContent;
            deleteTask(gift);
        });
    });
    
}

function enableEditBtn() {
    addBtn.disabled = true;
    editBtn.disabled = false;
}

function enableAddBtn() {
    addBtn.disabled = false;
    editBtn.disabled = true;
}

function createTaskEventHandler(ev) {
    ev.preventDefault();
    if (giftElement.value !== '' && forElement.value !== '' && priceElement.value !== '') {
        const headers = {
            method: 'POST',
            body: JSON.stringify({
                gift: giftElement.value,
                for: forElement.value,
                price: priceElement.value,
            }),
        };

        fetch(BASE_URL, headers)
            .then(loadBoardEventHandler)
            .catch(console.error);

        clearAllInputs();
    }
}

async function editTask(gift, priceGift, forGift) {
    selectedTaskId = await getIdByGift(gift);
    giftElement.value = gift;
    priceElement.value = priceGift;
    forElement.value = forGift;
}

function editTaskEventHandler(ev) {
    ev.preventDefault();
    const gift = giftElement.value;
    const data = {
        gift: giftElement.value,
        for: forElement.value,
        price: priceElement.value,
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

function deleteTask(taskLoacation) {
    getIdByGift(taskLoacation)
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

function clearAllSections() {
    list.innerHTML = '';
}

function clearAllInputs() {
    giftElement.value = '';
    forElement.value = '';
    priceElement.value = '';
}

attachEvents();

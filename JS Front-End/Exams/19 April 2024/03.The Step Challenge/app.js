const BASE_URL = 'http://localhost:3030/jsonstore/records/'

const listEl = document.getElementById('list');
const formEl = document.querySelector('#form form');

const addBtn = document.getElementById('add-record');
const editBtn = document.getElementById('edit-record');
const loadBtn = document.getElementById('load-records');

const nameInput = document.getElementById('p-name');
const stepsInput = document.getElementById('steps');
const caloriesInput = document.getElementById('calories');

loadBtn.addEventListener('click', loadRecords);
addBtn.addEventListener('click', addRecord);
editBtn.addEventListener('click', editRecord);


function clearInputs() {
    nameInput.value = '';
    stepsInput.value = '';
    caloriesInput.value = '';
}

async function loadRecords() {
    listEl.innerHTML = '';

    const response = await fetch(BASE_URL);
    const result = await response.json();
    const records = Object.values(result);

    const challenges = records.map(record => createElement(record.name, record.steps, record.calories, record._id));

    listEl.append(...challenges);
}

function createElement(name, steps, calories, _id) {
    const liEl = document.createElement('li');
    liEl.classList.add('record');

    const pNameEl = document.createElement('p');
    pNameEl.textContent = name;

    const pStepsEl = document.createElement('p');
    pStepsEl.textContent = steps;

    const pCaloriesEl = document.createElement('p');
    pCaloriesEl.textContent = calories;

    const infoDivEl = document.createElement('div');
    infoDivEl.classList.add('info');
    infoDivEl.appendChild(pNameEl);
    infoDivEl.appendChild(pStepsEl);
    infoDivEl.appendChild(pCaloriesEl);

    const changeBtn = document.createElement('button');
    changeBtn.classList.add('change-btn');
    changeBtn.textContent = 'Change';

    changeBtn.addEventListener('click', () => {
        nameInput.value = name
        stepsInput.value = steps
        caloriesInput.value = calories

        editBtn.removeAttribute('disabled');
        addBtn.setAttribute('disabled', 'disabled');

        formEl.setAttribute('data-record-id', _id);
    })

    const deleteBtn = document.createElement('button');
    deleteBtn.classList.add('delete-btn');
    deleteBtn.textContent = 'Delete';
    deleteBtn.addEventListener('click', async () => {
        await fetch(`${BASE_URL}/${_id}`, {
            method: 'DELETE',
        });

        await loadRecords();
    })

    const btnsContainer = document.createElement('div');
    btnsContainer.classList.add('btn-wrapper');

    btnsContainer.appendChild(changeBtn);
    btnsContainer.appendChild(deleteBtn);

    liEl.appendChild(infoDivEl);
    liEl.appendChild(btnsContainer);

    return liEl;
}

async function addRecord() {
    const name = nameInput.value;
    const steps = stepsInput.value;
    const calories = caloriesInput.value;

    clearInputs();

    const response = await fetch(BASE_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ name, steps, calories })
    }
    )

    await loadRecords();
}

async function editRecord() {
    const name = nameInput.value;
    const steps = stepsInput.value;
    const calories = caloriesInput.value;

    clearInputs();

    const _id = formEl.getAttribute('data-record-id');

    await fetch(`${BASE_URL}/${_id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ name, steps, calories, _id })
    });

    await loadRecords();

    editBtn.setAttribute('disabled', 'disabled');
    formEl.removeAttribute('data-record-id');
    addBtn.removeAttribute('disabled');

}
const BASE_URL = 'http://localhost:3030/jsonstore/tasks';

const endpoints = {
    update: (id) => `${BASE_URL}/${id}`,
    delete: (id) => `${BASE_URL}/${id}`,
};

const foodElement = document.getElementById("food");
const timeElement = document.getElementById("time");
const caloriesNumber = document.getElementById("calories");

const confirmedVacations = document.getElementById("history");
const list = document.getElementById('list');

const addBtn = document.getElementById("add-meal");
const editBtn = document.getElementById("edit-meal");
const loadBtn = document.getElementById("load-meals");


let selectedTaskId = null;

function attachEvents() {
    loadBtn.addEventListener('click', loadBoardEventHandler);
    addBtn.addEventListener('click', createTaskEventHandler);
    editBtn.addEventListener('click', editTaskEventHandler);
}

function getIdByLocation(task) {
    return fetch(BASE_URL)
        .then(res => res.json())
        .then(res => Object.entries(res).find(e => e[1].food == task)[1]._id);
}

async function loadBoardEventHandler() {
    clearAllSections();
    try {
        const res = await fetch(BASE_URL);
        const allTasks = await res.json();
        Object.values(allTasks).forEach((task) => {
            const container = document.createElement('div');
            container.className = 'meal';

            const foodElement = document.createElement('h2');
            foodElement.textContent = task.food;

            const timeElement = document.createElement('h3');
            timeElement.textContent = task.time ;

            const caloriesElement = document.createElement('h3');
            caloriesElement.textContent = task.calories ;
            

            const buttonsContainer = document.createElement('div'); 
            buttonsContainer.className = 'meal-buttons';

            const changeBtn = document.createElement('button');
            changeBtn.className = 'change-meal';
            changeBtn.textContent = 'Change';

            const doneBtn = document.createElement('button');
            doneBtn.className = 'delete-meal';
            doneBtn.textContent = 'Delete';

            buttonsContainer.appendChild(changeBtn); 
            buttonsContainer.appendChild(doneBtn);

            container.appendChild(foodElement);
            container.appendChild(timeElement);
            container.appendChild(caloriesElement);
            container.appendChild(buttonsContainer); 

            list.appendChild(container);
        });
        attachEventListeners();
    } catch (err) {
        console.error(err);
    }
}

function attachEventListeners() {
    const changeButtons = document.querySelectorAll('.change-meal');
    const doneButtons = document.querySelectorAll('.delete-meal');

    changeButtons.forEach((changeButton) => {
        changeButton.addEventListener('click', (event) => {
            const taskElement = event.target.closest('.meal');
            const location = taskElement.querySelector('h2').textContent;
            const date = taskElement.querySelector('h3:nth-child(2)').textContent;
            const temperature = taskElement.querySelector('h3:nth-child(3)').textContent;
            editTask(location, date, temperature);
            enableEditBtn();
        });
    });
    

    doneButtons.forEach((doneButton) => {
        doneButton.addEventListener('click', (event) => {
            const taskElement = event.target.closest('.meal');
            const location = taskElement.querySelector('h2').textContent;
            deleteTask(location);
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
    if (foodElement.value !== '' && caloriesNumber.value !== '' && timeElement.value !== '') {
        const headers = {
            method: 'POST',
            body: JSON.stringify({
                food: foodElement.value,
                calories: caloriesNumber.value,
                time: timeElement.value,
            }),
        };

        fetch(BASE_URL, headers)
            .then(loadBoardEventHandler)
            .catch(console.error);

        clearAllInputs();
    }
}

async function editTask(taskLocation, taskDate, taskTemperature) {
    selectedTaskId = await getIdByLocation(taskLocation);
    foodElement.value = taskLocation;
    timeElement.value = taskDate;
    caloriesNumber.value = taskTemperature;
}

function editTaskEventHandler(ev) {
    ev.preventDefault();
    const taskLocation = foodElement.value;
    const data = {
        food: foodElement.value,
        calories: caloriesNumber.value,
        time: timeElement.value,
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
    getIdByLocation(taskLoacation)
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
    foodElement.value = '';
    caloriesNumber.value = '';
    timeElement.value = '';
}

attachEvents();

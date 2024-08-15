const BASE_URL = 'http://localhost:3030/jsonstore/tasks';

const endpoints = {
    update: (id) => `${BASE_URL}/${id}`,
    delete: (id) => `${BASE_URL}/${id}`,
}
const nameElement = document.getElementById("name");
const dateElement = document.getElementById("from-date");
const daysNumber = document.getElementById("num-days");

const confirmedVacations = document.getElementById("confirmed-vacantions");
const list = document.getElementById('list');

const addBtn = document.getElementById("add-vacation");
const editBtn = document.getElementById("edit-vacation");
const loadBtn = document.getElementById("load-vacations");
const clearBtn = document.querySelector(".clear-btn");

function attachEvents() {
    loadBtn.addEventListener('click', loadBoardEventHandler);
    addBtn.addEventListener('click', (ev) => createTaskEventHandler(ev));
}

function getIdByName(task) {
    return fetch(BASE_URL)
        .then(res => res.json())
        .then(res => Object.entries(res).find(e => e[1].name == task)[1]._id)
}

async function loadBoardEventHandler() {
    clearAllSections();
    try {
        const res = await fetch(BASE_URL);
        const allTasks = await res.json();
        Object.values(allTasks)
            .forEach((task) => {
                list.innerHTML += `
                <div class="container">
                    <h2>${task.name}</h2>
                    <h3>${task.date}</h3>
                    <h3>${task.days}</h3>
                    <button class="change-btn">Change</button>
                    <button class="done-btn">Done</button>
                </div>`
                document.querySelector('.change-btn').addEventListener('click', (ev) => updateCourse(ev));
                document.querySelector('.done-btn').addEventListener('click', (ev) => deleteCourse(ev));
            })
    } catch (err) {
        console.error(err);
    }
}

function createTaskEventHandler(ev) {
    clearAllSections();

    ev.preventDefault();
    if (nameElement.value != '' && daysNumber.value != '' && dateElement.value != '') {

    }
    const headers = {
        method: 'POST',
        body: JSON.stringify({
            name: nameElement.value,
            days: daysNumber.value,
            date: dateElement.value,
        })
    };

    fetch(BASE_URL, headers)
        .then(loadBoardEventHandler)
        .catch(console.error);

    clearAllInputs();
}

async function updateCourse(ev) {
    ev.preventDefault();
    addBtn.disabled = true;
    editBtn.disabled = false;
    const tr = ev.target.parentElement;
    console.log()
    const [name, date, days] = Array.from(tr.children);
    nameElement.value = name.textContent;
    dateElement.value = date.textContent;
    daysNumber.value = days.textContent;


    editCourse(name.textContent);
}

function editCourse(task) {
    editBtn.addEventListener('click', (e) => {
        e.preventDefault();
        const data = { name: nameElement.value, days: daysNumber.value, date: dateElement.value };
        getIdByName(task)
            .then((id) => fetch(endpoints.update(id), {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    name: data.name,
                    days: data.days,
                    date: data.date,
                    _id: id
                })
            }))
            .then(() => {
                nameElement.value = '';
                daysNumber.value = '';
                dateElement.value = '';
            })
            .then(loadBoardEventHandler);
        addBtn.disabled = false;
        editBtn.disabled = true;
    })

}

function deleteCourse(ev) {
    const tr = ev.target.parentElement;
    const task = Array.from(tr.children)[0];

    getIdByName(task.textContent)
        .then((id) => fetch(endpoints.delete(id), {
            method: 'DELETE',
            headers: { 'content-type': 'application/json' }
        }))
        .then(loadBoardEventHandler);
}

function clearAllSections() {
    document.getElementById('list').innerHTML = '';
}

function clearAllInputs() {
    nameElement.value = '';
    daysNumber.value = '';
    dateElement.value = '';
}

attachEvents();

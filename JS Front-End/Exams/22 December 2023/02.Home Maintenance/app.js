window.addEventListener("load", solve);
function solve() {
    const addBtn = document.getElementById('add-btn');

    const taskList = document.getElementById('task-list');
    const doneList = document.getElementById('done-list');

    const placeInput = document.getElementById('place');
    const actionInput = document.getElementById('action');
    const personInput = document.getElementById('person');

    function createListElement(place, action, person) {
        const pPlaceEl = document.createElement('p');
        pPlaceEl.textContent = `Place:${place}`;

        const pActionEl = document.createElement('p');
        pActionEl.textContent = `Action:${action}`;

        const pPersonEl = document.createElement('p');
        pPersonEl.textContent = `Person:${person}`;

        const articleEl = document.createElement('article');

        articleEl.appendChild(pPlaceEl);
        articleEl.appendChild(pActionEl);
        articleEl.appendChild(pPersonEl);

        const editBtnEl = document.createElement('button');
        editBtnEl.classList.add('edit');
        editBtnEl.textContent = 'Edit';

        const doneBtnEl = document.createElement('button');
        doneBtnEl.classList.add('done');
        doneBtnEl.textContent = 'Done';

        const btnContainer = document.createElement('div');
        btnContainer.classList.add('buttons');

        btnContainer.appendChild(editBtnEl);
        btnContainer.appendChild(doneBtnEl);

        const liEl = document.createElement('li');

        liEl.appendChild(articleEl);
        liEl.appendChild(btnContainer);

        editBtnEl.addEventListener('click', () => {
            placeInput.value = place;
            actionInput.value = action;
            personInput.value = person;

            liEl.remove();
        });

        doneBtnEl.addEventListener('click', () => {
            btnContainer.remove();

            const deleteBtn = document.createElement('button');
            deleteBtn.classList.add('delete');
            deleteBtn.textContent = 'Delete'

            deleteBtn.addEventListener('click', () => {
                liEl.remove();
            })

            liEl.appendChild(deleteBtn);

            doneList.appendChild(liEl);
        })

        return liEl;
    }

    function clearInputs() {
        placeInput.value = '';
        actionInput.value = '';
        personInput.value = '';
    }

    addBtn.addEventListener('click', () => {
        const place = placeInput.value;
        const action = actionInput.value;
        const person = personInput.value;

        const liEl = createListElement(place, action, person);

        taskList.appendChild(liEl);

        clearInputs();
    })
}
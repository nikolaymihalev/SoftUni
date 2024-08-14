window.addEventListener("load", solve);

function solve() {
    const addBtn = document.getElementById('add-btn');
    const deleteBtn = document.querySelector("#expenses .delete");

    const expenseInput = document.getElementById('expense');
    const amountInput = document.getElementById('amount');
    const dateInput = document.getElementById('date');

    const previewList = document.getElementById('preview-list');
    const expensesList = document.getElementById('expenses-list');

    addBtn.addEventListener('click', () => {
        const expense = expenseInput.value;
        const amount = amountInput.value;
        const date = dateInput.value;

        const liEl = createLiEl(expense, amount, date);

        previewList.appendChild(liEl);

        clearInputs();
    })

    deleteBtn.addEventListener('click', () => {
        window.location.reload();
    })

    function clearInputs() {
        expenseInput.value = '';
        amountInput.value = '';
        dateInput.value = '';

        addBtn.setAttribute('disabled', 'disabled');
    }

    function createLiEl(expense, amount, date) {
        const pExpenseEl = document.createElement('p');
        pExpenseEl.textContent = `Type: ${expense}`;

        const pAmountEl = document.createElement('p');
        pAmountEl.textContent = `Amount: ${amount}$`;

        const pDateEl = document.createElement('p');
        pDateEl.textContent = `Date: ${date}`;

        const articleEl = document.createElement('article');

        articleEl.appendChild(pExpenseEl);
        articleEl.appendChild(pAmountEl);
        articleEl.appendChild(pDateEl);

        const editBtn = document.createElement('button');
        editBtn.classList.add('btn');
        editBtn.classList.add('edit');
        editBtn.textContent = 'edit';

        const okBtn = document.createElement('button');
        okBtn.classList.add('btn');
        okBtn.classList.add('ok');
        okBtn.textContent = 'ok';

        const btnsEl = document.createElement('div');
        btnsEl.classList.add('buttons');

        btnsEl.appendChild(editBtn);
        btnsEl.appendChild(okBtn);

        editBtn.addEventListener('click', () => {
            expenseInput.value = expense;
            amountInput.value = amount;
            dateInput.value = date.toString("mm/dd/yyyy");

            addBtn.removeAttribute('disabled');

            liEl.remove();
        })

        okBtn.addEventListener('click', () => {
            addBtn.removeAttribute('disabled');

            btnsEl.remove();

            expensesList.appendChild(liEl);
        })

        const liEl = document.createElement('li');
        liEl.classList.add('expense-item');

        liEl.appendChild(articleEl);
        liEl.appendChild(btnsEl);

        return liEl;
    }
}
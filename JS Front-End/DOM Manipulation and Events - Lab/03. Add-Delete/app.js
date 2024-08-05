function addItem() {
    const itemsElement = document.getElementById('items');
    const newItemTextElement = document.getElementById('newItemText');

    const liElement = document.createElement('li');
    liElement.textContent = newItemTextElement.value;

    const deleteButton = document.createElement('a');
    deleteButton.textContent = '[Delete]';
    deleteButton.href = '#';

    deleteButton.addEventListener('click', (e) => {
        e.currentTarget.parentElement.remove();
    });

    liElement.append(deleteButton);

    itemsElement.appendChild(liElement);

    newItemTextElement.value = '';
}
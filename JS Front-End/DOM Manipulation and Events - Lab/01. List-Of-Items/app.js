function addItem() {
    const itemsElement = document.getElementById('items');
    const inputElement = document.getElementById('newItemText');

    const itemText = inputElement.value;
    const liElement = document.createElement('li');

    liElement.textContent = itemText;

    itemsElement.appendChild(liElement);

    inputElement.value = '';
}
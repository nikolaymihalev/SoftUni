function addItem() {
    function createOptionElement(text, value) {
        const option = document.createElement('option');
        option.textContent = text;
        option.value = value;

        return option;
    }

    const [textInputEl, valueInputEl] = document.querySelectorAll('input');

    document.getElementById('menu').appendChild(createOptionElement(textInputEl.value, valueInputEl.value));

    textInputEl.value = '';
    valueInputEl.value = '';
}
function deleteByEmail() {
    const customerTableElement = document.getElementById('customers');
    const inputElement = document.querySelector('input[type=text][name=email]');
    const resultElement = document.getElementById('result');

    const searchEmail = inputElement.value;

    const tdElements = customerTableElement.querySelectorAll('tbody td:last-child');

    const searchElement = Array.from(tdElements).find(el => el.textContent === searchEmail);

    if (searchElement) {
        searchElement.parentElement.remove();
        resultElement.textContent = 'Deleted.';
    } else {
        resultElement.textContent = 'Not found.';
    }
}
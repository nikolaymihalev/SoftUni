function focused() {
    const inputElements = document.querySelectorAll('input[type=text]');

    inputElements.forEach(el => el.addEventListener('focus', (e) => {
        e.currentTarget.parentElement.classList.add('focused');
    }));

    inputElements.forEach(el => el.addEventListener('blur', (e) => {
        e.currentTarget.parentElement.classList.remove('focused');
    }));
}
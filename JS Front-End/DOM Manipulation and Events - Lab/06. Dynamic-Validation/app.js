function validate() {
    const emailInput = document.getElementById('email');
    const pattern = /[a-z]+@[a-z]+\.[a-z]+/;
    
    emailInput.addEventListener('change', (e) => {
        if (!pattern.test(e.currentTarget.value)) {
            return e.currentTarget.classList.add('error');
        }

        e.currentTarget.classList.remove('error');
    })
}
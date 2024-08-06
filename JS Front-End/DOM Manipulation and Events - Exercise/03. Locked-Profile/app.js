function lockedProfile() {
    const buttonElements = document.querySelectorAll("div.profile button");

    function showModeClickHandler(event) {
        const button = event.target;

        const [, unlockInputEl] = event.target.parentElement.querySelectorAll('input');

        if (!unlockInputEl.checked)
            return;

        if (button.textContent === "Show more") {
            button.parentElement.querySelector("div").style.display = "none";
            button.textContent = "Hide it";
        } else {
            button.parentElement.querySelector('div').style.display = "block";
            button.textContent = "Show more";
        }
    }

    buttonElements.forEach((el) => {
        el.addEventListener('click', showModeClickHandler)
    });
}
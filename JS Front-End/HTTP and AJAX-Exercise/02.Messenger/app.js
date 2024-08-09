function attachEvents() {
    const BASE_URL = "http://localhost:3030/jsonstore/messenger";

    const submitEl = document.getElementById('submit');
    const refreshEl = document.getElementById('refresh');
    const messagesEl = document.getElementById('messages');

    const [authorInputEl, contentInputEl] = document.querySelectorAll("div#controls input[type='text']");

    function customFetch(url, options) {
        return fetch(url, options).then((res) => res.json());
    }

    function createMessageHandler() {
        customFetch(BASE_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                author: authorInputEl.value,
                content: contentInputEl.value
            }),
        }).then(() => {
            authorInputEl.value = '';
            contentInputEl.value = '';
        })
    }

    function appendMessages(messagesResponse) {
        messagesEl.textContent = Object.values(messagesResponse).map(({ author, content }) => `${author}: ${content}`).join("\n")
    }

    function getAllMessagesHandler() {
        customFetch(BASE_URL).then(appendMessages);
    }

    submitEl.addEventListener('click', createMessageHandler);
    refreshEl.addEventListener('click', getAllMessagesHandler);
}

attachEvents();
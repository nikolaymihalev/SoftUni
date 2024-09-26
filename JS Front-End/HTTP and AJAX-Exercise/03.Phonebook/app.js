function attachEvents() {
    const BASE_URL = "http://localhost:3030/jsonstore/phonebook";

    async function customFetch(url, options) {
        try {
            const data = await fetch(url, options).then((res) => res.json());
            return { data, error: null };
        } catch (error) {
            return { data: null, error };
        }
    }

    const phonebookEl = document.getElementById('phonebook');
    const loadBtnEl = document.getElementById('btnLoad');
    const createBtnEl = document.getElementById('btnCreate');

    const [personInputEl, phoneInputEl] = document.querySelectorAll('input');

    async function fetchAllContacts() {
        const { data, error } = await customFetch(BASE_URL);

        if (error)
            return;

        const posts = Object.values(data);

        appendAllContactElements(posts);
    }

    async function deleteContact(contact) {
        const options = {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json"
            },
        };

        const [data, error] = await customFetch(`${BASE_URL}/${contact._id}`, options)

        if(error)
            return;
    }

    function appendAllContactElements(posts) {
        phonebookEl.innerHTML = '';

        posts.forEach((contact) => {
            const { person, phone, _id } = contact;

            const liElement = document.createElement('li');
            liElement.textContent = `${person}: ${phone}`;

            const deleteBtnEl = document.createElement('button');
            deleteBtnEl.textContent = 'Delete';
            deleteBtnEl.addEventListener('click', () => deleteContact(contact));

            liElement.appendChild(deleteBtnEl);
            phonebookEl.appendChild(liElement);
        })
    }

    async function createContact(){
        const person = personInputEl.value;
        const phone = phoneInputEl.value;

        const options = {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                phone,
                person
            }),
        };

        const {data, error} = await customFetch(BASE_URL, options);

        if(error)
            return;


        personInputEl.value = '';
        phoneInputEl.value = '';
        fetchAllContacts();
    }

    loadBtnEl.addEventListener('click', fetchAllContacts);
    createBtnEl.addEventListener('click', createContact);

    fetchAllContacts();
}

attachEvents();
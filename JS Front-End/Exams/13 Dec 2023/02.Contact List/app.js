function solve() {
  const addBtn = document.getElementById('add-btn');
  const checkList = document.getElementById('check-list');
  const nameInput = document.getElementById('name');
  const phoneInput = document.getElementById('phone');
  const categoryInput = document.getElementById('category');
  const contactList = document.getElementById('contact-list');

  function createCheckListElement(name, phoneNumber, category) {
    const pNameEl = document.createElement('p');
    pNameEl.textContent = `name:${name}`;

    const pPhoneEl = document.createElement('p');
    pPhoneEl.textContent = `phone:${phoneNumber}`;

    const pCategoryEl = document.createElement('p');
    pCategoryEl.textContent = `category:${category}`;

    const articleEl = document.createElement('article');

    articleEl.appendChild(pNameEl);
    articleEl.appendChild(pPhoneEl);
    articleEl.appendChild(pCategoryEl);

    const editBtnEl = document.createElement('button');
    editBtnEl.classList.add('edit-btn');

    const saveBtnEl = document.createElement('button');
    saveBtnEl.classList.add('save-btn');

    const btnContainer = document.createElement('div');
    btnContainer.classList.add('buttons');

    btnContainer.appendChild(editBtnEl);
    btnContainer.appendChild(saveBtnEl);

    const liEl = document.createElement('li');

    liEl.appendChild(articleEl);
    liEl.appendChild(btnContainer);

    editBtnEl.addEventListener('click', () => {
      nameInput.value = name;
      phoneInput.value = phoneNumber;
      categoryInput.value = category;

      liEl.remove();
    });

    saveBtnEl.addEventListener('click', () => {
      btnContainer.remove();

      const deleteBtn = document.createElement('button');
      deleteBtn.classList.add('del-btn');

      deleteBtn.addEventListener('click', () => {
        liEl.remove();
      })

      liEl.appendChild(deleteBtn);

      contactList.appendChild(liEl);
    })

    return liEl;
  }

  function clearInputs() {
    nameInput.value = '';
    phoneInput.value = '';
    categoryInput.value = '';
  }

  addBtn.addEventListener('click', () => {
    const name = nameInput.value;
    const phone = phoneInput.value;
    const category = categoryInput.value;

    const liEl = createCheckListElement(name, phone, category);

    checkList.appendChild(liEl);

    clearInputs();
  })
}

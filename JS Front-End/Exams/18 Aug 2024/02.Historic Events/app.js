window.addEventListener("load", solve);

function solve() {
  const addBtn = document.getElementById('add-btn');

  const nameInput = document.getElementById('name');
  const timeInput = document.getElementById('time');
  const descriptionTextarea = document.getElementById('description');

  const previewList = document.getElementById("preview-list");
  const archiveList = document.getElementById("archive-list");

  addBtn.addEventListener('click', ()=>{
    const name = nameInput.value;
    const time = timeInput.value;
    const description = descriptionTextarea.value;

    nameInput.value = '';
    timeInput.value = '';
    descriptionTextarea.value = '';
    addBtn.setAttribute('disabled', 'disabled');

    const liEl = createLiElement(name, time, description);

    previewList.appendChild(liEl);
  })

  function createLiElement(name, time, description){
    const pNameEl = document.createElement('p');
    pNameEl.textContent = name;

    const pTimeEl = document.createElement('p');
    pTimeEl.textContent = time;

    const pDescriptionEl = document.createElement('p');
    pDescriptionEl.textContent = description;

    const articleEl = document.createElement('article');
    articleEl.appendChild(pNameEl);
    articleEl.appendChild(pTimeEl);
    articleEl.appendChild(pDescriptionEl);

    const editBtn = document.createElement('button');
    editBtn.textContent = 'Edit';
    editBtn.classList.add('edit-btn');

    const nextBtn = document.createElement('button');
    nextBtn.textContent = 'Next';
    nextBtn.classList.add('next-btn');

    editBtn.addEventListener('click', ()=>{
      nameInput.value = name;
      timeInput.value = time;
      descriptionTextarea.value = description;

      addBtn.removeAttribute('disabled');

      liEl.remove();
    })

    nextBtn.addEventListener('click', ()=>{
      buttonsEl.remove();

      const archiveBtn = document.createElement('button');
      archiveBtn.textContent = 'Archive';
      archiveBtn.classList.add('archive-btn');

      archiveBtn.addEventListener('click', ()=>{
        liEl.remove();

        addBtn.removeAttribute('disabled');
      })

      liEl.appendChild(archiveBtn);

      archiveList.appendChild(liEl);
    })

    const buttonsEl = document.createElement('div');
    buttonsEl.classList.add('buttons');
    buttonsEl.appendChild(editBtn);
    buttonsEl.appendChild(nextBtn);

    const liEl = document.createElement('li');
    liEl.appendChild(articleEl);
    liEl.appendChild(buttonsEl);

    return liEl;
  }
}
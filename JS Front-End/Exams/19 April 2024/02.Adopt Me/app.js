function solve() {
  const adoptBtn = document.getElementById('adopt-btn');
  const typeInput = document.getElementById('type');
  const ageInput = document.getElementById('age');
  const genderSelect = document.getElementById('gender');
  const adoptionInfoUl = document.getElementById('adoption-info');
  const adoptedListUl = document.getElementById('adopted-list');

  adoptBtn.addEventListener('click', () => {
    const type = typeInput.value;
    const age = ageInput.value;
    const gender = genderSelect.value;

    const liEl = Adopt(type, age, gender);

    adoptionInfoUl.appendChild(liEl);

    clearInputs();
  });

  function Adopt(type, age, gender) {
    const pTypeEl = document.createElement('p');
    pTypeEl.textContent = `Pet:${type}`;

    const pAgeEl = document.createElement('p');
    pAgeEl.textContent = `Age:${age}`;

    const pGenderEl = document.createElement('p');
    pGenderEl.textContent = `Gender:${gender}`;

    const articleEl = document.createElement('article');
    articleEl.appendChild(pTypeEl);
    articleEl.appendChild(pGenderEl);
    articleEl.appendChild(pAgeEl);

    const editBtn = document.createElement('button');
    editBtn.classList.add('edit-btn');
    editBtn.textContent = 'Edit';

    const doneBtn = document.createElement('button');
    doneBtn.classList.add('done-btn');
    doneBtn.textContent = 'Done';

    const btnsContainer = document.createElement('div');
    btnsContainer.classList.add('buttons');

    btnsContainer.appendChild(editBtn);
    btnsContainer.appendChild(doneBtn);

    const liEl = document.createElement('li');

    liEl.appendChild(articleEl);
    liEl.appendChild(btnsContainer);

    editBtn.addEventListener('click', () => {
      typeInput.value = type;
      ageInput.value = age;
      genderSelect.value = gender;

      liEl.remove();
    })

    doneBtn.addEventListener('click', () => {
      btnsContainer.remove();

      const clearBtn = document.createElement('button');
      clearBtn.classList.add('clear-btn');
      clearBtn.textContent = 'Clear'

      clearBtn.addEventListener('click', () => {
        liEl.remove();
      })

      liEl.appendChild(clearBtn);

      adoptedListUl.appendChild(liEl);
    })

    return liEl;
  }

  function clearInputs() {
    typeInput.value = '';
    ageInput.value = '';
    genderSelect.value = '';
  }
}

function attachEvents() {
  const BASE_URL = "http://localhost:3030/jsonstore/collections/students";

  const [firstNameInputEl, lastNameInputEl, facultyNumberInputEl, gradeInputEl] = document.querySelectorAll(".inputs input[type='text']");

  const tableBodyEl = document.querySelector("#results tbody");
  const submitBtnEl = document.getElementById('submit');

  async function customFetch(url, options) {
    return fetch(url, options).then((res) => {
      if (!res.ok) {
        throw Error(res.message);
      }

      return res.json();
    })
      .then((data) => ({ data, error: null }))
      .catch((error) => ({ data: null, error }));
  }

  function appendStudents(students) {
    tableBodyEl.innerHTML = '';
    students.forEach((student) => {
      const trEl = document.createElement('tr');

      Object.keys(student).filter((key) => key !== "_id").map((key) => {
        const tdEl = document.createElement('td');

        tdEl.textContent = student[key];

        trEl.appendChild(tdEl);
      });

      tableBodyEl.appendChild(trEl);
    });
  }

  function fetchAllStudents() {
    customFetch(BASE_URL).then(({ data, error }) => {
      if (error)
        return;

      appendStudents(Object.values(data));
    });
  }

  function createStudentHandler() {
    const firstName = firstNameInputEl.value;
    const lastName = lastNameInputEl.value;
    const facultyNumber = facultyNumberInputEl.value;
    const grade = gradeInputEl.value;

    if (!firstName || !lastName || !facultyNumber || !grade) {
      return;
    }

    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        firstName,
        lastName,
        facultyNumber,
        grade
      }),
    };

    customFetch(BASE_URL, options).then(({ data, error }) => {
      if (error)
        return;

      firstNameInputEl.value = '';
      lastNameInputEl.value = '';
      facultyNumberInputEl.value = '';
      gradeInputEl.value = '';

      fetchAllStudents();
    })
  }

  submitBtnEl.addEventListener('click', createStudentHandler);
  fetchAllStudents();
}

attachEvents();
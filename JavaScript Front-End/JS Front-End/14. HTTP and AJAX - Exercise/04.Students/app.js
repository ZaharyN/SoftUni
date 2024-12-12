function attachEvents() {

  const baseUrl = 'http://localhost:3030/jsonstore/collections/students';

  const tbodyEl = document.querySelector('#results tbody');
  const submitButton = document.querySelector('#submit');
  const firstNameInput = document.querySelector('input[name="firstName"]');
  const lastNameInput = document.querySelector('input[name="lastName"]');
  const facultyNumberInput = document.querySelector('input[name="facultyNumber"]');
  const gradeInput = document.querySelector('input[name="grade"]');

  submitButton.addEventListener('click', addStudent);
  getStudents();

  function getStudents(){

    tbodyEl.innerHTML = '';

    fetch(baseUrl)
    .then(result => result.json())
    .then(students => {

      Object.values(students).forEach(students => {

        const rowEl = document.createElement('tr');

        rowEl.appendChild(createTableElement(students.firstName));
        rowEl.appendChild(createTableElement(students.lastName));
        rowEl.appendChild(createTableElement(students.facultyNumber));
        rowEl.appendChild(createTableElement(students.grade));

        tbodyEl.appendChild(rowEl);

      });
    })
    .catch(err => console.log(err));
  }

  function addStudent(e) {

    e.preventDefault();

    if (!firstNameInput.value.trim()
      || !lastNameInput.value.trim()
      || !facultyNumberInput.value.trim()
      || !gradeInput.value.trim()
    ) { return; }

    const body = {
      firstName: firstNameInput.value,
      lastName: lastNameInput.value,
      facultyNumber: facultyNumberInput.value,
      grade: gradeInput.value
    }

    fetch(baseUrl, {
      method: "POST",
      body: JSON.stringify(body)
    })
      .then(result => result.json())
      .then(data => {

        firstNameInput.value = '';
        lastNameInput.value = '';
        facultyNumberInput.value = '';
        gradeInput.value = '';
        getStudents();

      })
      .catch(err => console.log(err));
  }

  function createTableElement(content){
     const tdEL = document.createElement('td');
     tdEL.textContent = content;
     return tdEL;
  }
}

attachEvents();
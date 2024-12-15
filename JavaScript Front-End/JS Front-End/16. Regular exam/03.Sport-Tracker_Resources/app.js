const baseUrl = 'http://localhost:3030/jsonstore/workout/';

const workoutInput = document.querySelector('input#workout');
const locationInput = document.querySelector('input#location');
const dateInput = document.querySelector('input#date');

const addButton = document.querySelector('button#add-workout');
const editButton = document.querySelector('button#edit-workout');

const loadWorkoutButton = document.querySelector('#load-workout');
const sportList = document.querySelector('#list');

loadWorkoutButton.addEventListener('click', (e) => {

    sportList.innerHTML = '';

    fetch(baseUrl)
        .then(result => result.json())
        .then(workouts => {

            Object.values(workouts).forEach(workoutInfo => {

                let [workout, location, date, _id] = Object.values(workoutInfo);

                const parentDiv = createElement('div', { className: 'container', dataset: { workout, location, date, _id } }, sportList);
                createElement('h2', { textContent: workout }, parentDiv);
                createElement('h3', { textContent: date }, parentDiv);
                createElement('h3', { id: 'location', textContent: location }, parentDiv);
                const buttonDiv = createElement('div', { id: 'buttons-container' }, parentDiv);
                createElement('button', { className: 'change-btn', textContent: 'Change', onclick: changeButtonHandler }, buttonDiv);
                createElement('button', { className: 'delete-btn', textContent: 'Delete', onclick: deleteButtonHandler }, buttonDiv);
            });

            editButton.disabled = true;
        })
        .catch(err => console.log(err));
});

addButton.addEventListener('click', (e) => {

    e.preventDefault();

    if (!workoutInput.value
        || !locationInput.value
        || !dateInput.value
    ) { return; }

    const body = { workout: workoutInput.value, location: locationInput.value, date: dateInput.value };

    fetch(baseUrl, {
        method: "POST",
        body: JSON.stringify(body)
    })
        .then(result => result.json())
        .then(data => {

            loadWorkoutButton.click();
            
        })
        .catch(err => console.log(err));
        clearInputs();
});

editButton.addEventListener('click', (e) => {

    e.preventDefault();

    if (!workoutInput.value
        || !locationInput.value
        || !dateInput.value
    ) { return; }

    const id = sportList.querySelector('.active').dataset._id;
    console.log(id);
    
    const body = { workout: workoutInput.value, location: locationInput.value, date: dateInput.value, _id: id };

    fetch(baseUrl + id, {
        method: "PUT",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    })
        .then(result => result.json())
        .then(data => {

            clearInputs();
            loadWorkoutButton.click();
            editButton.disabled = true;
            addButton.disabled = false;
        })
        .catch(err => console.log(err));
});

function changeButtonHandler(e) {

    e.preventDefault();

    const currentDivEl = e.target.closest('div.container');

    workoutInput.value = currentDivEl.dataset.workout;
    locationInput.value = currentDivEl.dataset.location;
    dateInput.value = currentDivEl.dataset.date;

    editButton.disabled = false;
    addButton.disabled = true;
    currentDivEl.classList.add('active');
    //currentDivEl.remove();
    editButton.disabled = false;
    addButton.disabled = true;
}

function deleteButtonHandler(e) {

    e.preventDefault();

    const currentDiv = e.target.closest('div.container');
    console.log(currentDiv);

    const workoutId = currentDiv.dataset._id;

    fetch(baseUrl + workoutId, {
        method: "DELETE",
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(result => result.json())
        .then(data => {

            loadWorkoutButton.click();
        })
        .catch(err => console.log(err));
}

function clearInputs() {

    workoutInput.value = '';
    locationInput.value = '';
    dateInput.value = '';

}

function createElement(tag, attributes, parentContainer) {

    const element = document.createElement(tag);

    Object.keys(attributes).forEach(key => {
        if (typeof attributes[key] === 'object') {
            Object.assign(element[key], attributes[key]);
        } else {
            element[key] = attributes[key];
        }
    })

    if (parentContainer) parentContainer.appendChild(element);

    return element;
}
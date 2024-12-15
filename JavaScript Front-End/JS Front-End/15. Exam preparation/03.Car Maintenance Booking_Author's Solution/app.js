const BASE_URL = 'http://localhost:3030/jsonstore/appointments';

const endpoints = {
    update: (id) => `${BASE_URL}/${id}`,
    delete: (id) => `${BASE_URL}/${id}`,
};

const modelElement = document.getElementById("car-model");
const dateElement = document.getElementById("date");
const serviceElement = document.getElementById("car-service");

const confirmedVacations = document.getElementById("appointments");
const list = document.getElementById('appointments-list');

const addBtn = document.getElementById("add-appointment");
const editBtn = document.getElementById("edit-appointment");
const loadBtn = document.getElementById("load-appointments");

let selectedTaskId = null;

function attachEvents() {
    loadBtn.addEventListener('click', loadBoardEventHandler);
    addBtn.addEventListener('click', createTaskEventHandler);
    editBtn.addEventListener('click', editTaskEventHandler);
}

function getIdBymodel(task) {
    return fetch(BASE_URL)
        .then(res => res.json())
        .then(res => Object.entries(res).find(e => e[1].model == task)[1]._id);
}

async function loadBoardEventHandler() {
    clearAllSections();
    try {
        const res = await fetch(BASE_URL);
        const allTasks = await res.json();
        Object.values(allTasks).forEach((task) => {
            const appointment = document.createElement('li');
            appointment.className = 'appointment';

            const modelElement = document.createElement('h2');
            modelElement.textContent = task.model;

            const dateElement = document.createElement('h3');
            dateElement.textContent = task.date;

            const serviceElement = document.createElement('h3');
            serviceElement.textContent = task.service;


            const buttonsappointment = document.createElement('div'); // Create the new div for buttons
            buttonsappointment.className = 'buttons-appointment';

            const changeBtn = document.createElement('button');
            changeBtn.className = 'change-btn';
            changeBtn.textContent = 'Change';

            const doneBtn = document.createElement('button');
            doneBtn.className = 'delete-btn';
            doneBtn.textContent = 'Delete';

            buttonsappointment.appendChild(changeBtn); // Append buttons to the new div
            buttonsappointment.appendChild(doneBtn);

            appointment.appendChild(modelElement);
            appointment.appendChild(dateElement);
            appointment.appendChild(serviceElement);
            appointment.appendChild(buttonsappointment); // Append the new div to the appointment

            list.appendChild(appointment);
        });
        attachEventListeners();
    } catch (err) {
        console.error(err);
    }
}

function attachEventListeners() {
    const changeButtons = document.querySelectorAll('.change-btn');
    const doneButtons = document.querySelectorAll('.delete-btn');

    changeButtons.forEach((changeButton) => {
        changeButton.addEventListener('click', (event) => {
            const taskElement = event.target.closest('.appointment');
            const model = taskElement.querySelector('h2').textContent;
            const date = taskElement.querySelector('h3:nth-child(2)').textContent;
            const service = taskElement.querySelector('h3:nth-child(3)').textContent;
            editTask(model, date, service);
            enableEditBtn();
        });
    });
    

    doneButtons.forEach((doneButton) => {
        doneButton.addEventListener('click', (event) => {
            const taskElement = event.target.closest('.appointment');
            const model = taskElement.querySelector('h2').textContent;
            deleteTask(model);
        });
    });
    
}

function enableEditBtn() {
    addBtn.disabled = true;
    editBtn.disabled = false;
}

function enableAddBtn() {
    addBtn.disabled = false;
    editBtn.disabled = true;
}

function createTaskEventHandler(ev) {
    ev.preventDefault();
    if (modelElement.value !== '' && serviceElement.value !== '' && dateElement.value !== '') {
        const headers = {
            method: 'POST',
            body: JSON.stringify({
                model: modelElement.value,
                service: serviceElement.value,
                date: dateElement.value,
            }),
        };

        fetch(BASE_URL, headers)
            .then(loadBoardEventHandler)
            .catch(console.error);

        clearAllInputs();
    }
}

async function editTask(taskmodel, taskDate, taskservice) {
    selectedTaskId = await getIdBymodel(taskmodel);
    modelElement.value = taskmodel;
    dateElement.value = taskDate;
    serviceElement.value = taskservice;
}

function editTaskEventHandler(ev) {
    ev.preventDefault();
    const taskmodel = modelElement.value;
    const data = {
        model: modelElement.value,
        service: serviceElement.value,
        date: dateElement.value,
        _id: selectedTaskId,
    };

    fetch(endpoints.update(data._id), {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })
        .then(() => {
            clearAllInputs();
            loadBoardEventHandler();
            selectedTaskId = null;
            enableAddBtn();
        })
        .catch(console.error);
}

function deleteTask(taskLoacation) {
    getIdBymodel(taskLoacation)
        .then((id) =>
            fetch(endpoints.delete(id), {
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json' },
            })
        )
        .then(() => {
            clearAllSections();
            loadBoardEventHandler();
            selectedTaskId = null;
            enableAddBtn();
        })
        .catch(console.error);
}

function clearAllSections() {
    list.innerHTML = '';
}

function clearAllInputs() {
    modelElement.value = '';
    serviceElement.value = '';
    dateElement.value = '';
}

attachEvents();

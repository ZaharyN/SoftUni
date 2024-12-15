const baseUrl = 'http://localhost:3030/jsonstore/appointments/';

const loadAppointmentsBtn = document.querySelector('#load-appointments');
const addAppointmentBtn = document.querySelector('#add-appointment');
const editAppointmentBtn = document.querySelector('#edit-appointment');
const appointmentList = document.querySelector('#appointments-list');

const carModelInput = document.querySelector('input#car-model');
const serviceInput = document.querySelector('select#car-service');
const dateInput = document.querySelector('input#date');

loadAppointmentsBtn.addEventListener('click', loadAppointments);

function loadAppointments() {

    appointmentList.innerHTML = '';

    fetch(baseUrl)
        .then(result => result.json())
        .then(appointments => {

            Object.values(appointments).forEach(app => {

                let [model, service, date, _id] = Object.values(app);

                const liEl = createElement('li', { className: 'appointment', dataset: { model, service, date, _id } });
                createElement('h2', { textContent: model }, liEl);
                createElement('h3', { textContent: date }, liEl);
                createElement('h3', { textContent: service }, liEl);
                const buttonDiv = createElement('div', { className: 'buttons-appointment' }, liEl);
                createElement('button', { className: "change-btn", onclick: changeAppointmentHandler, textContent: 'Change' }, buttonDiv);
                createElement('button', { className: "delete-btn", onclick: deleteAppointmentHandler, textContent: 'Delete' }, buttonDiv);
                appointmentList.appendChild(liEl);
            });
            addAppointmentBtn.disabled = true;
            editAppointmentBtn.disabled = false;
        })
        .catch(err => console.log(err));
}

addAppointmentBtn.addEventListener('click', (e) => {

    e.preventDefault();

    if (!carModelInput.value
        || !serviceInput.value
        || !dateInput.value
    ) { return; }

    const body = { model: carModelInput.value, service: serviceInput.value, date: dateInput.value };

    fetch(baseUrl, {
        method: "POST",
        body: JSON.stringify(body)
    })
        .then(result => result.json())
        .then(data => {

            carModelInput.value = '';
            serviceInput.value = '';
            dateInput.value = '';
            loadAppointments();

        })
        .catch(err => console.log(err));
});

editAppointmentBtn.addEventListener('click', (e) => {

    e.preventDefault();

    if (!carModelInput.value
        || !serviceInput.value
        || !dateInput.value
    ) { return; }

    const id = appointmentList.querySelector('.active').dataset._id;

    const body = { model: carModelInput.value, service: serviceInput.value, date: dateInput.value, _id: id };

    fetch(baseUrl + id, {
        method: "PUT",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    })
        .then(result => result.json())
        .then(data => {

            editAppointmentBtn.disabled = true;
            addAppointmentBtn.disabled = false;

            carModelInput.value = '';
            serviceInput.value = '';
            dateInput.value = '';

            loadAppointments();
        })
        .catch(err => console.log(err));
});


function changeAppointmentHandler(e) {

    const currentLiEl = e.target.closest('li');

    carModelInput.value = currentLiEl.dataset.model;
    serviceInput.value = currentLiEl.dataset.service;
    dateInput.value = currentLiEl.dataset.date;

    currentLiEl.classList.add('active');
    editAppointmentBtn.disabled = false;
    addAppointmentBtn.disabled = true;
}


function deleteAppointmentHandler(e) {

    const currentLiEl = e.target.closest('li');
    const appointmentId = currentLiEl.dataset._id;

    fetch(baseUrl + appointmentId, {
        method: "DELETE",
        headers: {
            'Content-Type': 'application/json'}
    })
        .then(result => result.json())
        .then(data => {

            loadAppointments();

        })
        .catch(err => console.log(err));
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
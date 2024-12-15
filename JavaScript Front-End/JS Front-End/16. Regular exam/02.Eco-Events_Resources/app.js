window.addEventListener("load", solve);

function solve() {

  const emailInput = document.querySelector('input#email');
  const eventInput = document.querySelector('input#event');
  const locationInput = document.querySelector('input#location');
  const nextButton = document.querySelector('#next-btn');

  const previewList = document.querySelector('#preview-list');
  const eventList = document.querySelector('#event-list');

  nextButton.addEventListener('click', (e) => {

    e.preventDefault();
    
    if (!emailInput.value
      || !eventInput.value
      || !locationInput.value
    ) { return; }

    const liEl = createElement('li', { className: 'application' }, previewList);
    const articleEl = createElement('article', {}, liEl);
    createElement('h4', { textContent: emailInput.value }, articleEl);
    const eventPEl = document.createElement('p');
    eventPEl.innerHTML = `<strong>Event:</strong><br>${eventInput.value}`;
    articleEl.appendChild(eventPEl);
    const locationPEL = document.createElement('p');
    locationPEL.innerHTML = `<strong>Location:</strong><br>${locationInput.value}`;
    articleEl.appendChild(locationPEL);
    const editButton = createElement('button', { className: 'action-btn edit', textContent: 'EDIT' }, liEl);
    const applyButon = createElement('button', { className: 'action-btn apply', textContent: 'APPLY' }, liEl);

    clearInputs();
    nextButton.disabled = true;

    editButton.addEventListener('click', editButtonHandler);
    applyButon.addEventListener('click', applyButtonHandler);
  });

  function editButtonHandler(e) {

    e.preventDefault();
    const liEl = document.querySelector('li.application');
    const email = liEl.querySelector('h4').textContent.trim();
    const paragraphs = liEl.querySelectorAll('p');
    let event = paragraphs[0].textContent.trim(); 
    let location = paragraphs[1].textContent.trim();

    event = event.split(':')[1];
    location = location.split(':')[1];

    emailInput.value = email;
    eventInput.value = event;
    locationInput.value = location;

    liEl.remove();
    nextButton.disabled = false;
  }

  function applyButtonHandler(e) {

    e.preventDefault();
    
    const liEvent = document.querySelector('li.application');
    eventList.appendChild(liEvent);
    
    const buttons = [...eventList.querySelectorAll('button')];

    buttons.forEach(b => b.remove());
    
    nextButton.disabled = false;
  }

  function clearInputs() {
    emailInput.value = '';
    eventInput.value = '';
    locationInput.value = '';
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
}
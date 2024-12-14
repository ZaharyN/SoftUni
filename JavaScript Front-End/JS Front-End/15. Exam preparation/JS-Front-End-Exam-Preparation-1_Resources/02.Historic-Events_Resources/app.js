window.addEventListener("load", solve);

function solve() {

  const formEl = document.querySelector('form');
  const nameInput = document.querySelector('input#name');
  const datetimeInput = document.querySelector('input#time');
  const descriptionInput = document.querySelector('textarea#description');
  const addButton = document.querySelector('button#add-btn');
  const previewList = document.querySelector('#preview-list');
  const archiveList = document.querySelector('#archive-list');

  formEl.addEventListener('submit', (e) => {

    e.preventDefault();

    if (!nameInput.value
      || !datetimeInput.value
      || !descriptionInput.value
    ) { return; }

    const liEl = createElement('li');
    const articleEl = createElement('article');
    createElement('p', { textContent: nameInput.value }, articleEl);
    createElement('p', { textContent: datetimeInput.value }, articleEl);
    createElement('p', { textContent: descriptionInput.value }, articleEl);

    liEl.appendChild(articleEl);
    const buttonsDiv = createElement('div', { class: 'buttons' }, liEl);
    createElement('button', { class: 'edit-btn', onclick: editButtonHandler, textContent: 'Edit' }, buttonsDiv);
    createElement('button', { class: 'next-btn', onclick: nextButtonHandler, textContent: 'Next' }, buttonsDiv);
    previewList.appendChild(liEl);

    addButton.disabled = true;
    nameInput.value = '';
    datetimeInput.value = '';
    descriptionInput.value = '';
  })

  function editButtonHandler(e) {

    e.preventDefault();
    listParagraphs = [...document.querySelectorAll('#preview-list p')];
    nameInput.value = listParagraphs[0].textContent;
    datetimeInput.value = listParagraphs[1].textContent;
    descriptionInput.value = listParagraphs[2].textContent;

    addButton.disabled = false;
    previewList.innerHTML = '';
  }

  function nextButtonHandler(e) {

    e.preventDefault();
    archiveList.appendChild(document.querySelector('#preview-list li'));
    archiveList.querySelector('.buttons').remove();
    const archiveButton = createElement('button', { class: 'archive-btn', textContent: 'Archive', onclick: archiveButtonHandler });
    archiveList.querySelector('li').appendChild(archiveButton);
  }

  function archiveButtonHandler(e) {

    e.preventDefault();
    archiveList.querySelector('li').remove();
    addButton.disabled = false;
  }

  function createElement(tag, attributes, parentContainer) {

    const element = document.createElement(tag);

    if (attributes) {
      for (const [key, value] of Object.entries(attributes)) {
        if (key === 'object') {
          Object.assign(element[key], value);
        }
        else if (key === 'class') {
          element.className = value;
        }
        else {
          element[key] = value;
        }
      }
    }
    if (parentContainer) parentContainer.appendChild(element);

    return element;
  }
}
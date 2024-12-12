function attachEvents() {

    const getPhonebooksUrl = 'http://localhost:3030/jsonstore/phonebook';
    const createPhonebookUrl = 'http://localhost:3030/jsonstore/phonebook';
    const deletePhonebookUrl = 'http://localhost:3030/jsonstore/phonebook/';

    const phoneBookList = document.querySelector('#phonebook');
    const loadButton = document.querySelector('#btnLoad');
    const personInput = document.querySelector('input#person');
    const phoneInput = document.querySelector('input#phone');
    const createButton = document.querySelector('#btnCreate');

    loadPhonebook(getPhonebooksUrl, (result) => {

        Object.values(result).forEach(pd => {

            const liEl = document.createElement('li');
            liEl.textContent = `${pd.person}: ${pd.phone}`;

            const btnEl = document.createElement('button');
            btnEl.textContent = 'Delete';
            liEl.append(btnEl);
            phoneBookList.appendChild(liEl);
            Object.assign(liEl.dataset, { _id: pd._id });

            btnEl.addEventListener('click', () => handleDelete(pd._id));
        });
    });

    createButton.addEventListener('click', handleCreate);

    function handleCreate(e) {

        if (!personInput.value) return;
        if (!phoneInput.value) return;

        const body = { person: personInput.value, phone: phoneInput.value };

        fetch(createPhonebookUrl, {
            method: "POST",
            body: JSON.stringify(body)
        })
        .then(result => result.json())
        .then(data => {

            personInput.value ='';
            phoneInput.value ='';
            loadButton.click();
            
        })
        .catch(err => console.log(err));

    };

    function handleDelete(id) {

        fetch(deletePhonebookUrl + id)
            .then(result => result.json())
            .then(data => {

                phoneBookList.querySelector(`li[data-_id="${data._id}"]`).remove();

            })
            .catch(err => console.log(err));
    };
}

function loadPhonebook(baseUrl, onSuccess) {

    fetch(baseUrl)
        .then(result => result.json())
        .then(onSuccess)
        .catch(err => console.log(err));
}

attachEvents();
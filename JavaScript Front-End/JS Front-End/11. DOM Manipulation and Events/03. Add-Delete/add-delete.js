function addItem() {
    const ul = document.getElementById('items');
    let newText = document.getElementById('newItemText');

    if (newText.value == '') {
        return;
    }

    let li = document.createElement('li');
    let deleteEl = document.createElement('a');

    li.textContent = newText.value;

    deleteEl.setAttribute('href', '#');
    deleteEl.textContent = '[Delete]';
    deleteEl.addEventListener('click', (e) => {
        e.currentTarget.parentElement.remove();
    });

    li.appendChild(deleteEl);

    ul.appendChild(li);
    newText.value = '';
}
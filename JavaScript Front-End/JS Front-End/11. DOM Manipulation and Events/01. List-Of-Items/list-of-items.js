function addItem() {
    const ul = document.getElementById('items');
    let newText = document.getElementById('newItemText');

    if (newText.value == '') {
        return;
    }

    let li = document.createElement('li');
    li.textContent = newText.value;
    ul.appendChild(li);
    newText.value = '';
}
function deleteByEmail() {
    let rows = document.querySelectorAll('#customers tbody tr');
    let searchEmail = document.querySelector('label input[type="text"]');
    let resultString = document.querySelector('#result');
    let found = false;

    rows.forEach(r => {

        if (r.children[1].textContent == searchEmail.value) {
            console.log('FOund');

            r.parentNode.removeChild(r);
            found = true;
        }
    })

    if (found) {
        resultString.textContent = 'Deleted.';
    } else {
        resultString.textContent = 'Not found.';
    }
}
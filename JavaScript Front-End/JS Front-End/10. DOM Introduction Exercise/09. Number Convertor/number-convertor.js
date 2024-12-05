function solve() {
    const inputNumber = Number(document.querySelector('#input').value);
    const convertOption = document.querySelector('#selectMenuTo').value.toLowerCase();
    const result = document.querySelector('#result');

    switch (convertOption) {
        case 'binary':
            result.value = inputNumber.toString(2);
            break;
        case 'hexadecimal':
            result.value = inputNumber.toString(16).toUpperCase();
            break;
        default:
            return;
    }
}
document.addEventListener('DOMContentLoaded', solve);

function solve() {
    const formEl = document.querySelector('form');
    const textInputEl = document.querySelector('input#newItemText');
    const valueInputEl = document.querySelector('input#newItemValue');
    const menuList = document.querySelector('#menu');

    formEl.addEventListener('submit', (e) => {
        e.preventDefault();
        
        const optionText = textInputEl.value;
        if( optionText == '') return;

        const optionValue = valueInputEl.value;
        if(optionValue == '') return;

        const optionEl = document.createElement('option');
        optionEl.value = optionValue;
        optionEl.textContent = optionText;
        menuList.appendChild(optionEl);

        e.target.reset();
    });
}
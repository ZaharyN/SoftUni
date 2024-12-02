function toggle() {
    const span = document.querySelector('#accordion .button');
    const divEl = document.querySelector('#extra');

    if (divEl.style.display == 'block') {
        divEl.style.display = 'none';
        span.textContent = 'More';
    }
    else {
        divEl.style.display = 'block';
        span.textContent = 'Less';
    }
}
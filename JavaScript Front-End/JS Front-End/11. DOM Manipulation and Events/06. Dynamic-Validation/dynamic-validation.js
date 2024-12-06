document.addEventListener('DOMContentLoaded', solve);

function solve() {

    const emailPattern = /^[a-z]+@[a-z]+\.[a-z]{2,63}$/;

    function validatePassword(e) {
        const text = e.currentTarget;

        if (!emailPattern.test(text.value)) {
            text.classList.add('error');
        } else {
            text.classList.remove('error');
        }
    }

    let input = document.querySelector('input[type="text"]');

    input.addEventListener('change', validatePassword);
}
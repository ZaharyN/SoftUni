document.addEventListener('DOMContentLoaded', solve);

function solve() {
    const inputs = document.querySelectorAll('input[type="text"]');

    inputs.forEach(s => {
        s.addEventListener('focus', (e) => {
            const selectedSection = e.currentTarget.parentElement;
            selectedSection.classList.add('focused');
        })
        s.addEventListener('blur', (e) => {
            e.currentTarget.parentElement.classList.remove('focused');
        })
    })
}
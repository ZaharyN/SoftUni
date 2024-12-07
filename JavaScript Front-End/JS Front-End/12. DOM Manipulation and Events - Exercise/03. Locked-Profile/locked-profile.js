document.addEventListener('DOMContentLoaded', solve);

function solve() {

    document.querySelector('main').addEventListener('click', (e) => {

        if (e.target.nodeName != 'BUTTON') return;

        const profileEl = e.target.closest('.profile');
        const state = profileEl.querySelector('#radio-group input:checked').getAttribute('id');

        if (state.includes('Lock')) return;

        const hiddenField = profileEl.querySelector('.hidden-fields');

        if (hiddenField.classList.contains('active')) {
            hiddenField.classList.remove('active');
            e.target.textContent = 'Show less';
        }
        else {
            hiddenField.classList.add('active');
            e.target.textContent = 'Show more';
        }
    });
}
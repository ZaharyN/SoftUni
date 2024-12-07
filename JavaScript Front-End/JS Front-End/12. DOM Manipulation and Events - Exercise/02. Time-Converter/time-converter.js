document.addEventListener('DOMContentLoaded', solve);

function solve() {
    const forms = document.querySelectorAll('form');
    const days = document.querySelector('#days-input');
    const hours = document.querySelector('#hours-input');
    const minutes = document.querySelector('#minutes-input');
    const seconds = document.querySelector('#seconds-input');

    forms.forEach(f => {
        f.addEventListener('submit', (e) => {
            e.preventDefault();

            const currentEl = e.target.querySelector('input[type="number"]');
            const currentElValue = Number(currentEl.value);
            if(currentElValue < 1) { return;}
            const multiplier = currentEl.getAttribute('id');

            updateInputs(currentElValue, multiplier);
        });
    });

    function updateInputs(elValue, timeMultiplier) {
        switch (timeMultiplier) {
            case ('days-input'):
                days.value = Number(elValue).toFixed(2);
                hours.value = Number(elValue * 24).toFixed(2);
                minutes.value = Number(elValue * 1440).toFixed(2);
                seconds.value = Number(elValue * 86400).toFixed(2);
                break;
            case ('hours-input'):
                days.value = Number(elValue / 24).toFixed(2);
                hours.value = Number(elValue).toFixed(2);
                minutes.value = Number(elValue * 60).toFixed(2);
                seconds.value = Number(elValue * 3600).toFixed(2);
                break;
            case ('minutes-input'):
                days.value = Number(elValue / 1440).toFixed(2);
                hours.value = Number(elValue / 60).toFixed(2);
                minutes.value = Number(elValue).toFixed(2);
                seconds.value = Number(elValue * 60).toFixed(2);
                break;
            case ('seconds-input'):
                days.value = Number(elValue / 86400).toFixed(2);
                hours.value = Number(elValue / 3600).toFixed(2);
                minutes.value = Number(elValue / 60).toFixed(2);
                seconds.value = Number(elValue).toFixed(2);
                break;
        }
    }
}
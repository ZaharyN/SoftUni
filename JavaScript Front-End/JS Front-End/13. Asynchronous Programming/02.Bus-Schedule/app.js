function solve() {
    const infoBox = document.querySelector('span.info');
    const departButton = document.querySelector('input#depart');
    const arriveButton = document.querySelector('input#arrive');
    let nextStopId = "depot";
    let nextStopName = '';

    function depart() {

        const getUrl = 'http://localhost:3030/jsonstore/bus/schedule/';

        fetch(getUrl + nextStopId)
            .then(result => result.json())
            .then(data => {
                infoBox.textContent = `Next stop ${data.name}`;
                nextStopId = data.next;
                nextStopName = data.name;
                departButton.disabled = true;
                arriveButton.disabled = false;
            })
            .catch(err => infoBox.textContent = 'Error');
    }

    async function arrive() {
        infoBox.textContent = `Arriving at ${nextStopName}`;
        arriveButton.disabled = true;
        departButton.disabled = false;
    }

    return {
        depart,
        arrive
    };
}

let result = solve();
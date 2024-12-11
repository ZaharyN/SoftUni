function getInfo() {

    const getUrl = "http://localhost:3030/jsonstore/bus/businfo/";
    const input = document.querySelector('#stopInfo #stopId');
    const submitButton = document.querySelector('#stopInfo #submit');
    const busId = input.value;
    const divName = document.querySelector('#result #stopName');
    const busList = document.querySelector('#result #buses');
    divName.innerHTML = '';
    busList.innerHTML = '';

    fetch(getUrl + busId)
        .then(result => result.json())
        .then(data => {
            divName.textContent = data.name;

            Object.entries(data.buses).forEach(([busId, time]) => {

                const liEl = document.createElement('li');
                liEl.textContent = `Bus ${busId} arrives in ${time} minutes`;
                busList.append(liEl);

            })
        })
        .catch(error => divName.textContent = 'Error');
}
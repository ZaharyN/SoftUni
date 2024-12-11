function attachEvents() {
    const getUrl = "http://localhost:3030/jsonstore/forecaster/locations";
    const currentConditionUrl = "http://localhost:3030/jsonstore/forecaster/today/";
    const forecastUrl = "http://localhost:3030/jsonstore/forecaster/upcoming/";

    const getLocationsBtn = document.querySelector('#submit');
    const inputField = document.querySelector('input#location');
    let forecastDiv = document.querySelector('#forecast');

    getLocationsBtn.addEventListener('click', (e) => {

        if( ! inputField.value || inputField.value == '') return;
        fetch(getUrl)
            .then(result => result.json())
            .then(data => {
                const userInput = inputField.value;
                forecastDiv.style.display = 'block';
                
                [...data].forEach(d => {

                    if (d.name == userInput) {

                        makeCurrentConditionRequest(d.code);

                        makeThreeDayForecastRequest(d.code);
                    }
                });
            })
            .catch(err => forecastDiv.textContent = 'Error');
    })

    function makeCurrentConditionRequest(code) {

        fetch(currentConditionUrl + code)
            .then(result => result.json())
            .then(data => {

                const currentConditionContainer = document.createElement('div');
                currentConditionContainer.className = 'forecasts';
                document.querySelector('div#current').append(currentConditionContainer);

                const forecastData = data.forecast;

                const weatherSymbol = findWeatherSymbol(forecastData.condition);
                currentConditionContainer.appendChild(createElement('span', 'condition symbol', weatherSymbol));
                const spanContainer = createElement('span', 'condition',);
                spanContainer.appendChild(createElement('span', 'forecast-data', data.name));
                let lowHigh = forecastData.low + '°' + '/' + forecastData.high + '°';
                spanContainer.appendChild(createElement('span', 'forecast-data', lowHigh));
                spanContainer.appendChild(createElement('span', 'forecast-data', forecastData.condition));

                currentConditionContainer.appendChild(spanContainer);
            })
            .catch(err => forecastDiv.textContent = 'Error');
    }

    function makeThreeDayForecastRequest(code) {

        fetch(forecastUrl + code)
            .then(result => result.json())
            .then(data => {

                const forecastContainer = createElement('div', 'forecast-info');
                forecastDiv.querySelector('#upcoming').appendChild(forecastContainer);

                const forecastData = data.forecast;
                forecastData.forEach(element => {

                    const upcomingSpan = createElement('span', 'upcoming');
                    forecastContainer.appendChild(upcomingSpan);

                    upcomingSpan.appendChild(createElement('span', 'symbol', findWeatherSymbol(element.condition)));
                    let lowHigh = element.low + '°' + '/' + element.high + '°';
                    upcomingSpan.appendChild(createElement('span', 'forecast-data', lowHigh));
                    upcomingSpan.appendChild(createElement('span', 'forecast-data', element.condition));
                });
            })
            .catch(err => forecastDiv.textContent = 'Error');
    }

    function findWeatherSymbol(weather) {
        switch (weather) {
            case 'Sunny':
                return '\u2600'
            case 'Partly sunny':
                return '\u26C5';
            case 'Overcast':
                return '\u2601';
            case 'Rain':
                return '\u2614';
            case 'Degrees':
                return '°';
        }
    }

    function createElement(tag, className, textContent) {
        const element = document.createElement(tag);
        if (className) element.className = className;
        if (textContent) element.textContent = textContent;
        return element;
    }
}

attachEvents();
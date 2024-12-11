function attachEvents() {

    const baseUrl = "http://localhost:3030/jsonstore/messenger";

    const messageArea = document.querySelector('#messages');
    const nameInput = document.querySelector('input[name="author"]');
    const messageInput = document.querySelector('input[name="content"]');
    const sendButton = document.querySelector('#submit');
    const refreshButton = document.querySelector('#refresh');

    sendButton.addEventListener('click', (e) => {

        if (!nameInput.value) return;
        if (!messageInput.value) return;

        const body = { author: nameInput.value, content: messageInput.value };

        fetch(baseUrl, {
            method: "POST",
            body: JSON.stringify(body)
        })
            .then(result => result.json())
            .then(message => {

                refreshButton.click();
            })
            .catch(err => console.log('Error: ',err));``
    });

    refreshButton.addEventListener('click', (e) => {

        messageArea.textContent = '';

        fetch(baseUrl)
            .then(result => result.json())
            .then(messages => {

                Object.values(messages).forEach(message => {
                    messageArea.textContent += `${message.author}: ${message.content}\n`;
                });
                messageArea.textContent = messageArea.textContent.trimEnd();
            })
            .catch(err => console.log(err));
    })

    refreshButton.click();
}

attachEvents();
// function attachEvents() {

//     const baseUrl = 'http://localhost:3030/jsonstore/messenger';

//     const outputEl = document.querySelector('#messages');
//     const inputs = document.querySelectorAll('#controls input[name]');
//     const buttonSubmitEl = document.querySelector('#submit');
//     const buttonRefreshEl = document.querySelector('#refresh');

//     buttonSubmitEl.addEventListener('click', (e) => {

//         const [ author, content ] = [...inputs].map(field => field.value);

//         if ( ! author || ! content ) return;

//         const body = { author, content }

//         fetch(baseUrl, {
//             method: 'POST',
//             body: JSON.stringify(body)
//         })
//             .then(response => response.json())
//             .then(result => {
//                 buttonRefreshEl.click();
//             })
//             .catch(error => console.error('Error: ', error));

//     });

//     buttonRefreshEl.addEventListener('click', (e) => {

//         outputEl.textContent = '';

//         fetch(baseUrl)
//             .then(response => response.json())
//             .then(messages => {
                
//                 Object.values(messages).forEach(message => {
//                     console.log(message);
//                     outputEl.textContent += `${message.author}: ${message.content}\n`
//                 });

//                 outputEl.textContent = outputEl.textContent.trimEnd();

//             })
//             .catch(error => console.error('Error: ', error));

//     });

//     buttonRefreshEl.click();

// }

// attachEvents();
document.addEventListener('DOMContentLoaded', solve);

function solve() {

    const encodeForm = document.querySelector('#encode');
    const decodeForm = document.querySelector('#decode');

    encodeForm.addEventListener('submit', (e) => {
        e.preventDefault();
        const textArea = e.target.querySelector('textarea');
        const encodedMessage = encodeMessage(textArea.value);

        const decodedTextArea = decodeForm.querySelector('textarea');
        decodedTextArea.value = encodedMessage;
        textArea.value = '';
    });

    decodeForm.addEventListener('submit', (e) => {
        e.preventDefault();
        const textArea = e.target.querySelector('textarea');
        const decodedMessage = decodeMessage(textArea.value);

        const encodedTextArea = encodeForm.querySelector('textarea');
        textArea.value = decodedMessage;
    });

    function encodeMessage(textMessage) {
        let encoded = '';

        for (let i = 0; i < textMessage.length; i++) {
            let asciiSymbol = textMessage.charCodeAt(i) + 1;
            encoded += String.fromCharCode(asciiSymbol);
        }

        return encoded;
    }

    function decodeMessage(textMessage) {
        let decoded = '';

        for (let i = 0; i < textMessage.length; i++) {
            let asciiSymbol = textMessage.charCodeAt(i) - 1;
            decoded += String.fromCharCode(asciiSymbol);
        }

        return decoded;
    }
}
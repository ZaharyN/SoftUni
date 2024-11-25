function extractText() {
    const liItems = document.getElementsByTagName('li');
    const textarea = document.getElementById('result');
    

    let result = '';
    for (const li of liItems) {
        result += li.textContent + '\n';
    }
    textarea.value = result;
}
function extract(id) {
    let text = document.getElementById(id).textContent;
    let pattern = /\(([^)]+)\)/g;
    let result = [];

    let matchedWords = text.match(pattern);
    
    for (const e of matchedWords) {
        result.push(e);
    }

    return result.join('; ');
}
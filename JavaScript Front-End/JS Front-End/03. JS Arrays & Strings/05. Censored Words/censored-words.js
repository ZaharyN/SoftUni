function solve(text, word) {
    text = text.replaceAll(word, '*'.repeat(word.length));
    console.log(text);
}

solve('A small sentence with some words', 'small');
solve('Find the hidden word', 'hidden');

// 'A small sentence with some words', 'small'	A ***** sentence with some words
// 'Find the hidden word', 'hidden'	Find the ****** word

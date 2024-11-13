function solve(words, sentence) {
    words = words.split(', ');

    words.forEach(e => {
        sentence = sentence.replace('*'.repeat(e.length), e);
    });

    console.log(sentence); 
}

solve('great', 'softuni is ***** place for learning new programming languages');
// softuni is great place for learning new programming languages
solve('great, learning','softuni is ***** place for ******** new programming languages');	
// softuni is great place for learning new programming languages

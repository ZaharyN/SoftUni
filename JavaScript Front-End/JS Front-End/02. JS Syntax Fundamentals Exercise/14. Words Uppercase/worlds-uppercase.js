function solve(sentence) {
    let words = sentence.match(/\b\w+\b/g)
        .map(w => w.toUpperCase());

    console.log(words.join(', ', words));
}

solve("Hi, how are you?");
solve('hello');
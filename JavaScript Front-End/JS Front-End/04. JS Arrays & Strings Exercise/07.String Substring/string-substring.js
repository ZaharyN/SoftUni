function solve(word, text) {
    
    if (text.toLowerCase().split(' ').includes(word.toLowerCase())) {
        console.log(word);
        return;
    }

    console.log(`${word.toLowerCase()} not found!`);
}

solve('javascript', 'JavaScript is the best programming language');
//javascript

solve('python', 'JavaScript is the best programming language');
//python not found!
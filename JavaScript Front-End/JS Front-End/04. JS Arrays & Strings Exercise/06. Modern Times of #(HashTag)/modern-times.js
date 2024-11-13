function solve(input) {

    let specialWords = input
        .split(' ')
        .filter(w => w[0] == "#" && /#[a-zA-Z]+$/.test(w));

    specialWords.forEach(element => {
        console.log(element.slice(1));
    });
}

solve('Nowadays everyone uses # to tag a #special word in #socialMedia');
solve('The symbol # is known #variously in English-speaking #regions as the #number sign');

// 'Nowadays everyone uses # to tag a #special word in #socialMedia'
// special
// socialMedia

// 'The symbol # is known #variously in English-speaking #regions as the #number sign'
// variously
// regions
// number
function solve(input) {

    let wordsToMatch = input.shift().split(' ');
    let matched = {};
    wordsToMatch.forEach(el => {
        matched[el] = 0;
    });

    input.forEach(x => {
        if (matched.hasOwnProperty(x)) {
            matched[x] += 1;
        }
    })

    let entries = Object.entries(matched);
    entries.sort(([keyA, valueA], [keyB, valueB]) => valueB - valueA);

    for (const [key, value] of entries) {
        console.log(`${key} - ${value}`);
    }
}

solve([
    'this sentence',
    'In', 'this', 'sentence', 'you', 'have', 'to', 'count', 'the', 'occurrences', 'of', 'the', 'words', 'this', 'and', 'sentence', 'because', 'this', 'is', 'your', 'task'
]);
// this - 3
// sentence - 2

solve([
    'is the',
    'first', 'sentence', 'Here', 'is', 'another', 'the', 'And', 'finally', 'the', 'the', 'sentence']);
// the – 3
// is - 1
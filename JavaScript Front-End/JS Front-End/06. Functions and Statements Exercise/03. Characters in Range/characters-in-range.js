function solve(first, second){
    let start = Math.min(first.charCodeAt(0), second.charCodeAt(0));
    let end = Math.max(first.charCodeAt(0), second.charCodeAt(0));
    let result = '';

    for (let i = start + 1; i < end; i++) {
        result += String.fromCharCode(i) + ' ';
    }

    console.log(result);
}

solve('a', 'd');
solve('#', ':');
solve('C', '#');
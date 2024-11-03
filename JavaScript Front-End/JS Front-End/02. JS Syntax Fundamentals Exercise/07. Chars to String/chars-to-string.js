function solve(...chars) {
    let result = "";
    chars.forEach(element => {
        result += element;
    });

    console.log(result);
}

solve('a', 'b', 'c');
solve('%', '2', 'o');
solve('1', '5', 'p');


// 'a',
// 'b',
// 'c'
// abc

// '%',
// '2',
// 'o'
// %2o

// '1',
// '5',
// 'p'
// 15p

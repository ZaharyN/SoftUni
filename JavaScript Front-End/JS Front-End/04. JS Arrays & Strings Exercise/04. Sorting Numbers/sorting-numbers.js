function solve(array) {
    let zippedArr = [];
    let length = array.length;

    array.sort((a, b) => a - b);

    for (let i = 0; i < length; i++) {
        if (i % 2 == 0) {
            let el = array.shift();
            zippedArr.push(el);
        }
        else {
            let el = array.pop();
            zippedArr.push(el);
        }
    }
    return zippedArr;
}

solve([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]);
// [1, 65, 3, 52, 48, 63, 31, -3, 18, 56]	[-3, 65, 1, 63, 3, 56, 18, 52, 31, 48]
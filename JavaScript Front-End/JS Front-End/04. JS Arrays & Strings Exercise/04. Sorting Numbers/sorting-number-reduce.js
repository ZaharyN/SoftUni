function solve(array) {
    array.sort((a, b) => a - b)

    let zippedArr = [...new Array(array.length).keys()]
    zippedArr.reduce((acc, el, i) => {
        if (i % 2 == 0) {
            return zippedArr[i] = array.shift();
        }
        else {
            return zippedArr[i] = array.pop();
        }   
    }, []);

    return zippedArr;
}

solve([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]);
// [1, 65, 3, 52, 48, 63, 31, -3, 18, 56]	[-3, 65, 1, 63, 3, 56, 18, 52, 31, 48]
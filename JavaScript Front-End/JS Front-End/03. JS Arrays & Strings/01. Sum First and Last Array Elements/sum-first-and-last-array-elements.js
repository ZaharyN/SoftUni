function solve(arr) {
    console.log(arr.pop() + arr.shift());
} 

solve([20, 30, 40]);
solve([10, 17, 22, 33]);
solve([11, 58, 69]);
// [20, 30, 40]	60
// [10, 17, 22, 33]	43
// [11, 58, 69]	80

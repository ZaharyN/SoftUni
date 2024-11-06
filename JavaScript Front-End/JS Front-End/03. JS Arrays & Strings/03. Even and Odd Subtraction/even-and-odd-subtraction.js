function solve(arr) {
    // let evenSum = arr.reduce((acc, evenNum ) => evenNum % 2 == 0 ? acc + evenNum : acc, 0);
    // let oddSum = arr.reduce((acc, oddNum) => oddNum % 2 != 0 ? acc + oddNum : acc, 0);
    // console.log(evenSum - oddSum);
    console.log(arr.reduce((acc, evenNum ) => evenNum % 2 == 0 ? acc + evenNum : acc, 0)
        - 
        arr.reduce((acc, oddNum) => oddNum % 2 != 0 ? acc + oddNum : acc, 0));
}

solve([1,2,3,4,5,6]);
solve([3,5,7,9]);
solve([2,4,6,8,10]);

// [1,2,3,4,5,6]	3
// [3,5,7,9]	-24
// [2,4,6,8,10]	30

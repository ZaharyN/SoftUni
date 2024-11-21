// function solve(n) {
//     let result = '';
    
//     for (let i = 0; i < n; i++) {
//         for (let j = 0; j < n; j++) {
//             result += n + ' ';
//         }
//         result += "\n";
//     }
//     console.log(result);
// }

function solve(n){
    let row = (n + " ").repeat(n);
    console.log((row + "\n").repeat(n));
}

solve(3);
//3 3 3
//3 3 3
//3 3 3

solve(7);
//7 7 7 7 7 7 7
//7 7 7 7 7 7 7
//7 7 7 7 7 7 7
// 7 7 7 7 7 7 7
// 7 7 7 7 7 7 7
// 7 7 7 7 7 7 7
// 7 7 7 7 7 7 7

solve(2);
//22
//22



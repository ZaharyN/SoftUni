// function calculator(a, b, operation) {
//     switch (operation) {
//         case "multiply":
//             console.log(a * b);
//             break;
//         case "divide":
//             console.log(a / b);
//             break;
//         case "add":
//             console.log(a + b);
//             break;
//         case "subtract":
//             console.log(a - b);
//             break;
//     }
// }

// const sum = (a, b) => a + b;

// calculator(5, 5, 'multiply');
// calculator(40, 8, 'divide');
// calculator(12, 19, 'add');
// calculator(50, 13, 'subtract');

function buildOperation(operationName) {
    switch (operationName) {
        case "multiply":
            return function (a, b) {
                return a * b;
            }
        case "divide":
            return function (a, b) {
                return a / b;
            }
        case "add":
            return function (a, b) {
                return a + b;
            }
        case "subtract":
            return function (a, b) {
                return a - b;
            }
    }
}

const sum = buildOperation("multiply");
console.log(sum(5, 5));
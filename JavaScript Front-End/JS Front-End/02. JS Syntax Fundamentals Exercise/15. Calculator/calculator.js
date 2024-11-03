function solve(number1, operator, number2) {
    let result = 0;
    switch (operator) {
        case "+":
            result = number1 + number2;
            break;
        case "-":
            result = number1 - number2;
            break;
        case "/":
            result = number1 / number2;
            break;
        case "*":
            result = number1 * number2;
            break;
    }

    console.log(result.toFixed(2));
}

solve(5, '+', 10);
solve(25.5, '-', 3);

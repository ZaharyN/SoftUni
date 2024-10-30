function solve(num1, num2, operator) {
    switch (operator) {
        case '+':
            console.log(num1 + num2);
            break;
        case '-':
            console.log(num1 - num2);
            break;
        case '*':
            console.log(num1 * num2);
            break;
        case '/':
            console.log(num1 / num2);
            break;
        case '%':
            console.log(num1 % num2);
            break;
        case '**':
            console.log(num1 ** num2);
            break;
    }
}

solve(5, 6, '+');
solve(3, 5.5, '*');

// Input	Output
// 5, 6, '+'	11
// 3, 5.5, '*'	16.5

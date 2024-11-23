function solve(number1, number2) {

    let sum1 = factorial(number1);
    let sum2 = factorial(number2);

    function factorial(number) {

        if (number <= 1) {
            return 1;
        }

        return number * factorial(number - 1);
    }

    console.log((sum1 / sum2).toFixed(2));
}

solve(5, 2);
//60.00

solve(6, 2);
//360.00
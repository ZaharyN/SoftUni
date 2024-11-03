function solve(number) {
    let numberAsString = number.toString();
    let areEqual = true;
    let sum = 0;

    for (let i = 1; i < numberAsString.length; i++) {
        if (numberAsString[i] != numberAsString[i - 1]) {
            areEqual = false;
        }
        sum += Number(numberAsString[i - 1]);
    }

    sum += Number(numberAsString[numberAsString.length - 1]);
    console.log(areEqual);
    console.log(sum);
}

solve(2222222);
solve(1234);
function solve(num1, num2, num3) {
    let number = (Math.sign(num1) * Math.sign(num2) * Math.sign(num3));
    if (number < 0){
        console.log("Negative");
    }
    else {
        console.log("Positive");
    }
}

solve(5, 12, 15);
solve(-6, -12, 14);
solve(-1, -2, -3);
solve(-5, 1, 1);
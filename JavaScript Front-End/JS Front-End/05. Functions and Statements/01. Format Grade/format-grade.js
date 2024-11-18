function evaluate(number) {
    let grade = ""; 

    if (number < 3.00){
        grade = "Fail";
        console.log(`${grade} (${Math.floor(number)})`);
    }
    else if (number < 3.5) {
        grade = "Poor";
        console.log(`${grade} (${number.toFixed(2)})`);
    }
    else if (number < 4.5) {
        grade = "Good";
        console.log(`${grade} (${number.toFixed(2)})`);
    }
    else if (number < 5.5) {
        grade = "Very good";
        console.log(`${grade} (${number.toFixed(2)})`);
    }
    else {
        grade = "Excellent";
        console.log(`${grade} (${number.toFixed(2)})`);
    }
}

evaluate(3.33);
evaluate(4.50);
evaluate(2.99);
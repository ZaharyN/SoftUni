function solve(password) {
    let isValid = true;
    const lettersAndNumbers = /^[a-zA-z0-9]+$/;
    const twoNumber = /^(?=(.*\d.*\d))/;

    if (!(password.length >= 6 && password.length <= 10)) {
        isValid = false;
        console.log("Password must be between 6 and 10 characters");
    }
    if (!lettersAndNumbers.test(password)) {
        isValid = false;
        console.log("Password must consist only of letters and digits");
    }
    if (!twoNumber.test(password)) {
        isValid = false;
        console.log("Password must have at least 2 digits");
    }
    if (isValid) {
        console.log("Password is valid");
    }
}

solve('logIn');
// Password must be between 6 and 10 characters
// Password must have at least 2 digits

solve('MyPass123');
// Password is valid

solve('Pa$s$s');
//Password must consist only of letters and digits
//Password must have at least 2 digits

solve("f");
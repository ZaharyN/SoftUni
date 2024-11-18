function sum(a, b) {
    return a + b;
}

function subtract(a, b) {
    return a - b;
}

function solve(a, b, c) {
    console.log((subtract(sum(a, b), c)));
    
    function sum(a, b) {
        return a + b;
    }
    
    function subtract(a, b) {
        return a - b;
    }
}

solve(23, 6, 10);
solve(1, 17, 30);
solve(42, 58, 100);
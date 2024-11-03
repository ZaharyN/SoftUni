function solve(fruit, grams, pricePerKg) {
    console.log(`I need $${(grams/1000 * pricePerKg).toFixed(2)} to buy ${(grams/1000).toFixed(2)} kilograms ${fruit}.`);
}

solve('orange', 2500, 1.80);
solve('apple', 1563, 2.35);

// 'orange', 2500, 1.80	I need $4.50 to buy 2.50 kilograms orange.

// 'apple', 1563, 2.35	I need $3.67 to buy 1.56 kilograms apple.

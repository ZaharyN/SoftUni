function solve(stock, order) {
    let products = {};

    stock.forEach((element, i) => {
        if (i % 2 != 0) {
            products[stock[i - 1]] = Number(element);
        }
    });

    order.forEach((element, i) => {
        if (i % 2 != 0) {
            if (products.hasOwnProperty(order[i - 1])) {
                products[order[i - 1]] += Number(element);
            } else {
                products[order[i - 1]] = element;
            }
        }
    });
    
    for (const product in products) {
        console.log(`${product} -> ${products[product]}`);
    }
}

solve(['Chips', '5', 'CocaCola', '9', 'Bananas', '14', 'Pasta', '4', 'Beer', '2'],
    ['Flour', '44', 'Oil', '12', 'Pasta', '7', 'Tomatoes', '70', 'Bananas', '30']);
// Chips -> 5
// CocaCola -> 9
// Bananas -> 44
// Pasta -> 11
// Beer -> 2
// Flour -> 44
// Oil -> 12
// Tomatoes -> 70

solve(['Salt', '2', 'Fanta', '4', 'Apple', '14', 'Water', '4', 'Juice', '5'],
    ['Sugar', '44', 'Oil', '12', 'Apple', '7', 'Tomatoes', '7', 'Bananas', '30']);
// Salt -> 2
// Fanta -> 4
// Apple -> 21
// Water -> 4
// Juice -> 5
// Sugar -> 44
// Oil -> 12
// Tomatoes -> 7
// Bananas -> 30
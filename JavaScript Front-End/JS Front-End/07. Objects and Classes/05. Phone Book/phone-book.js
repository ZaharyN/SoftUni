function solve(input) {
    let phoneBook = {};

    for (const line of input) {
        let tokens = line.split(' ');
        let name = tokens[0];
        let phone = tokens[1];
        phoneBook[name] = phone;
    }

    for (const key in phoneBook) {
        console.log(`${key} -> ${phoneBook[key]}`);
    }
}

solve(['Tim 0834212554','Peter 0877547887','Bill 0896543112','Tim 0876566344'])

// Tim -> 0876566344
// Peter -> 0877547887
// Bill -> 0896543112

solve(['George 0552554','Peter 087587','George 0453112','Bill 0845344']);
//George -> 0453112
//Peter -> 087587
//Bill -> 0845344

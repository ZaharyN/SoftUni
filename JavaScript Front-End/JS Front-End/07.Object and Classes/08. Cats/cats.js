function solve(input) {
    let cats = [];

    class Cat {
        constructor(name, age) {
            this.name = name,
                this.age = age
        }

        meow() {
            console.log(`${this.name}, age ${this.age} says Meow`)
        }
    }

    input.forEach(cat => {
        let [name, age] = cat.split(' ');
        cats.push(new Cat(name, parseInt(age)))
    });

    for (const cat of cats) {
        cat.meow();
    }
}

solve(['Mellow 2', 'Tom 5']);
//Mellow, age 2 says Meow
//Tom, age 5 says Meow
solve(['Candy 1', 'Poppy 3', 'Nyx 2']);
// Candy, age 1 says Meow
// Poppy, age 3 says Meow
// Nyx, age 2 says Meow
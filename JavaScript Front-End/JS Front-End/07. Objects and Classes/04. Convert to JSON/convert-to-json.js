function solve(name, lastName, hairColor) {
    let personInfo = {
        name: name,
        lastName: lastName,
        hairColor: hairColor
    }

    console.log(JSON.stringify(personInfo));
}

solve('George', 'Jones', 'Brown');
// {"name":"George","lastName":"Jones","hairColor":"Brown"}

solve('Peter', 'Smith', 'Blond');
// {"name":"Peter","lastName":"Smith","hairColor":"Blond"}
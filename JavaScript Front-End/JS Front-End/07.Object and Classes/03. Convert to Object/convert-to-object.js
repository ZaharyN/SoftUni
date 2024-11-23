function solve(jsonString) {
    let person = JSON.parse(jsonString);
    
    for (const [key, value] of Object.entries(person)) {
        console.log(`${key}: ${value}`);
    }
}

solve('{"name": "George", "age": 40, "town": "Sofia"}');
// name: George
// age: 40
// town: Sofia

solve('{"name": "Peter", "age": 35, "town": "Plovdiv"}');
// name: Peter
// age: 35
// town: Plovdiv
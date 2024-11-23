function solve(input) {
    
    let employees = [];
    class Employee{
        constructor(name){
            this.name = name,
            this.number = name.length
        }
    }

    input.forEach(element => {
        let employee = new Employee(element);
        employees.push(employee); 
    });

    for (const employee of employees) {
        console.log(`Name: ${employee.name} -- Personal Number: ${employee.number}`);
    }
}

solve([
    'Silas Butler',
    'Adnaan Buckley',
    'Juan Peterson',
    'Brendan Villarreal'
]);
// Name: Silas Butler-- Personal Number: 12
// Name: Adnaan Buckley-- Personal Number: 14
// Name: Juan Peterson-- Personal Number: 13
// Name: Brendan Villarreal-- Personal Number: 18

solve([
    'Samuel Jackson',
    'Will Smith',
    'Bruce Willis',
    'Tom Holland'
]);
// Name: Samuel Jackson-- Personal Number: 14
// Name: Will Smith-- Personal Number: 10
// Name: Bruce Willis-- Personal Number: 12
// Name: Tom Holland-- Personal Number: 11
function solve(input) {
    let appointments = {};
    
    input.forEach(element => {
        let [weekDay, name] = element.split(' ');
        
        if(appointments.hasOwnProperty(weekDay)){
            console.log(`Conflict on ${weekDay}!`);
        } else {
            appointments[weekDay] = name;
            console.log(`Scheduled for ${weekDay}`);
        }
    });
    
    for (const key in appointments) {
        console.log(`${key} -> ${appointments[key]}`);
    }
}

solve(['Monday Peter', 'Wednesday Bill', 'Monday Tim', 'Friday Tim']);
// Scheduled for Monday
// Scheduled for Wednesday
// Conflict on Monday!
// Scheduled for Friday
// Monday -> Peter
// Wednesday -> Bill
// Friday -> Tim

solve(['Friday Bob', 'Saturday Ted', 'Monday Bill', 'Monday John', 'Wednesday George']);
// Scheduled for Friday
// Scheduled for Saturday
// Scheduled for Monday
// Conflict on Monday!
// Scheduled for Wednesday
// Friday -> Bob
// Saturday -> Ted
// Monday -> Bill
// Wednesday -> George
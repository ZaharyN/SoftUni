function solve(speed, area) {
    let speedLimit;
    let difference = 0;
    let status = "";

    switch (area) {
        case "city":
            speedLimit = 50;

            if(speed <= speedLimit){
                console.log(`Driving ${speed} km/h in a ${speedLimit} zone`);
            }
            else{
                difference = speed - speedLimit;
                if(difference <= 20){
                    status = "speeding";
                } 
                else if(difference <= 40){
                    status = "excessive speeding";
                }
                else {
                    status = "reckless driving";
                }
                console.log(`The speed is ${difference} km/h faster than the allowed speed of ${speedLimit} - ${status}`);
            }
            break;
        case "residential":
            speedLimit = 20;

            if(speed <= speedLimit){
                console.log(`Driving ${speed} km/h in a ${speedLimit} zone`);
            }
            else{
                difference = speed - speedLimit;
                if(difference <= 20){
                    status = "speeding";
                } 
                else if(difference <= 40){
                    status = "excessive speeding";
                }
                else {
                    status = "reckless driving";
                }
                console.log(`The speed is ${difference} km/h faster than the allowed speed of ${speedLimit} - ${status}`);
            }
            break;
        case "interstate":
            speedLimit = 90;

            if(speed <= speedLimit){
                console.log(`Driving ${speed} km/h in a ${speedLimit} zone`);
            }
            else{
                difference = speed - speedLimit;
                if(difference <= 20){
                    status = "speeding";
                } 
                else if(difference <= 40){
                    status = "excessive speeding";
                }
                else {
                    status = "reckless driving";
                }
                console.log(`The speed is ${difference} km/h faster than the allowed speed of ${speedLimit} - ${status}`);
            }
            break;
        case "motorway":
            speedLimit = 130;

            if(speed <= speedLimit){
                console.log(`Driving ${speed} km/h in a ${speedLimit} zone`);
            }
            else{
                difference = speed - speedLimit;
                if(difference <= 20){
                    status = "speeding";
                } 
                else if(difference <= 40){
                    status = "excessive speeding";
                }
                else {
                    status = "reckless driving";
                }
                console.log(`The speed is ${difference} km/h faster than the allowed speed of ${speedLimit} - ${status}`);
            }
            break;
    }
}


solve(40, 'city');
solve(21, 'residential');
solve(120, 'interstate');
solve(200, 'motorway');
// 40, 'city'
// 21, 'residential'
// 120, 'interstate'
// 200, 'motorway'

function solve(input) {

    let parkingLot = [];
    input.forEach(line => {

        let [direction, carPlate] = line.split(', ');

        if (parkingLot.includes(carPlate)) {
            if (direction == "OUT") {
                let index = parkingLot.indexOf(carPlate);
                parkingLot.splice(index, 1);
            }
        }
        else {
            if(direction == "IN"){
                parkingLot.push(carPlate);
            }
        }
    });

    parkingLot.sort((a, b) => a.localeCompare(b));
    if (parkingLot.length > 0) {

        for (const element of parkingLot) {
            console.log(element);
        }
    }
    else {
        console.log("Parking Lot is Empty");
    }
}

solve(['IN, CA2844AA',
    'IN, CA1234TA',
    'OUT, CA2844AA',
    'IN, CA9999TT',
    'IN, CA2866HI',
    'OUT, CA1234TA',
    'IN, CA2844AA',
    'OUT, CA2866HI',
    'IN, CA9876HH',
    'IN, CA2822UU']);
// CA2822UU
// CA2844AA
// CA9876HH
// CA9999TT

solve(['IN, CA2844AA',
    'IN, CA1234TA',
    'OUT, CA2844AA',
    'OUT, CA1234TA']);
//Parking Lot is Empty
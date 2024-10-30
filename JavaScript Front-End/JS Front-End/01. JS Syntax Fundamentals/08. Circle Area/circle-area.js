function solve(radius){

    if(typeof(radius) !== 'number'){
        console.log(`We can not calculate the circle area, because we receive a ${typeof(radius)}.`);
        return;
    }

    const circleArea = radius * radius * Math.PI;
    console.log(circleArea.toFixed(2));
}

solve(5);
solve('name');
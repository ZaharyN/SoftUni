function solve(startingYieldPerDay) {
    let days = 0;
    const mineCostPerDay = 10;
    const mineCrewTax = 26;
    let spiceGathered = 0;

    while(startingYieldPerDay >= 100) {
        spiceGathered += startingYieldPerDay;
        spiceGathered -= mineCrewTax;
        
        startingYieldPerDay -= mineCostPerDay;
        days++;
    }
    if(spiceGathered >= mineCrewTax){
        spiceGathered -= mineCrewTax;
    }

    console.log(days);
    console.log(spiceGathered);
}

solve(111);
solve(450);
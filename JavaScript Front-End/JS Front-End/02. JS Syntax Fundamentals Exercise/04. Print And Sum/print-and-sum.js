function solve(numStart, numEnd) {
    let sum = 0;
    
    for (let i = numStart; i <= numEnd; i++){
        sum += i;
        //console.log(`${i}`);
        process.stdout.write(`${i} `);
    }
    console.log();
    console.log(`Sum: ${sum}`);
}

solve(5, 10);
solve(0, 26);
solve(50, 60);
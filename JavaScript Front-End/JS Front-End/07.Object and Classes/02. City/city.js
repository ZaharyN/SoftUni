function solve(obj){
    let entries = Object.entries(obj);

    for (const [key, value] of entries) {
        console.log(`${key} -> ${value}`);
    }
}

solve({
    name: "Sofia",
    area: 492,
    population: 1238438,
    country: "Bulgaria",
    postCode: "1000"});
// name -> Sofia
// area -> 492
// population -> 1238438
// country -> Bulgaria
// postCode -> 1000

solve({
    name: "Plovdiv",
    area: 389,
    population: 1162358,
    country: "Bulgaria",
    postCode: "4000"
});
// name -> Plovdiv
// area -> 389
// population -> 1162358
// country -> Bulgaria
// postCode -> 4000

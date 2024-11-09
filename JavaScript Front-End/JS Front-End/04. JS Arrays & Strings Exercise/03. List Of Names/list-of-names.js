function solve(array) {
    array.sort((a, b) => a.localeCompare(b))
        .forEach((element, i) => {
            console.log(`${i + 1}.${element}`)
        });
}



solve(["John", "Bob", "Christina", "Ema"]);
// 1.Bob
// 2.Christina
// 3.Ema
// 4.John

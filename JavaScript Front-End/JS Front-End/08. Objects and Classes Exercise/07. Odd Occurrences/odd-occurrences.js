function solve(input) {
    const words = input.toLowerCase().split(' ');
    const trackedWords = {};

    words.forEach(word => {

        if (trackedWords.hasOwnProperty(word)) {
            trackedWords[word] += 1;
        }
        else {
            trackedWords[word] = 1;
        }
    });

    let result = "";
    const entries = Object.entries(trackedWords);
    entries.forEach(([key, value]) => {
        if (value % 2 != 0) {
            result += key + " ";
        }
    });
    console.log(result);
}

solve('Java C# Php PHP Java PhP 3 C# 3 1 5 C#');
//c# php 1 5

solve('Cake IS SWEET is Soft CAKE sweet Food');
// soft food
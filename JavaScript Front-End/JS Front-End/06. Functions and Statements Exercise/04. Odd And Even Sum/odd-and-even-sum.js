function solve(number){
    let digits = number.toString().split('');
    let evenSum = 0;
    let oddSum = 0;

    digits.forEach(element => {
        if(element % 2 == 0){
            evenSum += Number(element);
        } 
        else {
            oddSum += Number(element);
        }
    });

    console.log(`Odd sum = ${oddSum}, Even sum = ${evenSum}`);
}


solve(1000435);	 
//Odd sum = 9, Even sum = 4

solve(3495892137259234)	
//Odd sum = 54, Even sum = 22

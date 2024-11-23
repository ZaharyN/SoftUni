function solve(percentage){
    let result = `${percentage}% `;

    if(percentage < 100){
        result += "[";
        result += (("%").repeat(percentage / 10) + (".").repeat(10 - percentage / 10));
        result += "]\n";
        result += "Still loading...";
    }
    else if(percentage == 100){
        result += "Complete!\n"
        result += "[";
        result += (("%").repeat(percentage / 10) + (".").repeat(10 - percentage / 10));
        result += "]\n";
    }
    console.log(result);
}

solve(30);	
// 30% [%%%.......]
// Still loading...

solve(50);	
// 50% [%%%%%.....]
// Still loading...

solve(100);	
// 100% Complete!
// [%%%%%%%%%%]


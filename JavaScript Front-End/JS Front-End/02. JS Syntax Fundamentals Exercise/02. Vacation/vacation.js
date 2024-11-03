function solve(groupNum, groupType, day) {
    let price = 0;
    let totalPrice = 0;

    switch (groupType) {
        case "Students":
            switch (day) {
                case "Friday":
                    price = 8.45;
                    totalPrice = price * groupNum;
                    break;
                case "Saturday":
                    price = 9.80;
                    totalPrice = price * groupNum;
                    break;
                case "Sunday":
                    price = 10.46;
                    totalPrice = price * groupNum;
                    break;
            }
            if(groupNum >= 30) {
                totalPrice -= totalPrice * 0.15;
            }
            break;
        case "Business":
            if(groupNum >= 100) {
                groupNum -= 10;
            }
            switch (day) {
                case "Friday":
                    price = 10.90;
                    totalPrice = price * groupNum;
                    break;
                case "Saturday":
                    price = 15.60;
                    totalPrice = price * groupNum;
                    break;
                case "Sunday":
                    price = 16;
                    totalPrice = price * groupNum;
                    break;
            }
            break;
        case "Regular":
            switch (day) {
                case "Friday":
                    price = 15;
                    totalPrice = price * groupNum;
                    break;
                case "Saturday":
                    price = 20;
                    totalPrice = price * groupNum;
                    break;
                case "Sunday":
                    price = 22.50;
                    totalPrice = price * groupNum;
                    break;
            }
            if(groupNum >= 10 && groupNum <= 20) {
                totalPrice -= totalPrice * 0.05;
            }
            break;
    }

    console.log(`Total price: ${totalPrice.toFixed(2)}`);
}

solve(30, "Students", "Sunday");
solve(40, "Regular", "Saturday");

// 30,
// "Students",
// "Sunday"	Total price: 266.73

// 40,
// "Regular",
// "Saturday"	Total price: 800.00

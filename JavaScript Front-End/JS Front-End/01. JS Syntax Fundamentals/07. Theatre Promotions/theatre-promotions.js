function solve(day, age) {
    switch (day) {
        case 'Weekday':
            if (age < 0 || age > 122) {
                console.log('Error!');
            } else if (age <= 18) {
                console.log('12$');
            } else if (age <= 64) {
                console.log('18$');
            }
            else if (age <= 122) {
                console.log('12$');
            }
            break;
        case 'Weekend':
            if (age < 0 || age > 122) {
                console.log('Error!');
            } else if (age <= 15) {
                console.log('15$');
            } else if (age <= 64) {
                console.log('20$');
            }
            else if (age <= 122) {
                console.log('15$');
            }
            break;
        case 'Holiday':
            if (age < 0 || age > 122) {
                console.log('Error!');
            } else if (age <= 15) {
                console.log('5$');
            } else if (age <= 64) {
                console.log('12$');
            } else if (age <= 122) {
                console.log('10$');
            }
            break;
    }
}

solve('Weekday', 42);
solve('Holiday', -12);
solve('Holiday', 15);

// 'Weekday', 42	18$		

// 'Holiday', -12	Error!	

// 'Holiday', 15	5$

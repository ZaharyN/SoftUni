function solve(lostFights, helmetPrice, swordPrice, shieldPrice, armorPrice) {
    let expenses = 0;
    let brokenHelmetCount = Math.floor(lostFights / 2);
    let brokenSwordCount = Math.floor(lostFights / 3);
    let brokentShieldCount = Math.floor(lostFights / 6);
    let brokenArmorCount = Math.floor(lostFights / 12);

    expenses = brokenHelmetCount * helmetPrice + brokenSwordCount * swordPrice
        + brokentShieldCount * shieldPrice + brokenArmorCount * armorPrice;

    console.log(`Gladiator expenses: ${expenses.toFixed(2)} aureus`)
}

solve(7, 2, 3, 4, 5);
solve(23, 12.50, 21.50, 40, 200);
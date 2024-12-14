function solve(arr) {

    const heroNumber = Number(arr.shift());
    let heroes = {};

    for (let i = 0; i < heroNumber; i++) {

        const currentHero = arr.shift();
        let [name, superpowers, energy] = currentHero.split('-');

        superpowers = superpowers.split(',');

        heroes[name] = { name, superpowers: superpowers, energy: Number(energy) };
    }

    arr.forEach(info => {

        let commandLine = info.split(' * ');
        let heroName = '';
        let heroPower = '';

        switch (commandLine[0]) {
            case 'Use Power':
                heroName = commandLine[1];
                heroPower = commandLine[2];
                let energyRequired = Number(commandLine[3]);

                if (heroes[heroName].superpowers.includes(heroPower)
                    && heroes[heroName].energy >= energyRequired) {

                    const initialEnergy = Number(heroes[heroName].energy);
                    console.log(`${heroName} has used ${heroPower} and now has ${initialEnergy - energyRequired} energy!`);
                    heroes[heroName].energy -= energyRequired;
                }
                else {
                    console.log(`${heroName} is unable to use ${heroPower} or lacks energy!`);
                }
                break;
                
            case 'Train':
                heroName = commandLine[1];
                let trainingEnergy = Number(commandLine[2]);

                if (heroes[heroName].energy == 100) {
                    console.log(`${heroName} is already at full energy!`);
                }
                else {
                    let gainedEnergy = 0;
                    heroes[heroName].energy += trainingEnergy;

                    if (heroes[heroName].energy <= 100) {
                        gainedEnergy = trainingEnergy;
                    } else {
                        gainedEnergy = trainingEnergy - (heroes[heroName].energy - 100);
                        heroes[heroName].energy = 100;
                    }
                    console.log(`${heroName} has trained and gained ${gainedEnergy} energy!`);
                }
                break;

            case 'Learn':
                heroName = commandLine[1];
                const newSuperpower = commandLine[2];

                if (heroes[heroName].superpowers.includes(newSuperpower)) {
                    console.log(`${heroName} already knows ${newSuperpower}.`);

                } else {
                    heroes[heroName].superpowers.push(newSuperpower);
                    console.log(`${heroName} has learned ${newSuperpower}!`);
                }
                break;
        }
    });

    Object.keys(heroes).forEach(k =>{
        console.log(`Superhero: ${k}`);
        console.log(`- Superpowers: ${heroes[k].superpowers.join(', ')}`);
        console.log(`- Energy: ${heroes[k].energy}`);
    });
}

solve(([
    "3",
    "Iron Man-Repulsor Beams,Flight-80",
    "Thor-Lightning Strike,Hammer Throw-10",
    "Hulk-Super Strength-60",
    "Use Power * Iron Man * Flight * 30",
    "Train * Thor * 20",
    "Train * Hulk * 50",
    "Learn * Hulk * Thunderclap",
    "Use Power * Hulk * Thunderclap * 70",
    "Evil Defeated!"
]));

// Iron Man has used Flight and now has 50 energy! 
// Thor has trained and gained 20 energy! 
// Hulk has trained and gained 40 energy! 
// Hulk has learned Thunderclap! 
// Hulk has used Thunderclap and now has 30 energy! 
// Superhero: Iron Man 
// - Superpowers: Repulsor Beams, Flight 
// - Energy: 50 
// Superhero: Thor 
// - Superpowers: Lightning Strike, Hammer Throw 
// - Energy: 30 
// Superhero: Hulk 
// - Superpowers: Super Strength, Thunderclap 
// - Energy: 30


solve( ([
    "2",
    "Iron Man-Repulsor Beams,Flight-20",
    "Thor-Lightning Strike,Hammer Throw-100",
    "Train * Thor * 20",
    "Use Power * Iron Man * Repulsor Beams * 30",
    "Evil Defeated!"
]));

// Thor is already at full energy! 
// Iron Man is unable to use Repulsor Beams or lacks energy! 
// Superhero: Iron Man 
// - Superpowers: Repulsor Beams, Fligh
// - Energy: 20 
// Superhero: Thor 
// - Superpowers: Lightning Strike, Hammer Throw
// - Energy: 100

solve(([
    "2",
    "Iron Man-Repulsor Beams,Flight-100",
    "Thor-Lightning Strike,Hammer Throw-50",
    "Train * Thor * 20",
    "Learn * Thor * Hammer Throw",
    "Use Power * Iron Man * Repulsor Beams * 30",
    "Evil Defeated!"
]));

// Thor has trained and gained 20 energy! 
// Thor already knows Hammer Throw. 
// Iron Man has used Repulsor Beams and now has 70 energy! 
// Superhero: Iron Man 
// - Superpowers: Repulsor Beams, Flight 
// - Energy: 70 
// Superhero: Thor 
// - Superpowers: Lightning Strike, Hammer Throw 
// - Energy: 70
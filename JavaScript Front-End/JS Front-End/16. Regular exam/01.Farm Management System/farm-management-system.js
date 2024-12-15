function solve(arr) {

    const farmerNumber = arr.shift();
    let farmers = {};

    for (let i = 0; i < farmerNumber; i++) {

        const currentLine = arr.shift();
        let [farmerName, workArea, tasks] = currentLine.split(' ');
        tasks = tasks.split(',');

        farmers[farmerName] = { name: farmerName, workarea: workArea, tasks: tasks };
    }
    
    arr.forEach(cmdInfo => {

        cmdInfo = cmdInfo.split(' / ');
        const cmd = cmdInfo[0];
        const farmerName = cmdInfo[1];

        switch (cmd) {
            case 'Execute':

                const currentWorkArea = cmdInfo[2];
                const currentTask = cmdInfo[3];

                if (farmers[farmerName].workarea == currentWorkArea
                    && farmers[farmerName].tasks.includes(currentTask)) {

                    console.log(`${farmerName} has executed the task: ${currentTask}!`);

                }
                else {

                    console.log(`${farmerName} cannot execute the task: ${currentTask}.`);
                    
                }
                break;
            case 'Change Area':

                const newWorkArea = cmdInfo[2];

                farmers[farmerName].workarea = newWorkArea;
                console.log(`${farmerName} has changed their work area to: ${newWorkArea}`);
                
                break;
            case 'Learn Task':

                const newTask = cmdInfo[2];

                if(farmers[farmerName].tasks.includes(newTask)){

                    console.log(`${farmerName} already knows how to perform ${newTask}.`);
                    
                }
                else {

                    farmers[farmerName].tasks.push(newTask);
                    console.log(`${farmerName} has learned a new task: ${newTask}.`);
                    
                }
                break;
        }
    });

    Object.keys(farmers).forEach(k =>{

        console.log(`Farmer: ${k}, Area: ${farmers[k].workarea}, Tasks: ${farmers[k].tasks.sort().join(', ')}`);

    });

}

solve([
    "2",
    "John garden watering,weeding",
    "Mary barn feeding,cleaning",
    "Execute / John / garden / watering",
    "Execute / Mary / garden / feeding",
    "Learn Task / John / planting",
    "Execute / John / garden / planting",
    "Change Area / Mary / garden",
    "Execute / Mary / garden / cleaning",
    "End"
]);

// John has executed the task: watering!
// Mary cannot execute the task: feeding.
// John has learned a new task: planting.
// John has executed the task: planting!
// Mary has changed their work area to: garden
// Mary has executed the task: cleaning!
// Farmer: John, Area: garden, Tasks: planting, watering, weeding
// Farmer: Mary, Area: garden, Tasks: cleaning, feeding

// solve([
//     "3",
//     "Alex apiary harvesting,honeycomb",
//     "Emma barn milking,cleaning",
//     "Chris garden planting,weeding",
//     "Execute / Alex / apiary / harvesting",
//     "Learn Task / Alex / beeswax",
//     "Execute / Alex / apiary / beeswax",
//     "Change Area / Emma / apiary",
//     "Execute / Emma / apiary / milking",
//     "Execute / Chris / garden / watering",
//     "Learn Task / Chris / pruning",
//     "Execute / Chris / garden / pruning",
//     "End"
// ]);

//   Alex has executed the task: harvesting!
// Alex has learned a new task: beeswax.
// Alex has executed the task: beeswax!
// Emma has changed their work area to: apiary
// Emma has executed the task: milking!
// Chris cannot execute the task: watering.
// Chris has learned a new task: pruning.
// Chris has executed the task: pruning!
// Farmer: Alex, Area: apiary, Tasks: beeswax, harvesting, honeycomb
// Farmer: Emma, Area: apiary, Tasks: cleaning, milking
// Farmer: Chris, Area: garden, Tasks: planting, pruning, weeding

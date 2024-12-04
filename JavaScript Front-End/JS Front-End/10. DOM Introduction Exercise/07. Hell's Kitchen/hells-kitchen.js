function solve() {
    let input = document.querySelector('#inputs textarea').value;
    let bestRestaurantP = document.querySelector('#outputs #bestRestaurant p');
    let bestWorkerP = document.querySelector('#outputs #workers p');

    if (!input) { return; }

    const data = JSON.parse(input).reduce((acc, data) => {

        const [name, workersData] = data.split(' - ');

        const workers = workersData.split(', ').map(w => {
            const [workerName, salary] = w.split(' ');
            return { name: workerName, salary: Number(salary) };
        })

        if (!acc.hasOwnProperty(name)) {
            acc[name] = { workers: [] };
        }
        acc[name].workers.push(...workers);
        return acc;

    }, {});

    function getAvg(restaurant) {
        return restaurant.workers.reduce((acc, w) => {
            return acc + w.salary
        }, 0) / restaurant.workers.length;
    }

    const [bestRestaurantName] = Object.keys(data).sort((a, b) => getAvg(data[b]) - getAvg(data[a]));
    const bestWorkers = data[bestRestaurantName].workers.toSorted((a, b) => b.salary - a.salary);

    bestRestaurantP.textContent =
        `Name: ${bestRestaurantName} Average Salary: ${getAvg(data[bestRestaurantName]).toFixed(2)} Best Salary: ${bestWorkers[0].salary.toFixed(2)}`;

    bestWorkerP.textContent = 
        bestWorkers.map(w => `Name: ${w.name} With Salary: ${w.salary}`).join(' ');
}
function solve(movieInfo) {
    let movies = [];

    movieInfo.forEach(line => {
        if (line.includes('addMovie')) {
            let [name] = line.split('addMovie ').filter(Boolean);
            movies.push({ name });
        }
        else if (line.includes('directedBy')) {
            let [movieName, director] = line.split(' directedBy ');

            const movie = movies.find(x => x.name == movieName);
            if(movie != undefined){
                movie.director = director;
            }
        }
        else if (line.includes('onDate')) {
            let [movieName, date] = line.split(' onDate ');
            const movie = movies.find(x => x.name == movieName);

            if(movie != undefined){
                movie.date = date;
            }
        }
    });

    movies.forEach( (movie) =>{
        if(movie.director != undefined && movie.date != undefined){
            console.log(JSON.stringify(movie));
        }
    });
}

solve([
    'addMovie Fast and Furious',
    'addMovie Godfather',
    'Inception directedBy Christopher Nolan',
    'Godfather directedBy Francis Ford Coppola',
    'Godfather onDate 29.07.2018',
    'Fast and Furious onDate 30.07.2018',
    'Batman onDate 01.08.2018',
    'Fast and Furious directedBy Rob Cohen']);
// {"name":"Fast and Furious","date":"30.07.2018","director":"Rob Cohen"}
// {"name":"Godfather","director":"Francis Ford Coppola","date":"29.07.2018"}

solve([
    'addMovie The Avengers',
    'addMovie Superman',
    'The Avengers directedBy Anthony Russo',
    'The Avengers onDate 30.07.2010',
    'Captain America onDate 30.07.2010',
    'Captain America directedBy Joe Russo'
]);
// {"name":"The Avengers","director":"Anthony Russo","date":"30.07.2010"} 
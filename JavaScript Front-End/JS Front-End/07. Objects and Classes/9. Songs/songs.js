function solve(input) {
    class Song {
        constructor(typeList, name, time) {
            this.typeList = typeList,
                this.name = name,
                this.time = time
        }
    }

    let songsCount = input.shift();
    let typeListFilter = input.pop();
    let songs = [];

    input.forEach(songData => {
        let [type, name, time] = songData.split('_');
        let song = new Song(type, name, time);

        if (typeListFilter == "all") {
            songs.push(song);
        }
        else {
            if (typeListFilter == type) {
                songs.push(song);
            }
        }
    });

    for (const song of songs) {
        console.log(song.name);
    }
}

solve([3,
    'favourite_DownTown_3:14',
    'favourite_Kiss_4:16',
    'favourite_Smooth Criminal_4:01',
    'favourite']
);
// DownTown
// Kiss
// Smooth Criminal

solve([4,
    'favourite_DownTown_3:14',
    'listenLater_Andalouse_3:24',
    'favourite_In To The Night_3:58',
    'favourite_Live It Up_3:48',
    'listenLater']
    );
//Andalouse

solve([2,
    'like_Replay_3:15',
    'ban_Photoshop_3:48',
    'all']
    );
//Replay
//Photoshop
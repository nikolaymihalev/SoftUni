function solve(input) {
    class Song{
        constructor(name, time){
            this.name = name;
            this.time = time;
        }

        print(){
            console.log(this.name);
        }
    }

    const count = input.shift();
    const songs = {};
    const allSongs = [];

    for (let i = 0; i < count; i++) {
        const [typeList, name, time] = input.shift().split('_');

        if(!songs[typeList]){
            songs[typeList] = [];
        }

        const newSong = new Song(name, time);
        songs[typeList].push(newSong);
        allSongs.push(newSong);
    }

    const typeList = input.shift();

    if(typeList === 'all'){
        allSongs.forEach(song=>song.print());
    }else {
        songs[typeList].forEach(song => song.print());
    }
}
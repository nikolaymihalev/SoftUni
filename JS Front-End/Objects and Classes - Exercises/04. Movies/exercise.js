function solve(input) {
    const database = [];

    input.forEach((command)=>{
        if(command.startsWith("addMovie")){
            const [name]=command.split("addMovie ").filter(Boolean);
            database.push({name});
        }else if(command.includes("directedBy")){
            const [name, director] = command.split(" directedBy ").filter(Boolean);

            const movie = database.find((x)=>x?.name===name);
            if(movie?.name){
                movie.director = director;
            }
        }else if(command.includes("onDate")){
            const [name, date] = command.split(" onDate ").filter(Boolean);

            const movie = database.find((x)=> x?.name===name);

            if(movie?.name){
                movie.date = date;
            }
        }
    });

    database.filter((x)=>x.name && x.director && x.date)
        .forEach((x)=>console.log(JSON.stringify(x)));
}
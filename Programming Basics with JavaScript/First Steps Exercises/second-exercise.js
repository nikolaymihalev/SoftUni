function demo(input){
    let pagesCount = Number(input[0]);
    let pagesPerHour = Number(input[1]);
    let day = Number(input[2]);

    console.log(pagesCount/pagesPerHour/day);
}
demo(["212","20","2"]);
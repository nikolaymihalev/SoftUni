function spaceship(input){
    let shipWidth = Number(input[0]);
    let shipLength = Number(input[1]);
    let shipHeight = Number(input[2]);
    let averageAstroHeight = Number(input[3]);

    let volume = shipWidth*shipLength*shipHeight;
    let volumeForOneRoom = (averageAstroHeight+0.4)*2*2;
    let result = "";
    let spaceForHumans = Math.floor(volume/volumeForOneRoom);    

    if(spaceForHumans>=3 && spaceForHumans <= 10){
        result = `The spacecraft holds ${spaceForHumans} astronauts.`;
    }else if(spaceForHumans<3){
        result = "The spacecraft is too small.";
    }else if(spaceForHumans>10){
        result = "The spacecraft is too big.";
    }

    console.log(result);
}

spaceship(["3.5","4","5","1.7"]);
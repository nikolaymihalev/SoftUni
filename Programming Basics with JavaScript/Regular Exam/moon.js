function moon(input){
    let averageSpeed = Number(input[0]);
    let neededFuelFor100 = Number(input[1]);

    let totalDistance = 768800;
    let timeOnMoon = 3;

    let totalTime = Math.ceil(totalDistance/averageSpeed)+timeOnMoon;
    let fuel = (neededFuelFor100*totalDistance)/100;

    console.log(totalTime);
    console.log(fuel);
}

moon(["10000","5"]);
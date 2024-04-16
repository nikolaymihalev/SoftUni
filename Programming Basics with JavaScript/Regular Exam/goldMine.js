function goldMine(input){
    let locations = Number(input[0]);
    let index = 1;

    for (let i = 0; i < locations; i++) {
        let averageGoldPerDay = Number(input[index]);
        index++;

        let daysCount = Number(input[index]);
        index++;

        let sumGold = 0;

        for (let j = 0; j < daysCount; j++) {
            let gold = Number(input[index]);
            sumGold+=gold;
            index++;
        }
        let averageGold = sumGold/daysCount;

        if(averageGold>=averageGoldPerDay){
            console.log(`Good job! Average gold per day: ${averageGold.toFixed(2)}.`);
        }else {
            console.log(`You need ${(averageGoldPerDay-averageGold).toFixed(2)} gold.`);
        }
    }
}

goldMine([
    "2",
    "10",
    "3",
    "10",
    "10",
    "11",
    "20",
    "2",
    "20",
    "10"
    ]);
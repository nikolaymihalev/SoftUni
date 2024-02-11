function lunchBreak(input){
    let name = input[0];
    let episodeDuration = Number(input[1]);
    let rest = Number(input[2]);

    let lunchTime = rest/8;
    let restTime = rest/4;
    let timeLeft = rest-lunchTime-restTime;

    if(timeLeft>= episodeDuration){
        console.log(`You have enough time to watch ${name} and left with ${Math.ceil(timeLeft-episodeDuration)} minutes free time.`);
    }else {
        console.log(`You don't have enough time to watch ${name}, you need ${Math.ceil(episodeDuration-timeLeft)} more minutes.`);
    }
}
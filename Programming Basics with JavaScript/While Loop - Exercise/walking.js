function walking(input){
    let targetSteps = 10000;
    let stepsSum = 0;
    let index = 0;
    let row = input[index];

    while (row !== "Going home") {
        let steps = Number(row);
        stepsSum+=steps;

        if(stepsSum >= targetSteps){
            console.log('Goal reached! Good job!');
            console.log(`${stepsSum - targetSteps} steps over the goal!`);
            break;
        }
        index++;
        row = input[index];
    }

    if(row === "Going home"){
        let stepsToHome = Number(input[index+1]);
        stepsSum+=stepsToHome;

        if(stepsSum >= targetSteps){
            console.log('Goal reached! Good job!');
            console.log(`${stepsSum - targetSteps} steps over the goal!`);
        }
        else {
            console.log(`${targetSteps - stepsSum} more steps to reach goal.`);
        }
    }
}
function vacation(input){
    let neededMoney = Number(input[0]);
    let availableMoney = Number(input[1]);

    let index = 2;
    let currentRow = input[index];
    let spendDays = 0;
    let totalDays = 0;

    while (availableMoney < neededMoney) {
        totalDays++;

        if(currentRow === 'spend'){
            spendDays++;
            if(spendDays === 5){
                console.log("You can't save the money.");
                console.log(totalDays);
                break;
            }
            index++;
            let moneyToSpend = Number(input[index]);
            availableMoney -= moneyToSpend;

            if(availableMoney < 0){
                availableMoney = 0;
            }
        }
        else if(currentRow === 'save'){
            index++;
            let moneyToSave = Number(input[index]);
            availableMoney+=moneyToSave;
            spendDays = 0;
        }

        index++;
        currentRow = input[index];
    }

    if(availableMoney >= neededMoney){
        console.log(`You saved the money for ${totalDays} days.`);
    }
}
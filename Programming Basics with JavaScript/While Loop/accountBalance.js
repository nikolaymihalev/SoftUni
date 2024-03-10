function accountBalance(input){
    let account = 0;
    let count = 1;

    let command = input[0];

    while(command !== "NoMoreMoney"){
        let money = Number(command);

        if(money<0){
            console.log("Invalid operation!");
            break;
        }

        account+=money;
        console.log(`Increase: ${money.toFixed(2)}`);
        command = input[count];
        count++;
    }
    console.log(`Total: ${account.toFixed(2)}`);
}
function cleverLily(input){
    let age = Number(input[0]);
    let washerPrice = Number(input[1]);
    let pricePerToy = Number(input[2]);

    let moneySaved = 0;
    let moneyGiven = 10;

    for (let i = 1; i <= age; i++) {
        if (i % 2 === 0) {
            moneySaved += moneyGiven - 1;
            moneyGiven += 10;
        }else {
            moneySaved += pricePerToy;
        }
    }

    if (moneySaved >= washerPrice) {
        console.log(`Yes! ${(moneySaved-washerPrice).toFixed(2)}`);
    }else{
        console.log(`No! ${(washerPrice - moneySaved).toFixed(2)}`);
    }
}
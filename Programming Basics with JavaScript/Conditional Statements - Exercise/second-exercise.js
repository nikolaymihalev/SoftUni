function film(input){
    let budget = Number(input[0]);
    let actors = Number(input[1]);
    let clothesPrice = Number(input[2]);

    let price = actors*clothesPrice;
    if(actors>150){
        price*=0.9;
    }

    let sum = budget*0.1 + price;
    let money = budget - sum;

    if(money>=0){
        console.log("Action!");
        console.log(`Wingard starts filming with ${money.toFixed(2)} leva left.`);
    }else{
        console.log("Not enough money!");
        console.log(`Wingard needs ${(Math.abs(money)).toFixed(2)} leva more.`);
    }
}

film(["9587.88","222","55.68"]);
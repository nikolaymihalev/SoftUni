function toyShop(input){
    let tripPrice = Number(input[0]);
    let puzzles = Number(input[1]);
    let dolss = Number(input[2]);
    let bears = Number(input[3]);
    let minions = Number(input[4]);
    let trucks = Number(input[5]);

    let sum = puzzles*2.6 + dolss*3 + bears*4.1 + minions*8.2 + trucks*2;
    let count = puzzles+dolss+bears+minions+trucks;

    if(count>=50){
        sum*=0.75;
    }

    let rent = sum*0.9;

    if(rent >= tripPrice){
        console.log(`Yes! ${(rent-tripPrice).toFixed(2)} lv left.`);
    }else {
        console.log(`Not enough money! ${(tripPrice-rent).toFixed(2)} lv needed.`);
    }
}
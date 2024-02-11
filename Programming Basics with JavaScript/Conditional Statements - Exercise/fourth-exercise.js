function shopping(input){
    let budget = Number(input[0]);
    let GPU = Number(input[1]);
    let CPU = Number(input[2]);
    let RAM = Number(input[3]);

    let GPUSum = GPU*250;
    let CPUSum = GPUSum*0.35*CPU;
    let RAMSum = GPUSum*0.1*RAM;
    let sum = GPUSum+CPUSum+RAMSum;

    if(GPU>CPU){
        sum*=0.85;
    }

    if(sum>budget){
        console.log(`Not enough money! You need ${(sum-budget).toFixed(2)} leva more!`);
    }else{
        console.log(`You have ${(budget-sum).toFixed(2)} leva left!`);
    }
}

shopping(["920.45","3","1","1"])
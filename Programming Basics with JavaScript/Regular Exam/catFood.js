function catFood(input){
    let catsCount = input[0];

    let firstGroupCount = 0;
    let secondGroupCount = 0;
    let thirdGroupCount = 0;
    let kilos = 0;

    for (let i = 1; i <= catsCount; i++) {
        let food = Number(input[i]);

        if(food>= 100 && food < 200){
            firstGroupCount++;
        }else if(food>= 200 && food < 300){
            secondGroupCount++;
        }else if(food>= 300 && food < 400){
            thirdGroupCount++;
        }

        kilos+=food;
    }

    let price= (kilos/1000)*12.45;

    console.log(`Group 1: ${firstGroupCount} cats.`);
    console.log(`Group 2: ${secondGroupCount} cats.`);
    console.log(`Group 3: ${thirdGroupCount} cats.`);
    console.log(`Price for food per day: ${price.toFixed(2)} lv.`);
}

catFood(["6",
    "102",
    "236",
    "123",
    "399",
    "342",
    "222"
    ]);
function christmasGifts(input){
    let index = 0;
    
    let kidsCount = 0;
    let adultsCount = 0;
    
    let toysPrice = 5;
    let sweaterPrice = 15;
    
    let command = input[index];
    
    while (command !== "Christmas") {
        let person = Number(command);

        if(person<=16){
            kidsCount++;
        }else {
            adultsCount++;
        }

        index++;
        command = input[index];
    }

    console.log(`Number of adults: ${adultsCount}`);
    console.log(`Number of kids: ${kidsCount}`);
    console.log(`Money for toys: ${kidsCount*toysPrice}`);
    console.log(`Money for sweaters: ${adultsCount*sweaterPrice}`);
}

christmasGifts([
    "16",
    "20",
    "46",
    "12",
    "8",
    "20",
    "49",
    "Christmas"
]);
function maxNumber(input){
    let command = input[0];

    let count = 1;
    let myMaxNumber = Number.MIN_SAFE_INTEGER;

    while(command!=="Stop"){
        let num = Number(command);

        if(myMaxNumber<num){
            myMaxNumber = num;
        }

        command = input[count];
        count++;
    }
    console.log(myMaxNumber);
}
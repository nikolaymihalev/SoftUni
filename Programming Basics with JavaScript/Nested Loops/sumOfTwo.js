function sumOfTwo(input){
    let start = Number(input[0]);
    let end = Number(input[1]);
    let magicalNumber = Number(input[2]);

    let isFound = false;
    let count = 0;

    for (let x = start; x <= end; x++) {
        for (let y = start; y <= end; y++) {
            let sum = x+y;
            count++;
            if(sum === magicalNumber){
                console.log(`Combination N:${count} (${x} + ${y} = ${magicalNumber})`);
                isFound = true;
                break;
            }            
        }
        if(isFound){
            break;
        }
    }       
    if(!isFound)
        console.log(`${count} combinations - neither equals ${magicalNumber}`);
}
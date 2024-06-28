function solve(number) {
    let array = `${number}`.split('');
    let condition = true;
    let sum = 0;

    for (let i = 0; i < array.length; i++) {
        if(i>0){
            if(array[i]!==array[i-1]){
                condition = false;
            }
        }

        sum += parseInt(array[i]);
    }

    console.log(condition);
    console.log(sum);
}
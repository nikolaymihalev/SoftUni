function sumNumbers(input){
    let target = Number(input[0]);
    let sum = 0;
    let count = 1;

    while(sum<target){
        let num = Number(input[count]);
        count++;
        sum+=num;
    }
    console.log(sum);
}
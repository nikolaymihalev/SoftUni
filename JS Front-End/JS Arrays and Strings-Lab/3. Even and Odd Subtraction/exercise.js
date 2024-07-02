function solve(array) {
    let oddSum = 0;
    let evSum = 0;

    for (const number of array) {
        if(number % 2 === 0)
            evSum+=number;
        else
            oddSum+=number
    }

    console.log(evSum-oddSum);
}
function solve(x) {
    const notMessage = "It's not so perfect."

    if(x < 0 || !Number.isInteger(x)){
        console.log(notMessage);
        return;
    }

    const half = x / 2;
    const divisors = [];

    for (let i = 1; i <= half; i++) {
        if(x % i !== 0)
            continue
        
        divisors.push(i);
    }

    console.log(divisors.reduce((a,b)=>a + b, 0) === x ? "We have a perfect number!" : notMessage);
}

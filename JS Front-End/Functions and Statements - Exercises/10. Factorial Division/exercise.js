function solve(x1, x2) {
    const findFactorial = (n) => {
        if(n < 0)
            return -1;

        if(n === 0)
            return 1;

        return n * findFactorial(n-1);
    };

    console.log((findFactorial(x1) / findFactorial(x2)).toFixed(2)); 
}
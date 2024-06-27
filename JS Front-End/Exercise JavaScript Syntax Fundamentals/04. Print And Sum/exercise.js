function solve(x1, x2) {
    let stringRes = '';  
    let sum = 0;
    for (let i = x1; i <= x2; i++) {
        stringRes += i + " ";       
        sum += i;
    }

    console.log(stringRes);
    console.log(`Sum: ${sum}`);
}

solve(5, 10)
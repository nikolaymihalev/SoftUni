function solve(x1) {
    let stringVal = x1.toString();
    let result = 0;
    for (let i = 0; i < stringVal.length; i++) {
        let number = parseInt(stringVal[i]);
        result += number;
    }

    console.log(result);
}

solve(245678);
function solve(x) {
    const printNumbers = (number, seperator = " ")=>{
        return `${number}${seperator}`.repeat(number).trim();
    }

    for (let i = 0; i < x; i++) {
        console.log(printNumbers(x));
    }
}
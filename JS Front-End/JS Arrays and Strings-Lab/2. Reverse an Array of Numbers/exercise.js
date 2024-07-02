function solve(x, array) {
    let result = [];

    for (let i = 0; i < x; i++) {
        result.push(array[i]);
    }

    console.log(result.reverse().join(' '));
}
function solve(a, b, c) {
    const sum = (x, y) => x + y;
    const subtract = (x, y) =>  x - y;

    const result = subtract(sum(a, b), c);

    console.log(result);
}
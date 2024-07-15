function solve(x1, x2, operation) {
    let result = 0;

    switch (operation) {
        case 'multiply':
            result = x1 * x2;
            break;
        case 'divide':
            result = x1 / x2;
            break;
        case 'add':
            result = x1 + x2;
            break;
        case 'subtract':
            result = x1 - x2;
            break;
    }

    console.log(result);
}
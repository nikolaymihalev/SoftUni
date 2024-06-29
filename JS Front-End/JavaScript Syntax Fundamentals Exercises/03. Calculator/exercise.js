function sovle(x1, op, x2) {
    let num = 0;
    switch (op) {
        case '+':
            num = x1+x2;
            break;
    
        case '-':
            num = x1-x2;
            break;
        case '/':
            num = x1/x2;
            break;
        case '*':
            num = x1*x2;
            break;
    }

    console.log(num.toFixed(2));
}
function solve(product, count) {
    let result = 0;

    switch(product){
        case "coffee": result = count * 1.5;
            break;
        case "water": result = count * 1;
            break;
        case "coke": result = count * 1.4;
            break;
        case "snacks": result = count * 2;
            break;
    }

    console.log(result.toFixed(2));
}
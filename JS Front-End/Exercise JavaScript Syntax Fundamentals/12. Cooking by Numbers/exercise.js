function solve(number, op1, op2, op3, op4, op5) {
    let x = parseInt(number);

    switch(op1){
        case 'chop':
            x /= 2;
            break;
        case 'dice':
            x = Math.sqrt(x); 
            break;
        case 'spice':
            x += 1;
            break;
        case 'bake':
            x *= 3;
            break;
        case 'fillet':
            x -= x*0.2;
            break;
    }
    console.log(x);

    switch(op2){
        case 'chop':
            x /= 2;
            break;
        case 'dice':
            x = Math.sqrt(x); 
            break;
        case 'spice':
            x += 1;
            break;
        case 'bake':
            x *= 3;
            break;
        case 'fillet':
            x -= x*0.2;
            break;
    }
    console.log(x);

    switch(op3){
        case 'chop':
            x /= 2;
            break;
        case 'dice':
            x = Math.sqrt(x); 
            break;
        case 'spice':
            x += 1;
            break;
        case 'bake':
            x *= 3;
            break;
        case 'fillet':
            x -= x*0.2;
            break;
    }
    console.log(x);


    switch(op4){
        case 'chop':
            x /= 2;
            break;
        case 'dice':
            x = Math.sqrt(x); 
            break;
        case 'spice':
            x += 1;
            break;
        case 'bake':
            x *= 3;
            break;
        case 'fillet':
            x -= x*0.2;
            break;
    }
    console.log(x);

    switch(op5){
        case 'chop':
            x /= 2;
            break;
        case 'dice':
            x = Math.sqrt(x); 
            break;
        case 'spice':
            x += 1;
            break;
        case 'bake':
            x *= 3;
            break;
        case 'fillet':
            x -= x*0.2;
            break;
    }
    console.log(x);
}
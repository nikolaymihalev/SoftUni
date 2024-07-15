function solve(x1, x2, x3) {
    let array = [x1, x2, x3];
    let count = 0;

    for (let i = 0; i < 3; i++) {
        if(array[i] < 0){
            count++;
        }
    }

    if(count === 3 || count === 1){
        console.log('Negative');
    }else{
        console.log('Positive');
    }    
}
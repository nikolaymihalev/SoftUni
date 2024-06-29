function sovle(x1, y1, x2, y2) {
    if(Number.isInteger(Math.sqrt(Math.pow((x1-y1),2))))
        console.log(`{${x1}, ${y1}} to {0, 0} is valid`);
    else 
        console.log(`{${x1}, ${y1}} to {0, 0} is invalid`);

    if(Number.isInteger(Math.sqrt(Math.pow((x2-y2),2))))
        console.log(`{${x2}, ${y2}} to {0, 0} is valid`);
    else 
        console.log(`{${x2}, ${y2}} to {0, 0} is invalid`);
}   

sovle(2, 1, 1, 1)
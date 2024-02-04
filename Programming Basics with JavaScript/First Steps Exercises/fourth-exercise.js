function demo(input){
    let nylon = Number(input[0]);
    let paint = Number(input[1]);
    let thinner = Number(input[2]);
    let hours = Number(input[3]);

    let sum = (nylon+2)*1.5 + (paint*1.1)*14.5 + thinner*5 +  0.4;
    let workers = sum*0.3*hours;
    
    console.log(sum+workers);
}
demo(["10","11","4","8"]);
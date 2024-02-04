function demo(input){
    let tax = Number(input[0]);

    let shoes = tax*0.6;
    let apparel = shoes*0.8;
    let ball = apparel/4;
    let extraStuff = ball/5;

    console.log(tax+shoes+apparel+ball+extraStuff);
}
demo(["365"]);
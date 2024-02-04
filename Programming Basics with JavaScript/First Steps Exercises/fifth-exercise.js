function demo(input){
    let chickenMenus = Number(input[0]);
    let fishMenus = Number(input[1]);
    let vegyMenus = Number(input[2]);

    let sum = (chickenMenus*10.35 + fishMenus*12.4 + vegyMenus*8.15);
    console.log(sum + sum*0.2 + 2.5);
}
demo(["2","4","3"]);
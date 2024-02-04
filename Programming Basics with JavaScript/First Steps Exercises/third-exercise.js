function demo(input){
    let pens = Number(input[0]);
    let markers = Number(input[1]);
    let liters = Number(input[2]);
    let discount = Number(input[3]);
    
    let totalSum = pens*5.8 + markers*7.2 + liters*1.2 ;

    console.log(totalSum - totalSum*(discount/100));
}
demo(["2","3","4","25"]);
function demo(input){
    let deposit = Number(input[0]);
    let period = Number(input[1]);
    let percent = parseFloat(input[2]);

    console.log(deposit+period*((deposit * percent / 100)/12));
}
demo(["200","3","5.7"]);
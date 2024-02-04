function demo(input){
    let l = Number(input[0]);
    let w = Number(input[1]);
    let h = Number(input[2]);
    let percent = Number(input[3]);

    console.log(((l*w*h)/1000)*(1-(percent/100)));
}
demo(["85","75","47","17"]);
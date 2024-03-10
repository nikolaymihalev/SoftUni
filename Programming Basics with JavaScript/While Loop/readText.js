function readText(input){
    let count = 0;
    
    while(true){
        let text = input[count];

        if(text==="Stop")
            break;

        console.log(text);
        count++;
    }
}
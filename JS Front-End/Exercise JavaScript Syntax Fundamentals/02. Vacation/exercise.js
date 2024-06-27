function solve(count, group, day) {
    let price = 0; 

    for (let i = 1; i <= count; i++) {
        switch(group){
            case "Students": 
                switch(day){
                    case "Friday": price+=8.45; break;
                    case "Saturday": price+=9.8; break;
                    case "Sunday": price+=10.46; break;
                }
                break;
            case "Business": 
                switch(day){
                    case "Friday": price+=10.9; break;
                    case "Saturday": price+=15.6; break;
                    case "Sunday": price+=16; break;
                }
                break;
            case "Regular": 
                switch(day){
                    case "Friday": price+=15; break;
                    case "Saturday": price+=20; break;
                    case "Sunday": price+=22.5; break;
                }
            break;
        }
    }
    
    if(group === "Business" && count >= 100){
        switch(day){
            case "Friday": price-=109; break;
            case "Saturday": price-=156; break;
            case "Sunday": price-=160; break;
        }
    }

    if(group === "Students" && count >= 30)
        price *= 0.85;

    if(group === "Regular" && count >= 10 && count <= 20)
        price *= 0.95;

    console.log(`Total price: ${price.toFixed(2)}`);
}

solve(30,"Students","Sunday");
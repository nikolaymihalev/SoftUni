function catLife(input){
    let breed = input[0];
    let gender = input[1];

    let months = 0;
    let result = "";

    if(breed === "British Shorthair"){

        if(gender === "m"){
            months = Math.floor(13*12)/6;
        }else {
            months = Math.floor(14*12)/6;
        }

        result = `${months} cat months`;
    }else if(breed === "Siamese"){

        if(gender === "m"){
            months = Math.floor(15*12)/6;
        }else {
            months = Math.floor(16*12)/6;
        }

        result = `${months} cat months`;
    }else if(breed === "Persian"){
        if(gender === "m"){
            months = Math.floor(14*12)/6;
        }else {
            months = Math.floor(15*12)/6;
        }

        result = `${months} cat months`;
    }else if(breed === "Ragdoll"){
        if(gender === "m"){
            months = Math.floor(16*12)/6;
        }else {
            months = Math.floor(17*12)/6;
        }

        result = `${months} cat months`;
    }else if(breed === "American Shorthair"){
        if(gender === "m"){
            months = Math.floor(12*12)/6;
        }else {
            months = Math.floor(13*12)/6;
        }

        result = `${months} cat months`;
    }else if(breed === "Siberian"){
        if(gender === "m"){
            months = Math.floor(11*12)/6;
        }else {
            months = Math.floor(12*12)/6;
        }

        result = `${months} cat months`;
    }else {
        result = `${breed} is invalid cat!`;
    }

    console.log(result);
}

catLife(["Siberian","m"]);
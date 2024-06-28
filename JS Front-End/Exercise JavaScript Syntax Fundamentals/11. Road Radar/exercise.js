function solve(speed, area) {
    const motorwayLimit = 130;
    const interStateLimit = 90;
    const cityLimit = 50;
    const residentLimit = 20;

    let condition = true;
    let currentLimit = 0;

    switch (area) {
        case 'motorway':
            currentLimit = motorwayLimit;
            if(speed > motorwayLimit)
                condition = false;
            break;
        case 'interstate':
            currentLimit = interStateLimit;
            if(speed > interStateLimit)
                condition = false;
            break;
        case 'city':
            currentLimit = cityLimit;
            if(speed > cityLimit)
                condition = false;
            break;
        case 'residential':
            currentLimit = residentLimit;
            if(speed > residentLimit)
                condition = false;
            break;
    }

    if(condition){
        console.log(`Driving ${speed} km/h in a ${currentLimit} zone`);
    }else {
        let status = '';
        let difference = speed - currentLimit;

        if(difference <= 20){
            status = 'speeding';
        }else if(difference <= 40){
            status = 'excessive speeding';
        }else {
            status = 'reckless driving';
        }

        console.log(`The speed is ${difference} km/h faster than the allowed speed of ${currentLimit} - ${status}`);
    }
}
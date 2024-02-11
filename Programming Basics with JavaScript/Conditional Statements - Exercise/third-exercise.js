function swimingRecord(input){
    let worldRecord = Number(input[0]);
    let distance = Number(input[1]);
    let time = Number(input[2]);

    let metres = distance*time;
    let delay = Math.floor(distance/15)*12.5;
    let total = metres+delay;

    
    if(total<worldRecord){
        console.log(`Yes, he succeeded! The new world record is ${total.toFixed(2)} seconds.`);
    }else {
        console.log(`No, he failed! He was ${(total-worldRecord).toFixed(2)} seconds slower.`);
    }
}

swimingRecord(["10464","1500","20"]);
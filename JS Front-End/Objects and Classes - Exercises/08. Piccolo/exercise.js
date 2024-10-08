function solve(input) {
    const parkingLot = input.reduce((acc, curr)=>{
        const [operation, plateNumber] = curr.split(", ");
        switch(operation){
            case "IN": 
                if(!acc.hasOwnProperty(plateNumber)){
                    acc[plateNumber] = false;
                }
                acc[plateNumber] = true;
                break;
            case "OUT": 
                if(acc.hasOwnProperty(plateNumber)){
                    acc[plateNumber] = false;
                }
                break;
        }
        return acc;
    }, {});

    const inCars= Object.keys(parkingLot).filter((key)=>parkingLot[key]);

    if(inCars.length===0){
        console.log("Parking Lot is Empty");
        return;
    }

    Object.keys(parkingLot)
        .filter((key)=>parkingLot[key])
        .sort((a,b)=> a.localeCompare(b))
        .forEach((x)=>console.log(x));
}
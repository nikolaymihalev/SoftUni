function building(input){
    let floors = Number(input[0]);
    let rooms = Number(input[1]);

    for (let floor = floors; floor > 0; floor--) {
        let buffer = "";
        for (let room = 0; room < rooms; room++) {
            if (floor === floors) {                
                buffer += `L${floor}${room} `;
            }else if(floor % 2 === 0){
                buffer += `O${floor}${room} `;
            }else{
                buffer += `A${floor}${room} `;
            }
        }
        console.log(buffer);        
    }
}
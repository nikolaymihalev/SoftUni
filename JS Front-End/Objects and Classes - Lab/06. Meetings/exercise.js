function solve(input) {
    const meetings = {};

    for (const entry of input) {
        const [day, name] = entry.split(' ');

        if(!meetings[day]){
            meetings[day] = name;
            console.log(`Scheduled for ${day}`);
        }else {
            console.log(`Conflict on ${day}!`);            
        }        
    }

    for (const day in meetings) {
        console.log(`${day} -> ${meetings[day]}`);
    }
}
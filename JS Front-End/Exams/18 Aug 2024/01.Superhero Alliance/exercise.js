function solve(input) {
    let count = Number(input.shift());
    let heroes = {};
    let names = [];

    for (let i = 0; i < count; i++) {
        let [name, superpower, energy] = input.shift().split('-');

        heroes[name] = {
            superpowers: superpower.split(','),
            energy: Number(energy)
        };

        names.push(name);
    }

    let command = input.shift();

    while(command !== "Evil Defeated!"){
        let data = command.split(" * ");

        let action = data[0];
        let superHeroName = data[1];

        switch (action) {
            case "Use Power":
                let superpower = data[2];
                let requiredEnergy = Number(data[3]);

                if(heroes[superHeroName].superpowers.includes(superpower) && (heroes[superHeroName].energy > 0 && heroes[superHeroName].energy >= requiredEnergy)){
                    heroes[superHeroName].energy-=requiredEnergy;

                    console.log(`${superHeroName} has used ${superpower} and now has ${heroes[superHeroName].energy} energy!`);                   
                }else {
                    console.log(`${superHeroName} is unable to use ${superpower} or lacks energy!`);                    
                }
                break;
        
            case "Train":
                let trainEnergy = Number(data[2]);

                if(heroes[superHeroName].energy<100){

                    if(heroes[superHeroName].energy+trainEnergy>100){
                        console.log(`${superHeroName} has trained and gained ${100-heroes[superHeroName].energy} energy!`);
                        
                        heroes[superHeroName].energy = 100;
                    }else {
                        heroes[superHeroName].energy += trainEnergy;
                        console.log(`${superHeroName} has trained and gained ${trainEnergy} energy!`);
                    }
                    
                }else {
                    console.log(`${superHeroName} is already at full energy!`);                    
                }
                break;

            case "Learn":
                let newPower = data[2];

                if(heroes[superHeroName].superpowers.includes(newPower)){
                    console.log(`${superHeroName} already knows ${newPower}.`);                    
                }else {
                    heroes[superHeroName].superpowers.push(newPower);
                    console.log(`${superHeroName} has learned ${newPower}!`);                    
                }
                break;

        }

        command = input.shift();
    }

    names.forEach((x)=>{
        console.log(`Superhero: ${x}`);
        console.log(`- Superpowers: ${heroes[x].superpowers.join(", ")}`);
        console.log(`- Energy: ${heroes[x].energy}`);                
    })
}
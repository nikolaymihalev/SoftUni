function manageAstronauts(input) {
  let astronauts = [];
  
  const n = Number(input.shift());
  for (let i = 0; i < n; i++) {
    const [name, oxygen, energy] = input.shift().split(' ');
    astronauts.push({ name, oxygen: Number(oxygen), energy: Number(energy) });
  }
  
  while (input.length > 0) {
    const command = input.shift();
    
    if (command === 'End') {
      break;
    }
    
    const [action, name, value1] = command.split(' - ');
    
    if (action === 'Explore') {
      const energyNeeded = Number(value1);
      const astronaut = findAstronautByName(name);
      
      if (astronaut && astronaut.energy >= energyNeeded) {
        astronaut.energy -= energyNeeded;
        console.log(`${astronaut.name} has successfully explored a new area and now has ${astronaut.energy} energy!`);
      } else if (astronaut) {
        console.log(`${astronaut.name} does not have enough energy to explore!`);
      }
    } else if (action === 'Refuel') {
      const amount = Number(value1);
      const astronaut = findAstronautByName(name);
      
      if (astronaut) {
        const energyRecovered = Math.min(amount, 200 - astronaut.energy);
        astronaut.energy += energyRecovered;
        console.log(`${astronaut.name} refueled their energy by ${energyRecovered}!`);
      }
    } else if (action === 'Breathe') {
      const amount = Number(value1);
      const astronaut = findAstronautByName(name);
      
      if (astronaut) {
        const oxygenRecovered = Math.min(amount, 100 - astronaut.oxygen);
        astronaut.oxygen += oxygenRecovered;
        console.log(`${astronaut.name} took a breath and recovered ${oxygenRecovered} oxygen!`);
      }
    }
  }
  
  astronauts.forEach(astronaut => {
    console.log(`Astronaut: ${astronaut.name}, Oxygen: ${astronaut.oxygen}, Energy: ${astronaut.energy}`);
  });
  
  function findAstronautByName(name) {
    return astronauts.find(astronaut => astronaut.name === name);
  }
}
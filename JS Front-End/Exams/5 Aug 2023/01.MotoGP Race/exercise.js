function solve(input) {
  let n = parseInt(input[0]);
  let riders = {};

  function printFinalPosition(rider) {
    console.log(`${rider}`);
    console.log(`  Final position: ${riders[rider].position}`);
    delete riders[rider];
  }

  for (let i = 1; i <= n; i++) {
    let [rider, fuelCapacity, position] = input[i].split('|');
    riders[rider] = {
      fuelCapacity: parseFloat(fuelCapacity),
      position: parseInt(position)
    };
  }

  let index = n + 1;
  while (index < input.length && input[index] !== "Finish") {
    let [action, rider, arg1, arg2] = input[index].split(' - ');

    if (action === "StopForFuel") {
      let minFuel = parseFloat(arg1);
      let newPos = parseInt(arg2);
      if (riders[rider].fuelCapacity < minFuel) {
        console.log(`${rider} stopped to refuel but lost his position, now he is ${newPos}.`);
        riders[rider].position = newPos;
      } else {
        console.log(`${rider} does not need to stop for fuel!`);
      }
    } else if (action === "Overtaking") {
      let rider1 = rider;
      let rider2 = arg1;
      if (riders[rider1].position < riders[rider2].position) {
        [riders[rider1].position, riders[rider2].position] = [riders[rider2].position, riders[rider1].position];
        console.log(`${rider1} overtook ${rider2}!`);
      }
    } else if (action === "EngineFail") {
      let lapsLeft = parseInt(arg1);
      console.log(`${rider} is out of the race because of a technical issue, ${lapsLeft} laps before the finish.`);
      delete riders[rider];
    }

    index++;
  }

  for (let rider in riders) {
    printFinalPosition(rider);
  }
}
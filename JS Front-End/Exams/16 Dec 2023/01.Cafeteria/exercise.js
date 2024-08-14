function solve(input) {
    let baristasCount = Number(input.shift())

    let baristasNames = [];
    let baristaTeam = {};

    for (let i = 0; i < baristasCount; i++) {
        const [baristaName, shift, coffeeTypesArray] = input.shift().split(' ');

        baristaTeam[baristaName] = {
            shift,
            coffeeTypes: coffeeTypesArray.split(',')
        }

        baristasNames.push(baristaName);
    }

    let command = input.shift();

    while (command !== 'Closed') {
        const parts = command.split(" / ");

        const actionName = parts[0];
        const baristaName = parts[1];

        switch (actionName) {
            case "Prepare":
                const shift = parts[2];
                const coffeeType = parts[3];

                if (baristaTeam[baristaName].shift === shift && baristaTeam[baristaName].coffeeTypes.includes(coffeeType)) {
                    console.log(`${baristaName} has prepared a ${coffeeType} for you!`);
                } else {
                    console.log(`${baristaName} is not available to prepare a ${coffeeType}.`);
                }

                break;

            case "Change Shift":
                const newShift = parts[2];

                baristaTeam[baristaName].shift = newShift;

                console.log(`${baristaName} has updated his shift to: ${newShift}`);

                break;

            case "Learn":

                const newCoffeeType = parts[2];

                if (baristaTeam[baristaName].coffeeTypes.includes(newCoffeeType)) {
                    console.log(`${baristaName} knows how to make ${newCoffeeType}.`);
                } else {
                    baristaTeam[baristaName].coffeeTypes.push(newCoffeeType);

                    console.log(`${baristaName} has learned a new coffee type: ${newCoffeeType}.`);
                }
                break;
        }

        command = input.shift();
    }

    baristasNames.forEach((x) => {
        console.log(`Barista: ${x}, Shift: ${baristaTeam[x].shift}, Drinks: ${baristaTeam[x].coffeeTypes.join(', ')}`);

    })
}
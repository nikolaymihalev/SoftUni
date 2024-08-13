function solve(input) {
    const END_COMMAND = "Ride Off Into Sunset";
    let posse = {};

    const chCount = Number(input.shift());

    for (let i = 0; i < chCount; i++) {
        const [chName, chHP, chBullets] = input.shift().split(' ');

        posse[chName] = {
            hp: Number(chHP),
            bullets: Number(chBullets)
        };
    }

    let command = input.shift();

    let actionFunctions = {
        FireShot(characterName, target) {
            if (posse[characterName].bullets > 0) {
                posse[characterName].bullets -= 1;

                console.log(`${characterName} has successfully hit ${target} and now has ${posse[characterName].bullets} bullets!`);

            } else {
                console.log(`${characterName} doesn't have enough bullets to shoot at ${target}!`);
            }
        },
        TakeHit(characterName, damage, attacker) {
            posse[characterName].hp -= Number(damage);

            if (posse[characterName].hp > 0) {
                console.log(`${characterName} took a hit for ${damage} HP from ${attacker} and now has ${posse[characterName].hp} HP!`);
            } else {
                delete posse[characterName];
                console.log(`${characterName} was gunned down by ${attacker}!`);
            }
        },
        Reload(characterName) {
            if (posse[characterName].bullets === 6) {
                console.log(`${characterName}'s pistol is fully loaded!`);
            } else {
                console.log(`${characterName} reloaded ${6 - posse[characterName].bullets} bullets!`);
                posse[characterName].bullets = 6;
            }
        },
        PatchUp(characterName, amount) {
            const amountValue = Number(amount);
            if (posse[characterName].hp === 100) {
                console.log(`${characterName} is in full health!`);
            } else {
                posse[characterName].hp += amountValue;

                if (posse[characterName].hp < 100) {
                    console.log(`${characterName} patched up and recovered ${amountValue} HP!`);
                } else {
                    posse[characterName].hp = 100;
                    console.log(`${characterName} patched up and recovered ${100 - amountValue} HP!`);
                }
            }
        }
    }

    while (command !== END_COMMAND) {
        const [actionName, chName, ...args] = command.split(' - ');

        actionFunctions[actionName](chName, ...args);

        command = input.shift();
    }

    Object.keys(posse).forEach(chName => {
        console.log(chName);
        console.log(`HP: ${posse[chName].hp}`);
        console.log(`Bullets: ${posse[chName].bullets}`);
    })
}

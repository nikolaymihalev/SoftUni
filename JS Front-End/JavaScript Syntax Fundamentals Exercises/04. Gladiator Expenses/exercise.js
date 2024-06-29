function solve(fights, helPrice, sworPrice, shielPrice, armPrice) {
    let helmetCount = 0;
    let swordCount = 0;
    let shieldCount = 0;
    let armorCount = 0;

    for (let i = 1; i <= fights; i++) {
        if(i % 2 === 0){
            helmetCount++;
        }
        
        if(i % 3 === 0){
            swordCount++;
        }

        if(i % 2 === 0 && i % 3 === 0){
            shieldCount++;
            if(shieldCount % 2 === 0)
                armorCount++;
        }
        
    }

    let sum = helmetCount*helPrice + swordCount*sworPrice + shieldCount*shielPrice + armorCount*armPrice;

    console.log(`Gladiator expenses: ${sum.toFixed(2)} aureus`);
}
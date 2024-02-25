function histogram(input){
    let n = Number(input[0]);

    let count1 = 0;
    let count2 = 0;
    let count3 = 0;
    let count4 = 0;
    let count5 = 0;

    for (let i = 1; i <= n; i++) {

        let currentNum = Number(input[i]);

        if(currentNum < 200){
            count1++;
        }else if (currentNum <= 399){
            count2++;
        }else if (currentNum <= 599){
            count3++;
        }else if (currentNum <= 799){
            count4++;
        }else if (currentNum >= 800){
            count5++;
        }
    }
    let percent1 = (count1/n)*100;
    let percent2 = (count2/n)*100;
    let percent3 = (count3/n)*100;
    let percent4 = (count4/n)*100;
    let percent5 = (count5/n)*100;

    console.log(`${percent1.toFixed(2)}%`);
    console.log(`${percent2.toFixed(2)}%`);
    console.log(`${percent3.toFixed(2)}%`);
    console.log(`${percent4.toFixed(2)}%`);
    console.log(`${percent5.toFixed(2)}%`);
}   
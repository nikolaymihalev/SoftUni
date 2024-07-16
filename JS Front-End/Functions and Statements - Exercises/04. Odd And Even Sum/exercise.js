function solve(number) {
    const getDigits = (x) => {
        const evenDigits = [];
        const oddDigits = [];

        let current = x;

        while(current > 0){
            const lastDigit = current%10;

            if(lastDigit % 2 === 0){
                evenDigits.push(lastDigit);
            }else{
                oddDigits.push(lastDigit);
            }

            current = parseInt(current/10);
        }

        return [
            evenDigits, 
            oddDigits
        ]
    }

    const getSumOfDigits = (array) => array.reduce((a, b)=>a+b,0);

    const [evenNumbers, oddNumbers] = getDigits(number);

    const evenSum = getSumOfDigits(evenNumbers);
    const oddSum = getSumOfDigits(oddNumbers);

    console.log(`Odd sum = ${oddSum}, Even sum = ${evenSum}`);
}
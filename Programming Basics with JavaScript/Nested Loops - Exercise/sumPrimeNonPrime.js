function sumPrimeNonPrime(input){
    let sumSimpleNums = 0;
    let sumComplexNums = 0;

    let index = 0;
    let isComplex = false;
    let currentInput = input[index];

    while (currentInput !== 'stop') {
        let number = Number(currentInput);

        if (number < 0) {
            console.log("Number is negative.");
            index++;
            currentInput = input[index];
            continue;
        }else{
            for (let i = 2; i < number; i++) {
                if (number % i === 0) {
                    isComplex = true;
                    break;
                }
            }
        }

        if (isComplex) {
            sumComplexNums += number;
            isComplex = false;
        }else{
            sumSimpleNums += number;
        }

        index++;
        currentInput = input[index];
    }

    console.log(`Sum of all prime numbers is: ${sumSimpleNums}`);
    console.log(`Sum of all non prime numbers is: ${sumComplexNums}`);
}
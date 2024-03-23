function trainTheTrainers(input){
    let judges = Number(input[0]);
    let index = 1;
    let currentRow = input[index];

    let sumGrades = 0;
    let counter = 0;

    while (currentRow !== 'Finish') {
        counter++;

        let name = currentRow;
        let tempSumGrades = 0;

        for (let i = 1; i <= judges; i++) {
            index++;
            let grade = Number(input[i]);
            tempSumGrades+=grade;
        }

        let tempAVGGrade = tempSumGrades/judges;

        sumGrades+= tempSumGrades;
        console.log(`${name} - ${tempAVGGrade.toFixed(2)}.`);

        index++;
        currentRow = input[index];
    }

    let avgGrades = sumGrades/counter;

    console.log(`Student's final assessment is ${avgGrades.toFixed(2)}.`);
}
function graduation(input){
    let name = input[0];

    let count = 1;
    let classes = 1;
    let sumGrade = 0;
    let badGrades = 0;
    let isExited = false;

    while(classes <= 12){
        let grade = Number(input[count]);
        count++;

        if(grade < 4.00)
        {    
            badGrades++;

            if(badGrades===2)
            {
                isExited = true;
                break;
            }

            continue;
        }

        sumGrade += grade;
        classes++;
    }
    if(isExited){
        console.log(`${name} has been excluded at ${classes} grade`);
        return;
    }
    console.log(`${name} graduated. Average grade: ${(sumGrade/12).toFixed(2)}`);
}
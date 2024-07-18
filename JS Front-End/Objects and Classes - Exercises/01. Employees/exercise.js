function solve(input) {
    const employees = [];

    input.forEach((name) => {
        employees.push({
            name, 
            personalNumber: name.length
        })
    });

    employees.forEach((employee)=>{
        console.log(`Name: ${employee.name} -- Personal Number: ${employee.personalNumber}`);
    })
}
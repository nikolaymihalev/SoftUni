function password(input){
    let username = input[0];
    let password = input[1];

    let count = 2;
    let tempPassword = input[count];

    while(password!==tempPassword){
        tempPassword = input[count];
        count++;
    }

    console.log(`Welcome ${username}!`);
}
function solve(password) {
    function hasCorrectLength(passLength) {
        const minLength = 6;
        const maxLength = 10;

        return passLength >= minLength && passLength <= maxLength;
    }

    function hasValidCharacters(pass) {
        const regex = /^[A-Za-z0-9]+$/g;
        return regex.test(pass);
    }

    function hasAtLeastTwoDigits(pass) {        
        return pass.match(/[0-9]/g)?.length >= 2;
    }

    let isValid = true;

    if(!hasCorrectLength(password.length)){
        console.log("Password must be between 6 and 10 characters");
        isValid = false;
    }

    if(!hasValidCharacters(password)){
        console.log("Password must consist only of letters and digits");
        isValid = false;
    }

    if(!hasAtLeastTwoDigits(password)){
        console.log("Password must have at least 2 digits");
        isValid = false;
    }

    if(isValid){
        console.log("Password is valid");
    }
}
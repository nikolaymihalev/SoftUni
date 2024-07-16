function solve(first, second) {
    function getCharCode(stringOne, stringTwo) {
        return [
            stringOne.charCodeAt(0),
            stringTwo.charCodeAt(0)
        ]        
    }

    function getCharactersInRange(firstCode, secondCode) {
        const length = Math.abs(firstCode - secondCode);
        
        const minNumber = Math.min(firstCode, secondCode) + 1; 

        return new Array(length-1).fill(0).map((_, i) => String.fromCharCode(minNumber + i));        
    }

    const [fistCharCode, secondCharCode] = getCharCode(first, second);
    console.log(getCharactersInRange(fistCharCode, secondCharCode).join(" ")); 
}

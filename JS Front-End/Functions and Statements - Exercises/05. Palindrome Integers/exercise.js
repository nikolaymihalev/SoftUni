function solve(numbers) {
    function determinateIsNumberAPalindrome(number) {
        return number === Number(number.toString().split("").reverse().join(""));
    }

    numbers.forEach((number)=>{
        console.log(determinateIsNumberAPalindrome(number));
    })
}
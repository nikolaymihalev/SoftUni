function solve(){
    console.log("Hello world!");
}

function printNumbers(){
    console.log("1");
    console.log("2");
    console.log("3");
    console.log("4");
    console.log("5");
    console.log("6");
    console.log("7");
    console.log("8");
    console.log("9");
    console.log("10");
}

let name = "Peshto";
let age = 25;
let height;

let numArr = [10, 20, 30, 40];


function sum(input){
    let firstNum = input[0];
    let secondNum = input[1];
    let thirdNum = Number(input[2]);

    console.log(firstNum+secondNum+thirdNum);
}



function parsing(input){
    console.log(parseInt(input));
}



function concatenateData(input){
    let firstName = input[0];
    let lastName = input[1];
    let age = Number(input[2]);
    let town = input[3];

    console.log(`You are ${firstName} ${lastName}, a ${age}-years old person from ${town}`);
}

concatenateData(["Ivan", "Petrov", "50", "Shumen"]);
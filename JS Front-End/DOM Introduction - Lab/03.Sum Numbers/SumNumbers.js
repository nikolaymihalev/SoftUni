function calc() {
    const num1 = document.getElementById('num1');
    const num2 = document.getElementById('num2');
    const sum = document.getElementById('sum');

    const firstNum = Number(num1.value);
    const secondNum = Number(num2.value);

    sum.value = firstNum+secondNum;
}

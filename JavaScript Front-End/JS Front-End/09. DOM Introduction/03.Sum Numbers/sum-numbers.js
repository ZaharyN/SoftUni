function calc() {
    const num1 = Number(document.querySelector('#num1').value);
    const num2 = Number(document.querySelector('#num2').value);

    let sum = num1 + num2;
    const result = document.querySelector('#sum');
    result.value = sum;
}
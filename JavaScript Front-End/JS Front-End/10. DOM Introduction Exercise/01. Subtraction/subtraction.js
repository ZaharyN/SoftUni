function subtract() {
    let firstNumber = Number(document.querySelector('#firstNumber').value);
    let secondNumber = Number(document.querySelector('#secondNumber').value);
    console.log(firstNumber);
    console.log(secondNumber);
    
    
    let result = document.querySelector('#result');
    result.textContent = firstNumber - secondNumber;
}
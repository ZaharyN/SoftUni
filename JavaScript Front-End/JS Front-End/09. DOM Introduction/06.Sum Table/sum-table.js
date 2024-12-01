function sumTable() {
    
    const rowsToSum = document.querySelectorAll('tbody tr td:nth-child(2)');

    const resultEl = document.getElementById('sum');
    let resultSum = 0;

    rowsToSum.forEach((e, i) => {
        if( i == rowsToSum.length - 1){
            return;
        }

        resultSum += Number(e.textContent);
    });

    resultEl.textContent = resultSum;
}
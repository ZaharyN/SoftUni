function solve() {
  let textToTransform = document.getElementById('text').value;
  console.log(textToTransform);

  let convention = document.getElementById('naming-convention').value;
  console.log(convention);

  let resultEl = document.getElementById('result');

  let words = [];
  let transformedArr = [];
  let transformed = '';

  switch (convention) {
    case 'Camel Case':
      words = textToTransform.split(' ');
      words.forEach((e, i) => {
        if (i == 0) {
          transformed = e.toLowerCase();
        }
        else {
          transformed = e.charAt(0).toUpperCase() + e.toLowerCase().slice(1);
        }
        transformedArr.push(transformed);
      })
      resultEl.textContent = transformedArr.join('');
      break;

    case 'Pascal Case':
      words = textToTransform.split(' ');
      words.forEach((e, i) => {
        transformed = e.charAt(0).toUpperCase() + e.toLowerCase().slice(1);
        transformedArr.push(transformed);
      })

      resultEl.textContent = transformedArr.join('');
      break;
    default:
      resultEl.textContent = 'Error!';
      break;
  }
}
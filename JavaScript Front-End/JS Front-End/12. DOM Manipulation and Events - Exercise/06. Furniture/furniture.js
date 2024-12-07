document.addEventListener('DOMContentLoaded', solve);

function solve() {
  const inputForm = document.querySelector('#input');

  inputForm.addEventListener('submit', (e) => {

    e.preventDefault();

    const textAreaEl = e.target.querySelector('textarea');
    const data = JSON.parse(textAreaEl.value);

    console.log(data);

    data.forEach(el => {

      const imageSource = el.img;
      const name = el.name;
      const price = el.price;
      const decFactor = el.decFactor;

      const tdImageCell = document.createElement('td');
      const imageEl = document.createElement('img');
      const tdProductNameCell = document.createElement('td');
      const tdPriceCell = document.createElement('td');
      const tdDecFactorCell = document.createElement('td');
      const tdCheckboxCell = document.createElement('td');
      const productCheckInputCell = document.createElement('input');

      imageEl.setAttribute('src', imageSource);
      tdImageCell.append(imageEl);

      tdProductNameCell.textContent = name;
      tdPriceCell.textContent = price;
      tdDecFactorCell.textContent = decFactor;
      productCheckInputCell.setAttribute('type', 'checkbox');
      tdCheckboxCell.append(productCheckInputCell);

      const newRow = document.createElement('tr');

      newRow.append(tdImageCell, tdProductNameCell, tdPriceCell, tdDecFactorCell, tdCheckboxCell);
      shopForm.querySelector('tbody').append(newRow);
    });
  });

  const shopForm = document.querySelector('#shop');

  shopForm.addEventListener('submit', (e) => {
    e.preventDefault();

    const outputTextArea = e.target.querySelector('textarea');

    const productList = [...document.querySelectorAll('table tbody tr:has(input:checked)')].map(p => ({
      name: p.children[1].textContent.trim(),
      price: Number(p.children[2].textContent),
      decFactor: Number(p.children[3].textContent)
    }));

    let output = `Bought furniture: ${productList.map(p => p.name).join(', ')} \n`;
    output += `Total price: ${productList.reduce((total, p) => total + p.price, 0)} \n`;
    output += `Average decoration factor: ${productList.reduce((acc, p) => acc + p.decFactor, 0) / productList.length} \n`;

      outputTextArea.value = output;
  });
}
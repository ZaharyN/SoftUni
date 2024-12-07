document.addEventListener('DOMContentLoaded', solve);

function solve() {
   const shoppingCart = document.querySelector('.shopping-cart');
   const outputArea = document.querySelector('textarea[disabled]');

   let shoppingList = {};

   shoppingCart.addEventListener('click', (e) => {
      if (e.target.nodeName != 'BUTTON') {
         return;
      }

      switch (e.target.getAttribute('class')) {
         case 'add-product':

            const product = e.target.closest('.product');
            const productName = product.querySelector('.product-title').textContent;
            const productPrice = Number(product.querySelector('.product-line-price').textContent);

            shoppingList[productName] ??= 0;
            shoppingList[productName] += productPrice;

            outputArea.textContent += `Added ${productName} for ${productPrice.toFixed(2)} to the cart.\n`;
            break;
         case 'checkout':

            const totalPrice = Object.keys(shoppingList).reduce((acc, el) => {
               return acc += shoppingList[el];
            }, 0);

            outputArea.textContent += `You bought ${Object.keys(shoppingList).join(', ')} for ${totalPrice.toFixed(2)}.`

            shoppingCart.querySelectorAll('button').forEach(b => b.disabled = true);

            break;
      }
   });
}
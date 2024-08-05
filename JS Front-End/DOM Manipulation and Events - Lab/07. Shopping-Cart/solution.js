function solve() {
   const resultElement = document.querySelector('textarea[disabled]');
   const checkoutButton = document.querySelector('button.checkout');
   const productCatalogElement = document.querySelector('.shopping-cart');

   let products = [];

   productCatalogElement.addEventListener('click', (e) => {
      if (e.target.tagName !== 'BUTTON')
         return;

      if (e.target.textContent.trim() !== 'Add')
         return;

      const productElement = e.target.closest('.product');
      const name = productElement.querySelector('.product-title').textContent;
      const price = Number(productElement.querySelector('.product-line-price').textContent);

      resultElement.value += `Added ${name} for ${price.toFixed(2)} to the cart.\n`;

      products.push({
         name,
         price
      });
   });

   checkoutButton.addEventListener('click', (e) => {
      const totalPrice = products.reduce((price, product) => price + product.price, 0);
      const productList = [...new Set(products.map(product => product.name))];

      resultElement.value += `You bought ${productList.join(', ')} for ${totalPrice.toFixed(2)}.`;

      document.querySelectorAll('button')
         .forEach(el => el.setAttribute('disabled', 'disabled'));
   })
}
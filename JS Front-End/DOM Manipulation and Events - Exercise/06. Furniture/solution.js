function solve() {
  const [generateBtn, buyBtn] = document.querySelectorAll("button");
  const [inputTextArea, outputTextArea] = document.querySelectorAll('textarea');

  const tBodyEl = document.querySelector("tbody");
  const firstTrEl = tBodyEl.querySelector("tr");

  function onGenerateBtnClickHandler() {
    const inputData = JSON.parse(inputTextArea.value);

    inputData.forEach(({ img, name, price, decFactor }) => {
      const currentTrClone = firstTrEl.cloneNode(true);

      currentTrClone.children[0].children[0].setAttribute('src', img);
      currentTrClone.children[0].innerHTML = currentTrClone.children[0].innerHTML.trim();

      currentTrClone.children[1].children[0].textContent = name;
      currentTrClone.children[1].innerHTML = currentTrClone.children[1].innerHTML.trim();

      currentTrClone.children[2].children[0].textContent = price;
      currentTrClone.children[2].innerHTML = currentTrClone.children[2].innerHTML.trim();

      currentTrClone.children[3].children[0].textContent = decFactor;
      currentTrClone.children[3].innerHTML = currentTrClone.children[3].innerHTML.trim();

      currentTrClone.children[4].children[0].disabled = false;

      tBodyEl.appendChild(currentTrClone);
    })
  }

  function onBuyBtnClickHandler() {
    const outputData =
      [...document.querySelectorAll("input[type='checkbox']"),]
        .filter((inputEl) => inputEl.checked)
        .reduce((acc, curr) => {
          const name = curr.getAttribute('name');
          const price = curr.getAttribute('price');
          const decFactor = curr.getAttribute('decFactor');

          acc.names.push(name);
          acc.totalPrice += Number(price);
          acc.totalDecFactor += Number(decFactor);

          return acc;
        }, { names: [], totalPrice: 0, totalDecFactor: 0 });


    outputTextArea.value = `Bought furniture: ${outputData.names.join(', ')}\nTotal price: ${outputData.totalPrice.toFixed(2)}\nAverage decoration factor: ${outputData.totalDecFactor / outputData.names.length}`;
  }

  generateBtn.addEventListener('click', onGenerateBtnClickHandler);
  buyBtn.addEventListener('click', onBuyBtnClickHandler);
}
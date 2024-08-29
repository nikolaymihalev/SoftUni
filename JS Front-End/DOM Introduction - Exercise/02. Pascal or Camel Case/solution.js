function solve() {
  const validNaming = ["Camel Case", "Pascal Case"];

  const textInputEl = document.querySelector('#text');
  const namingConventionEl = document.querySelector('#naming-convention');
  const resultEl = document.querySelector(".result-container #result");

  if(!validNaming.includes(namingConventionEl.value)){
    resultEl.textContent = "Error!";
    return;
  }

  const pascalCaseText = textInputEl.value.toLowerCase()
    .split(' ')
    .map((x)=> x[0].toUpperCase().concat(x.slice(1)))
    .join("");

    resultEl.textContent = namingConventionEl.value === validNaming[0] ?
      pascalCaseText[0].toLowerCase().concat(pascalCaseText.slice(1)):
      pascalCaseText;

}
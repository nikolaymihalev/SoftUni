function solve() {
   document.querySelector('#searchBtn').addEventListener('click', onClick);

   function onClick() {
      const tableRowEls = document.querySelectorAll("table.container tbody tr");

      function getMatchElements(input) {
         return [...tableRowEls].filter((rowEl) => rowEl.textContent.toLocaleLowerCase().includes(input.toLowerCase()));
      }

      function clearPrevios() {
         [...tableRowEls].forEach((rowEl) => {
            rowEl.classList.remove("select");
         })
      }

      clearPrevios();

      const searchValue = document.getElementById("searchField").value;

      const matchRows = getMatchElements(searchValue);

      matchRows.forEach((matchRow) => {
         matchRow.classList.add("select");
      });
   }
}
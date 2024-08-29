function search() {
   const townEls = document.querySelectorAll("#towns li");

   function getMatchElements(input){
      return [...townEls].filter((x)=>x.textContent.toLowerCase().includes(input.toLowerCase()));
   }

   function clearPreviosState(){
      townEls.forEach((matchEl)=>{
         matchEl.style.fontWeight = "normal";
         matchEl.style.textDecoration = "none";
      })
   }

   clearPreviosState();

   const [inputEl] = document.getElementsByTagName("input");
   const matchElements = getMatchElements(inputEl.value);

   matchElements.forEach((matchEl)=>{
      matchEl.style.fontWeight = "bold";
      matchEl.style.textDecoration = "underline";
   });

   document.querySelector("div#result").textContent = `${matchElements.length} matches found`;
}

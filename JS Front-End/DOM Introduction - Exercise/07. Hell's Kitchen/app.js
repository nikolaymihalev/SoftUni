function solve() {
   document.querySelector('#btnSend').addEventListener('click', onClick);

   const inputTextArea = document.querySelector("#inputs textarea");
   const bestRestEl = document.querySelector("#outputs #bestRestaurant p");
   const workersEl = document.querySelector("#outputs #workers p");

   function onClick () {
      const restaurants = JSON.parse(inputTextArea.value).reduce((acc, data)=>{
         const [restName, workersData] = data.split(" - ");
         const workers = workersData.split(", ").map((workerData)=>{
            const [name, salary] = workerData.split(" ");
            return {
               name, 
               salary: Number(salary)
            }
         })

         if(!acc.hasOwnProperty(restName)){
            acc[restName] = {
               workers: []
            }
         }

         acc[restName].workers.push(...workers);

         return acc;
      }, {});

      function getAvgSalary(resData) {
         return resData.workers.reduce((a,b)=> a+b.salary,0)/resData.workerData.length;
      }

      const [bestRest] = Object.keys(restaurants).sort((a,b)=>getAvgSalary(restaurants[b])-getAvgSalary(restaurants[a]));

      const bestWorkers = restaurants[bestRest].workers.slice().sort((a,b)=>b.salary-a.salary);

      bestRestEl.textContent = `Name: ${bestRest} Average Salary: ${getAvgSalary(restaurants[bestRest]).toFixed(2)} Best Salary: ${bestWorkers[0].salary.toFixed(2)}`;

      workersEl.textContent = bestWorkers.map((x)=>`Name: ${x.name} With Salary: ${x.salary}`).join(" ")
   }
}
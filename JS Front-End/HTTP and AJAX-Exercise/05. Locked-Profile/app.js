function lockedProfile() {
    const BASE_URL = "http://localhost:3030/jsonstore/advanced/profiles";    
    
    const structureProfileEl = document.querySelector(".profile");
    const mainContentEl = document.querySelector("#main");
  
    async function customFetch(url, options) {
      return fetch(url, options).then((res) => {
        if (!res.ok) {
          throw Error(res.message);
        }
  
        return res.json();
      })
        .then((data) => ({ data, error: null }))
        .catch((error) => ({ data: null, error }));
    }

    function showAdditionalData(profileEL, _id) {
        const {checked} = profileEL.querySelector(`input[name="${_id}"]`);

        if(checked)
            return;

        const btnText = profileEL.querySelector('button').textContent;

        if(btnText === "Show more"){
            profileEL.querySelector(".profile > div").style.display = "block";
            profileEL.querySelector('button').textContent = "Hide it";
        }else {
            profileEL.querySelector(".profile > div").style.display = "none";
            profileEL.querySelector('button').textContent = "Show more";
        }
    }

    function appendProfiles(profiles) {

        profiles.forEach(({username, email, age, _id})=>{
            const profileClone = structureProfileEl.cloneNode(true);

            profileClone.querySelector("input[name='user1Username']").value = username;
            profileClone.querySelector("input[name='user1Email']").value = email;
            profileClone.querySelector("input[name='user1Age']").value = age;

            const [lockRadioEl, unlockRadioEl] = profileClone.querySelectorAll("input[name='user1Locked']");

            lockRadioEl.setAttribute("name", _id);
            unlockRadioEl.setAttribute("name", _id);

            profileClone.querySelector('button').addEventListener('click', ()=> showAdditionalData(profileClone,_id));

            profileClone.querySelector(".profile > div").style.display = "none";

            mainContentEl.appendChild(profileClone);
        })

        structureProfileEl.remove();
    }

    async function getAllProfiles() {
        const {data, error} = await customFetch(BASE_URL);

        if(error)
            return;

        appendProfiles(Object.values(data));
    }

    getAllProfiles();
}
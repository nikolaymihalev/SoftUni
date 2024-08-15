window.addEventListener("load", solve);

function solve() {
  let formElement = document.querySelector("form");
  let taskTitleInput = document.getElementById("player");
  let taskCategoryInput = document.getElementById("score");
  let taskContentInput = document.getElementById("round");
  let clearBtn = document.querySelector(".clear")
  let publishBtn = document.getElementById("add-btn");
  publishBtn.addEventListener("click", publish);

  function publish() {
    if (
      taskTitleInput.value == "" ||
      taskCategoryInput.value == "" ||
      taskContentInput.value == ""
    ) {
      return;
    }
    let reviewList = document.getElementById("sure-list");
    let publishedList = document.getElementById("scoreboard-list");

    let taskLiElement = document.createElement("li");
    taskLiElement.classList.add("dart-item");

    let taskArticleElement = document.createElement("article");

    let titleHeadingElement = document.createElement("p");
    titleHeadingElement.textContent = taskTitleInput.value;
    let taskTitle = taskTitleInput.value;
    let categoryPElement = document.createElement("p");
    categoryPElement.textContent = `Score: ${taskCategoryInput.value}`;
    let taskCategory = taskCategoryInput.value;

    let contentPElement = document.createElement("p");
    contentPElement.textContent = `Round: ${taskContentInput.value}`;
    let taskContent = taskContentInput.value;

    taskArticleElement.appendChild(titleHeadingElement);
    taskArticleElement.appendChild(categoryPElement);
    taskArticleElement.appendChild(contentPElement);

    let editBtn = document.createElement("button");
    editBtn.classList.add("btn");
    editBtn.classList.add("edit");
    editBtn.textContent = "edit";
    editBtn.addEventListener("click", edit);

    let postBtn = document.createElement("button");
    postBtn.classList.add("btn");
    postBtn.classList.add("ok");
    postBtn.textContent = "ok";
    postBtn.addEventListener("click", post);

    taskLiElement.appendChild(taskArticleElement);
    taskLiElement.appendChild(editBtn);
    taskLiElement.appendChild(postBtn);

    reviewList.appendChild(taskLiElement);
    formElement.reset();
    publishBtn.disabled = true;

    function edit() {
      taskTitleInput.value = taskTitle;
      taskCategoryInput.value = taskCategory;
      taskContentInput.value = taskContent;

      reviewList.removeChild(taskLiElement);
      publishBtn.disabled = false;
    }

    function post() {
      reviewList.removeChild(taskLiElement);
      taskLiElement.removeChild(postBtn);
      taskLiElement.removeChild(editBtn);
      publishedList.appendChild(taskLiElement);
      publishBtn.disabled = false;

      clearBtn.addEventListener("click", onClear)

      function onClear() {
        location.reload();
      }
    }
  }
}

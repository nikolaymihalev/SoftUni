window.addEventListener("load", solve);

function solve() {
  let formElement = document.querySelector("form");
  let taskTitleInput = document.getElementById("student");
  let taskCategoryInput = document.getElementById("university");
  let taskContentInput = document.getElementById("score");

  let publishBtn = document.getElementById("next-btn");
  publishBtn.addEventListener("click", publish);

  function publish() {
    if (
      taskTitleInput.value == "" ||
      taskCategoryInput.value == "" ||
      taskContentInput.value == ""
    ) {
      return;
    }
    let reviewList = document.getElementById("preview-list");
    let publishedList = document.getElementById("candidates-list");

    let taskLiElement = document.createElement("li");
    taskLiElement.classList.add("application");

    let taskArticleElement = document.createElement("article");

    let titleHeadingElement = document.createElement("h4");
    titleHeadingElement.textContent = taskTitleInput.value;
    let taskTitle = taskTitleInput.value;
    let categoryPElement = document.createElement("p");
    categoryPElement.textContent = `University:${" "}${taskCategoryInput.value}`;
    let taskCategory = taskCategoryInput.value;

    let contentPElement = document.createElement("p");
    contentPElement.textContent = `Score:${" "}${taskContentInput.value}`;
    let taskContent = taskContentInput.value;

    taskArticleElement.appendChild(titleHeadingElement);
    taskArticleElement.appendChild(categoryPElement);
    taskArticleElement.appendChild(contentPElement);

    let editBtn = document.createElement("button");
    editBtn.classList.add("action-btn");
    editBtn.classList.add("edit");
    editBtn.textContent = "edit";
    editBtn.addEventListener("click", edit);

    let postBtn = document.createElement("button");
    postBtn.classList.add("action-btn");
    postBtn.classList.add("apply");
    postBtn.textContent = "apply";
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
    }
  }
}

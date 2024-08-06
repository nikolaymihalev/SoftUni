function solve() {
  const sectionElements = document.querySelectorAll("section");

  const correctAnswers = ["onclick", "JSON.stringify()", "A programming API for HTML and XML documents"];
  const userAnswers = [];

  function showUserResult(answers, rightAnswers) {
    correctAnswersCount = answers.reduce((totalCount, currentAnswers, index) =>
      currentAnswers === rightAnswers[index]
        ? (totalCount += 1)
        : totalCount,
      0);

    const headingResultEl = document.querySelector("ul#results h1");

    headingResultEl.textContent = correctAnswersCount === rightAnswers.length
      ? `You are recognized as top Javascript fan!`
      : `You have ${correctAnswersCount} right answers`;

    headingResultEl.parentElement.parentElement.style.display = "block";
  }

  function onClickHandler(event) {
    const sectionToHide = sectionElements[userAnswers.length];
    const sectionToShow = sectionElements[userAnswers.length + 1];

    if (!!sectionToHide) {
      sectionToHide.style.display = 'none';
    }

    if (!!sectionToShow) {
      sectionToShow.style.display = 'block';
    }


    userAnswers.push(event.target.textContent.trim());

    if (userAnswers.length === correctAnswers.length) {
      showUserResult(userAnswers, correctAnswers);
    }
  }

  document.querySelectorAll(".answer-wrap").forEach((wrapEl) => wrapEl.addEventListener('click', onClickHandler))
}

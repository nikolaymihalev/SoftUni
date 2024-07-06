function solve(specialWords, template) {
    const words = specialWords.split(", ");
    let finatSentence = template;
    words.forEach((word) => {
        const searchValue = "*".repeat(word.length);
        finatSentence = finatSentence.replace(searchValue, word);
    });

    console.log(finatSentence);
}
function solve(text, word) {
    let replaceWord = '';
    for (let i = 0; i < word.length; i++) {
        replaceWord+='*';
        
    }
    console.log(text.replaceAll(word, replaceWord));
}
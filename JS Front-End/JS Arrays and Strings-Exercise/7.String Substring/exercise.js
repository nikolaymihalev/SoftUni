function solve(specialWord, text) {
    const regex = new RegExp(`\\b${specialWord}\\b`, "i");
    const result = text.match(regex);
    if(result !== null){
        console.log(specialWord);
    }else {
        console.log(`${specialWord} not found!`);
    }
}
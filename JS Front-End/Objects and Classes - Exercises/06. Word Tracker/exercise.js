function solve(input) {
    const [specialWords, ...restOfWords] = input;

    const words = specialWords.split(' ').reduce((acc, word)=>{
        acc[word] = 0;
        return acc;    
    }, {});

    restOfWords.forEach((word)=>{
        if(words.hasOwnProperty(word)){
            words[word]+=1
        }
    })

    Object.keys(words).sort((a,b)=>words[b]-words[a])
        .forEach((word)=>console.log(`${word} - ${words[word]}`));
}
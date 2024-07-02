function solve(text, search) {
    let count = 0;
    let array = text.split(' ');

    for (const word of array) {
        if(word===search)
            count++;
    }

    console.log(count);
}
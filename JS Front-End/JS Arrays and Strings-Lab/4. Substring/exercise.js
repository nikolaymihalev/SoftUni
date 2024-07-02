function solve(word, start, end) {
    let result = '';

    for (let i = start; i <= end; i++) {
        result+=word[i];
    }

    console.log(result);
}
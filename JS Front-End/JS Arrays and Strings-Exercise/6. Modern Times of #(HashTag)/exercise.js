function solve(input) {
    const regex = /#[A-Za-z]+/gm;
    const matches = input.matchAll(regex);
    for (const match of matches) {
        console.log(match[0].substring(1));
    }
}
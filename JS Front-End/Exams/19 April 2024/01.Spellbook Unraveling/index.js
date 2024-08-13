function solve(input) {
    const END_COMMAND = "End";

    let word = input.shift();

    let commad = input.shift();

    const actionCommand = {
        RemoveEven() {
            let newWord = '';

            for (let i = 0; i < word.length; i += 2) {
                newWord += word[i];
            }

            word = newWord;
            console.log(word);

        },
        TakePart(startIndex, endIndex) {
            let newWord = '';

            for (let i = Number(startIndex); i < Number(endIndex); i++) {
                newWord += word[i];
            }

            word = newWord;
            console.log(word);
        },
        Reverse(substring) {
            let newPart = substring.split('').reverse().join('');

            if (word.includes(substring)) {
                word = word.replace(substring, '');
                word += newPart;

                console.log(word);
            } else {
                console.log('Error');
            }
        }
    }

    while (commad !== END_COMMAND) {
        const [action, ...args] = commad.split('!');

        actionCommand[action](...args);

        commad = input.shift();
    }

    console.log(`The concealed spell is: ${word}`);

}

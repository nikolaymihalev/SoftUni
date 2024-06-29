function solve(sentence) {
    sentence = sentence.replace(',','');
    sentence = sentence.replace('.','');
    sentence = sentence.replace('?','');
    sentence = sentence.replace('!','');

    let array = sentence.toUpperCase().split(' ');
    let result = '';

    for (let i = 0; i < array.length; i++) {
        result += array[i];
        if(i < array.length-1)
            result += ", ";
    }

    console.log(result);
}
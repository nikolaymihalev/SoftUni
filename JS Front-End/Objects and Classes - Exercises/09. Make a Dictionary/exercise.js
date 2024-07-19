function solve(input) {
    const dictionary = {};
    for (const json of input) {
        const object = JSON.parse(json);

        for (const key of Object.keys(object)) {    
            dictionary[key] = object[key];
        }
    }

    const sortedKeys = Object.keys(dictionary)
        .sort((a,b)=>a.localeCompare(b));

    for (const term of sortedKeys) {
        console.log(`Term: ${term} => Definition: ${dictionary[term]}`);
    }
}
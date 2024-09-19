function solve(input) {
    let phonebook = {};

    for (const entry of input) {
        let [name, phone] = entry.split(' ');

        phonebook[name] = phone;
    }

    for (const name in phonebook) {
        console.log(`${name} -> ${phonebook[name]}`);
    }
}

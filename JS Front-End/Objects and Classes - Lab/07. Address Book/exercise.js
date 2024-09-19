function solve(input) {
    let addressBook = {};

    input.forEach(entry => {
        const [name, address] = entry.split(':');
        addressBook[name] = address;
    });

    Object.entries(addressBook)
    .sort((a,b) => a[0].localeCompare(b[0]))
    .forEach(([name, address]) => 
        console.log(`${name} -> ${address}`
            
        ));
   
}
function solve(json) {
    let data = JSON.parse(json);

    Object.keys(data).forEach(key=>{
        console.log(`${key}: ${data[key]}`);
    })
}
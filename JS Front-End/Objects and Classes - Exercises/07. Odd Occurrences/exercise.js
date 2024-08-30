function solve(input) {
    const occurenceses = input.split(' ').reduce((acc, curr)=>{
        const key = curr.toLowerCase();

        if(!acc.hasOwnProperty(key)){
            acc[key] = 0;
        }

        acc[key] += 1;

        return acc;
    }, {});

    console.log(Object.keys(occurenceses).filter((key)=>occurenceses[key]%2!==0).join(' '));
}
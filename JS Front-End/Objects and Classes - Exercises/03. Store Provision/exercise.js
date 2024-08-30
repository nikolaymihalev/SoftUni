function solve(stockProducts, orderedProducts){
    function generateObject(inputArray) {
        return new Array(inputArray.length / 2).fill({}).reduce((acc, curr, i)=>{
            const product = inputArray[i+i];
            const quantity = Number(inputArray[i+i+1]);
    
            if(!curr[product]){
                curr[product] = 0;
            }
            
            curr[product] += quantity;
            return Object.assign(acc, curr);
        }, {});
    }

    const inStock = generateObject(stockProducts);
    const inOrder = generateObject(orderedProducts);

    const allProducts = {
        ...inStock
    };

    Object.keys(inOrder).forEach((x)=>{
        if(!allProducts[x]){
            allProducts[x] = 0
        }

        allProducts[x] += inOrder[x];
    })

    Object.entries(allProducts).forEach(([key, value])=>{
        console.log(`${key} -> ${value}`);
    })
}
function solve(arr, step) {
    const newArr = [];

    arr.forEach((element, index) => {
        if(index%step===0){
            newArr.push(element)
        }
    })
    return newArr;
}
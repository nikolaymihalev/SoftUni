function solve(arr, rot) {
    const cutOff = rot % arr.length;

    const leftSide = arr.slice(0, cutOff);
    const rightSide = arr.slice(cutOff);

    const result = rightSide.concat(leftSide);

    console.log(result.join(' '));
}
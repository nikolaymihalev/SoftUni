function solve(ch1, ch2, ch3) {
    let normal = `${ch1} ${ch2} ${ch3}`;
    let array = normal.split(' ');
    console.log(array.reverse().join(' '));
}
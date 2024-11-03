function solve(...chars) {
    let reversed = chars.reverse();
    console.log(reversed.join(" ", reversed));
}

solve('A', 'B', 'C');
solve('1', 'L', '&');
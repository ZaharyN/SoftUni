function solve(arr) {
    arr.forEach((e) => {
        isPalindrome(e);
    });

    function isPalindrome(element) {
        let arrEl = element.toString().split('');
        let copy = arrEl.toReversed();
        
        for (let i = 0; i < arrEl.length; i++) {
            if (arrEl[i] != copy[i]) {
                console.log("false");
                return;
            }
        }
        console.log("true");
    }
}

solve([123, 323, 421, 121]);
// false true false true

solve([32, 2, 232, 1010]);
// false true true false

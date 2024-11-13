function solve(text) {
    //console.log(text.match(/[A-Z][a-z]+/g).join(', '));
    console.log(text.split(/(?=[A-Z])/).join(', '));
}

solve('SplitMeIfYouCanHaHaYouCantOrYouCan');
//Split, Me, If, You, Can, Ha, Ha, You, Cant, Or, You, Can

solve('HoldTheDoor');
// Hold, The, Door

solve('ThisIsSoAnnoyingToDo');
// This, Is, So, Annoying, To, Do

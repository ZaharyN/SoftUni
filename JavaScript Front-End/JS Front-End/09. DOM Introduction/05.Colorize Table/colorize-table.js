function colorize() {
    let allRrows = document.querySelectorAll('tbody tr');

    let i = 1;
    for (const row of allRrows) {
        if (i % 2 == 0) {
            row.style.background = 'teal';
        }
        i++;
    }
}
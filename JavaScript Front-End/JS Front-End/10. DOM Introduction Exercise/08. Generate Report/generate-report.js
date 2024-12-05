function solve() {
    const headers = document.querySelectorAll('thead tr th');
    const allTableRows = document.querySelectorAll('tbody tr');

    const outputEl = document.getElementById('output');
    
    const checkedHeaders = [...headers]
    .map((h, i) => ({el: h.children[0], name: h.children[0].getAttribute('name'), index: i}))
    .filter(x => x.el.checked);
    
    console.log(checkedHeaders);
    
    const outputData = [...allTableRows]
        .map( row => {
            return checkedHeaders.reduce( (acc, checkedHeader) => {
                acc[checkedHeader.name] = row.children[checkedHeader.index].textContent;
                return acc;
            }, {});
        })

    outputEl.value = JSON.stringify(outputData);
}
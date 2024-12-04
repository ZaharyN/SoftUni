function solve() {
   let tableRows = document.querySelectorAll('tbody tr');
   let searchText = document.getElementById('searchField').value;

   if(searchText == ''){
      return;
   }

   for (const row of tableRows) {

      row.classList.remove('select');
      for (const data of row.children) {
         
         if(data.textContent.toLowerCase().includes(searchText.toLowerCase())){
            row.classList.add('select');
         }
      }
   }
}
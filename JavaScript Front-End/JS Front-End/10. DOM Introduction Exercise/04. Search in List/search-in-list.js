function solve() {
   let towns = document.getElementsByTagName('li');
   let searchText = document.getElementById('searchText').value;
   let textResult = document.getElementById('result');
   let matches = [];


   for (const town of towns) {

      let parsed = town.textContent;
      if(parsed.includes(searchText.toString())){
         matches.push(parsed);
         town.style.fontWeight = 'bold';
         town.style.textDecoration = 'underline';
      }
   }

   textResult.textContent = `${matches.length} matches found`;
}
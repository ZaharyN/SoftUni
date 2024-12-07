document.addEventListener('DOMContentLoaded', solve);

function solve() {
   const formEl = document.querySelector('#task-input');
   const text = document.querySelector('input[type="text"]').value;
   const generateArea = document.querySelector('#content');
   
   formEl.addEventListener('submit', (e) => {
      e.preventDefault();

      text.split(', ').forEach(w => {
         const divEl = document.createElement('div');
         const pEl = document.createElement('p');
         pEl.textContent = w;
         pEl.style.display = 'none';
         divEl.appendChild(pEl);
         divEl.addEventListener('click', (e) => {
            e.target.querySelector('p').style.display = 'block';
         });
         generateArea.appendChild(divEl);
      });
   })
}
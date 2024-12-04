function solve() {
  let text = document.querySelector('#input');
  let output = document.querySelector('#output');

  const sentencesPerP = 3;
  let sentences = text.value.split('. ');
  const pTagsCount = Math.ceil(sentences.length / sentencesPerP);
  let pText = '';

  for (let i = 0; i < pTagsCount; i++) {
    
    const startIndex = i * sentencesPerP;

    pText += '<p>';
    pText += sentences.slice(startIndex, (startIndex + sentencesPerP)).join('. ');
    pText += '</p>';

  }
  output.innerHTML = pText;
}

// 0, 1, 2,
// 3, 4, 5
// 6, 7,
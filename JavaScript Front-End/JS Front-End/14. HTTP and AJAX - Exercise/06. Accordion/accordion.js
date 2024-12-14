function solution() {

    const getUrl = 'http://localhost:3030/jsonstore/advanced/articles/list';
    const detailsUrl = 'http://localhost:3030/jsonstore/advanced/articles/details/';

    const mainEl = document.querySelector('#main');

    getArticles();

    function getArticles() {

        mainEl.innerHTML = '';

        fetch(getUrl)
            .then(result => result.json())
            .then(articles => {

                console.log(articles);
                Object.values(articles).forEach(article => {

                    mainEl.appendChild(createArticle(article.title, article._id));

                });
            })
            .catch(err => console.log(err));
    };

    function createArticle(title, id) {

        const currentArticle = document.createElement('div');
        currentArticle.className = 'accordion';

        currentArticle.innerHTML =
            `<div class="head">
                    <span>${title}</span>
                    <button class="button" id="${id}">More</button>
                </div>
                <div class="extra">
                    <p></p>
                </div>`;

        currentArticle.querySelector('.button').addEventListener('click', toggleAccordion);

        return currentArticle;
    }

    function toggleAccordion(e) {

        e.preventDefault();

        console.log(e.currentTarget.id);
        const currentArticle = e.currentTarget.closest('.accordion');
        const detailsParagraph = currentArticle.querySelector('.extra p');
        console.log(detailsParagraph);
        
        const hiddenDiv = currentArticle.querySelector('.extra');

        if (e.currentTarget.textContent == 'More') {

            const articleContent = getFurtherContent(e.currentTarget.id);
            console.log(articleContent);
            
            detailsParagraph.textContent = articleContent;
            e.currentTarget.textContent = 'Less';
            hiddenDiv.style.display = 'block';
        }
        else if (e.currentTarget.textContent == 'Less') {
            e.currentTarget.textContent = 'More';
            hiddenDiv.style.display = 'none';
        }
    }

    function getFurtherContent(id) {

        fetch(detailsUrl + id)
            .then(result => result.json())
            .then(info => {
                
                const pInfo = info.content;
                return pInfo;
            })
            .catch(err => console.log(err));
    }
}
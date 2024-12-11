function attachEvents() {
    const getPostUrl = "http://localhost:3030/jsonstore/blog/posts";
    const getCommentsUrl = "http://localhost:3030/jsonstore/blog/comments";

    const selectContainer = document.querySelector('#posts');
    const headingEl = document.querySelector('#post-title');
    const bodyEl = document.querySelector('#post-body');
    const commentList = document.querySelector('#post-comments');

    document.querySelector('#btnLoadPosts').addEventListener('click', (e) => {

        selectContainer.innerHTML = '';

        fetch(getPostUrl)
            .then(response => response.json())
            .then(posts => {

                Object.keys(posts).forEach(p => {

                    const optionEl = document.createElement('option');

                    Object.assign(optionEl.dataset, posts[p]);

                    optionEl.textContent = posts[p].title;
                    selectContainer.append(optionEl);
                });
            })
            .catch(err => console.log(err));
    })

    document.querySelector('#btnViewPost').addEventListener('click', (e) => {

        commentList.innerHTML = '';

        fetch(getCommentsUrl)
            .then(result => result.json())
            .then(comments => {

                const currentOption = selectContainer.querySelector('option:checked');

                headingEl.textContent = currentOption.dataset.title;
                bodyEl.textContent = currentOption.dataset.body;

                Object.values(comments).forEach(comment => {

                    if (comment.postId == currentOption.dataset.id) {
 
                        const liEl = document.createElement('li');
                        liEl.textContent = comment.text;
                        commentList.appendChild(liEl);
                    }
                });
            })
            .catch(err => console.log(err));
    })
}

attachEvents();
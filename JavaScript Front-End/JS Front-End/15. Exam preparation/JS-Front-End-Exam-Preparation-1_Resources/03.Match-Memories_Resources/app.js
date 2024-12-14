document.addEventListener('DOMContentLoaded', init);
const getUrl = 'http://localhost:3030/jsonstore/matches/';

const loadMatchesBtn = document.querySelector('#load-matches');
const addMatchBtn = document.querySelector('#add-match');
const editMatchBtn = document.querySelector('#edit-match');
const matchContainer = document.querySelector('#list');

const hostInput = document.querySelector('input#host');
const scoreInput = document.querySelector('input#score');
const guestInput = document.querySelector('input#guest');

function init() {

    loadMatches();
}

function loadMatches() {

    matchContainer.innerHTML = '';

    fetch(getUrl)
        .then(result => result.json())
        .then(matches => {

            console.log(matches);

            Object.values(matches).forEach(v => {

                const host = v.host;
                const score = v.score;
                const guest = v.guest;
                const _id = v._id;

                const match = createElement('li', { className: 'match', dataset: { host, score, guest, _id } }, matchContainer);
                const divEl = createElement('div', { className: 'info' }, match);
                createElement('p', { textContent: v.host }, divEl);
                createElement('p', { textContent: v.score }, divEl);
                createElement('p', { textContent: v.guest }, divEl);
                const buttonDiv = createElement('div', { className: 'btn-wrapper' }, match);
                createElement('button', { className: 'change-btn', onclick: changeMatchHandler, textContent: 'Change' }, buttonDiv);
                createElement('button', { className: 'delete-btn', onclick: deleteMatchHandler, textContent: 'Delete' }, buttonDiv);
            });
            editMatchBtn.disabled = true;
        })
        .catch(err => console.log(err));
}

function changeMatchHandler(e) {

    const matchLi = e.target.closest('li');
    const matchInfo = matchLi.querySelectorAll('p');

    hostInput.value = matchInfo[0].textContent;
    scoreInput.value = matchInfo[1].textContent;
    guestInput.value = matchInfo[2].textContent;

    editMatchBtn.disabled = false;
    addMatchBtn.disabled = true;
}

// editMatchBtn.addEventListener('click', (e) => {

//     if (!hostInput.value
//         || !scoreInput.value
//         || !guestInput.value
//     ) { return; }

//     const body = { host: hostInput.value, score: scoreInput.value, guest: guestInput.value, _id: matchId };

//     fetch(getUrl + matchId, {
//         method: "PUT",
//         body: JSON.stringify(body)
//     })
//         .then(result => result.json())
//         .then(data => {

//             editMatchBtn.disabled = true;
//             addMatchBtn.disabled = false;
//             hostInput.value = '';
//             scoreInput.value = '';
//             guestInput.value = '';
//             loadMatches();

//         })
//         .catch(err => console.log(err));
// });

function deleteMatchHandler(e) {

    const matchLi = e.target.closest('li');

}
//addMatchBtn.addEventListener('click', addMatch);


// function addMatch(e) {

//     if (!hostInput.value
//         || !scoreInput.value
//         || !guestInput.value
//     ) { return; }

//     const body = { host: hostInput.value, score: scoreInput.value, guest: guestInput.value };

//     fetch(getUrl, {
//         method: "POST",
//         body: JSON.stringify(body)
//     })
//         .then(result => result.json())
//         .then(data => {

//             hostInput.value = '';
//             scoreInput.value = '';
//             guestInput.value = '';
//             loadMatches();
//         })
//         .catch(err => console.log(err));
// }

// function changeButtonHandler(e) {

//     e.preventDefault();

//     const matchLi = e.target.closest('li');
//     const matchInfo = matchLi.querySelectorAll('p');
//     const matchId = matchLi.dataset._id;

//     hostInput.value = matchInfo[0].textContent;
//     scoreInput.value = matchInfo[1].textContent;
//     guestInput.value = matchInfo[2].textContent;
//     editMatchBtn.disabled = false;
//     addMatchBtn.disabled = true;

//     editMatchBtn.addEventListener('click', (e) => {

//         e.preventDefault();

//         if (!hostInput.value
//             || !scoreInput.value
//             || !guestInput.value
//         ) { return; }

//         const body = { host: hostInput.value, score: scoreInput.value, guest: guestInput.value, _id: matchId };

//         fetch(getUrl + matchId, {
//             method: "PUT",
//             body: JSON.stringify(body)
//         })
//             .then(result => result.json())
//             .then(data => {

//                 editMatchBtn.disabled = true;
//                 addMatchBtn.disabled = false;
//                 hostInput.value = '';
//                 scoreInput.value = '';
//                 guestInput.value = '';
//                 loadMatches();

//             })
//             .catch(err => console.log(err));
//     });
// }


// function deleteButtonHandler(e) {

//     e.preventDefault();
//     const matchLi = e.currentTarget.closest('li');
//     const matchId = matchLi.dataset._id;

//     fetch(getUrl + matchId, {
//         method: "DELETE",
//     })
//         .then(result => result.json())
//         .then(data => {

//             loadMatches();

//         })
//         .catch(err => console.log(err));
// }

function createElement(tag, attributes, parentContainer) {

    const element = document.createElement(tag);


    Object.keys(attributes).forEach(key => {
        if (typeof attributes[key] === 'object') {
            Object.assign(element[key], attributes[key]);
        } else {
            element[key] = attributes[key];
        }
    })

    if (parentContainer) parentContainer.appendChild(element);

    return element;
}



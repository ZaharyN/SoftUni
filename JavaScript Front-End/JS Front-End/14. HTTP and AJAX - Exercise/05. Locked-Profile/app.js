function lockedProfile() {

    const getUrl = 'http://localhost:3030/jsonstore/advanced/profiles';

    const profileEl = document.querySelector('.profile');
    const nameInput = document.querySelector('input[name="user1Username"]');
    const hiddenDiv = document.querySelector('.user1Username');
    const userEmail = document.querySelector('input[name="user1Email"]');
    const userAge = document.querySelector('input[name="user1Age"]');

    loadUsers();
    function loadUsers() {

        document.querySelector('main').innerHTML = '';

        fetch(getUrl)
            .then(result => result.json())
            .then(users => {

                console.log(users);
                Object.values(users).forEach((user, i) => {

                    document.querySelector('main')
                        .appendChild(createUser(user.username, user.email, user.age));

                });
            })
            .catch(err => console.log(err));
    }

    function createUser(username, email, age) {

        const userDiv = document.createElement('div');
        userDiv.className = 'profile';

        userDiv.innerHTML =
            `<img src="./iconProfile2.png" class="userIcon" />
				<label>Lock</label>
				<input type="radio" name="user1Locked" value="lock" checked>
				<label>Unlock</label>
				<input type="radio" name="user1Locked" value="unlock"><br>
				<hr>
				<label>Username</label>
                <input type="text" name="user1Username" value="${username}" disabled readonly />
                <div class="user1Username">
					<hr>
					<label>Email:</label>
					<input type="email" name="user1Email" value="${email}" disabled readonly />
					<label>Age:</label>
					<input type="number" name="user1Age" value="${age}" disabled readonly />
				</div>

				<button>Show more</button>`

        userDiv.querySelector('button').addEventListener('click', toggleHiddenDiv);
        userDiv.querySelector('.user1Username').style.display = 'none';
        return userDiv;
    }

    function toggleHiddenDiv(e) {

        e.preventDefault();

        const currentDiv = e.currentTarget.closest('.profile');
        const lockButton = currentDiv.querySelector('input[value="lock"]');
        const unlockButton = currentDiv.querySelector('input[value="unlock"]');
        const hiddenDiv = currentDiv.querySelector('.user1Username');
        const buttonShowMore = currentDiv.querySelector('button');

        if (buttonShowMore.textContent == 'Hide it') {
            if (lockButton.checked) {
                buttonShowMore.disabled = true;
                return;
            }
            if (unlockButton.checked) {
                hiddenDiv.style.display = 'none';
                buttonShowMore.textContent = 'Show more';
            }
        }
        else if (buttonShowMore.textContent == 'Show more') {
            if (lockButton.checked) {
                return;
            }
            if (unlockButton.checked) {
                hiddenDiv.style.display = 'block';
                buttonShowMore.textContent = 'Hide it';
            }
        }
    }
}
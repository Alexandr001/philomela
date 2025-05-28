// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const form = document.getElementById('login-form');
const button = document.getElementById('refr-button');

let refreshFunc = () => {
    fetch('api/Authentication/refresh', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify({login: "admin"})
    })
        .then(r => {
            if (r.ok === false) {
                alert('Не удалось обновить токены.')
                window.location.replace("/")
            }
        })
        .catch(err => {
            alert('Не удалось выполнить авторизацию!');
            window.location.replace("/");
        })
}

const tokenRefreshInterval = setInterval(refreshFunc, 1000 * 60);
// Останавливаем интервал при выходе из системы

window.addEventListener('beforeunload', () => {
    clearInterval(tokenRefreshInterval);
});

form.addEventListener('submit', e => {
    e.preventDefault();

    let object = {};
    new FormData(e.currentTarget).forEach((value, key) => {
        object[key] = value;
    });


    fetch('api/Authentication/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(object)
    })
        .then(r => {
            if (r.ok) {
                window.location.replace('/privacy')
            } else {
                alert('Неверный логин или пароль!')
            }
        })
        .catch(err => {
            alert('Не удалось выполнить авторизацию!')
        })
});

button.addEventListener("click", e => {
    e.preventDefault();
    refreshFunc();
})

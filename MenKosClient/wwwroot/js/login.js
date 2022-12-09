const loginFormEl = document.getElementById('loginForm')
loginFormEl.addEventListener('submit', login)

function login(event) {
    event.preventDefault()

    const formData = new FormData(loginFormEl)

    const data = {
        Email: formData.get('email'),
        Password: formData.get('password')
    }

    console.log(JSON.stringify(data))


    $.ajax({
        url: "https://localhost:7095/api/user/login",
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: (res) => {
            console.log(res)


            let token = res.Token

            window.sessionStorage.setItem('token', token)


            parsedToken = parseJwt(token)

            console.log(parsedToken)


            console.log(token)
            if (parsedToken.Role == 'admin') {
                window.location = "/admin"
            }

            if (parsedToken.Role == 'occupant') {
                window.location = "/user"
            }


        },
        error: (err) => {
            console.log(err)
        }

    })

}


function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}
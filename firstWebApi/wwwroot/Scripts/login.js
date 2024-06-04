const showRegisterForm = () => {
    const register = document.getElementById("register")
    register.style.visibility = "initial"
}

const login = async () => {
    try {
        const userName = document.getElementById("userName").value
        const password = document.getElementById("password").value

        const user = {userName, password}

        const res = await fetch('/api/users/login',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)
            });
        if (!res.ok)
            throw new Error("Error to connect the srver");
        if (res.status == 204)
            alert("Name or Password worng!!!!");
        else {
            const user = await res.json();
            sessionStorage.setItem("user", JSON.stringify(user))
            console.log(user)
            window.location.href = "./home.html"
        }
    }
    catch (ex) {
        alert(ex.message)
    }
}

const register = async () => {
    try {
        const userName = document.getElementById("registerUserName").value
        const password = document.getElementById("pass").value
        const name = document.getElementById("name").value
        const lastName = document.getElementById("lastName").value
        const progress = document.getElementById("progress").value

        if (progress < 3)
            throw new Error("Change the password")

        const user = { userName, password, name, lastName }

        const res = await fetch('/api/users',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)
            });

        if (!res.ok)
            throw Error("Error adding user to server")
        const created = await res.json();
        alert(`${created.id} created`);
        console.log(user)//save in form
    }
    catch (ex) {
        alert(ex.message)
    }

}
const checkPassword = async () => {
    try {
        const password = document.getElementById("pass").value
        const progress = document.getElementById("progress")
        const res = await fetch('/api/users/Pwd',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(password)
            });
        console.log(res)
        const secure = await res.json();
        progress.value = secure;
    }
    catch (ex) {
        alert(ex.message)
    }
}


function toggleFields() {
    var username = document.getElementById(txtUsernameID);
    var password = document.getElementById(txtPasswordID);
    var loginButton = document.getElementById(btnLoginID);

    if (username.value.trim() !== "") {
        password.disabled = false;
    } else {
        password.disabled = true;
        loginButton.disabled = true;
    }
    
    if (username.value.trim() !== "" && password.value.trim() !== "") {
        loginButton.disabled = false;
    } else {
        loginButton.disabled = true;
    }
}
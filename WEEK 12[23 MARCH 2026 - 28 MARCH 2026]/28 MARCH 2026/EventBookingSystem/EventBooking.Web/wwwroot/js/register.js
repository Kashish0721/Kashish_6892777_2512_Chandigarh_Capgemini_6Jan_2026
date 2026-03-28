function isValidEmail(email) {
    return email.indexOf("@") > -1 && email.indexOf(".") > -1;
}
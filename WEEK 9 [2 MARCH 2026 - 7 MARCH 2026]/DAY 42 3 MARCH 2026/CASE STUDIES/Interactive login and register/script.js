// Register
document.getElementById("registerForm").addEventListener("submit", function(e) {
  e.preventDefault();

  let username = regUsername.value;
  let email = regEmail.value;
  let password = regPassword.value;
  let confirm = confirmPassword.value;

  if(password.length < 6) {
    alert("Password must be at least 6 characters");
    return;
  }

  if(password !== confirm) {
    alert("Passwords do not match");
    return;
  }

  let user = { username, email, password };
  localStorage.setItem(email, JSON.stringify(user));

  alert("Registration Successful!");
});

// Login
document.getElementById("loginForm").addEventListener("submit", function(e) {
  e.preventDefault();

  let email = loginEmail.value;
  let password = loginPassword.value;

  let storedUser = JSON.parse(localStorage.getItem(email));

  if(storedUser && storedUser.password === password) {
    alert("Login Successful!");
  } else {
    alert("Invalid Credentials");
  }
});
let button = document.getElementById("btn-color");
let forms = document.getElementById("forms");

if (CurrentLogin) {
    toLogin();
} else {
    toRegister();
}

function toRegister() {
    button.style.left = "150px";
    forms.style.left = "-210px"
}

function toLogin() {
    button.style.left = "0";
    forms.style.left = "135px"
}
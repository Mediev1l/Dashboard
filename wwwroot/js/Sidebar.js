let sidebar = document.getElementsByClassName("sidebar");
let renderBody = document.getElementById("render-body");
let hamburger = document.getElementById("sidebar-hamburger");
let loginPage = document.getElementsByClassName("login-page")[0];
let hidden = true;


hamburger.addEventListener("click", () => {
    if (hidden) {
        sidebar[0].classList.remove("sidebar-small");
        renderBody.classList.add("render-body-big");
        if (loginPage != undefined) {
            loginPage.classList.add("login-small");
        }
        hidden = false;
    } else {
        sidebar[0].classList.add("sidebar-small");
        renderBody.classList.remove("render-body-big");
        if (loginPage != undefined) {
            loginPage.classList.remove("login-small");
        }
        hidden = true;
    }
})

setTimeout(setLogo, 500);

document.addEventListener("DOMContentLoaded", function (event) {
    
});

function setLogo() {
    //var logo = document.querySelector(".topbar-wrapper img");
    //if (logo) {
    //    logo.setAttribute("src", "/logos.png");
    //} else {
    //    setTimeout(setLogo, 500);
    //}
}

(function () {
    var link = document.querySelector("link[rel*='icon']") || document.createElement('link');;
    document.head.removeChild(link);
    link = document.querySelector("link[rel*='icon']") || document.createElement('link');
    document.head.removeChild(link);
    link = document.createElement('link');
    link.type = 'image/x-icon';    
    link.rel = 'shortcut icon';
    link.href = '/favicon.png';
    document.getElementsByTagName('head')[0].appendChild(link);
})();
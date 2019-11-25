const parallax = document.getElementById('parallax1');
window.addEventListener("scroll", function () {
    let offset = this.window.pageYOffset;
    parallax.style.backgroundPositionY = offset * 0.5 + "px";
})

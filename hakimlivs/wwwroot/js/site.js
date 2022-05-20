// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function placeOrder() {
    alert("Tack för din beställning.");
}

function clearCart() {
    alert("Varukorgen är nu tom.")
}

function addProduct() {
    alert("Produkt tillagd.")
}

function myFunction() {
    var x = document.getElementById("Hamburger");
    if (x.style.display === "none") {
        x.style.display = "flex";
    } else {
        x.style.display = "none";
    }
}
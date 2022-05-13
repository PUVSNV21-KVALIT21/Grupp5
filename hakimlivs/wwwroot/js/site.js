// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function clearCart() {
    alert("Varukorg rensad.");
}

function orderPlaced() {
    alert("Tack för din beställning.");
}

const form = document.getElementById("addForm");
form.onsubmit = event => {
    console.log("Hej");
    event.preventDefault();
}
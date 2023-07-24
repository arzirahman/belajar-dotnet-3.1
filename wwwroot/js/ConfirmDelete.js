// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const confirmDelete = document.getElementById("ConfirmDelete");
const confirmDeleteContent = document.getElementById("ConfirmDeleteContent");
const confirmDeleteId = document.getElementById("ConfirmDeleteId");
const confirmDeleteForm = document.getElementById("ConfirmDeleteForm");
const confirmDeleteMessage = document.getElementById("ConfirmDeleteMessage");
confirmDeleteContent.addEventListener("click", (e) => { e.stopPropagation() });
function ConfirmDeleteToggle(id = '', path = '', message = ''){
    if (confirmDelete.style.display === "none" || !confirmDelete.style.display) {
        confirmDeleteMessage.innerHTML = message;
        confirmDeleteId.value = id;
        confirmDeleteForm.action = path;
        confirmDelete.style.display = "flex";
    } else {
        confirmDelete.style.display = "none";
    }
}
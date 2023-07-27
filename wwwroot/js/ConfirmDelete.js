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
    if (confirmDelete.classList.contains('hidden')) {
        confirmDeleteMessage.innerHTML = message;
        confirmDeleteId.value = id;
        confirmDeleteForm.action = path;
        confirmDelete.classList.remove('hidden');
        confirmDelete.classList.add('flex');
        confirmDelete.classList.add('opacity-0');
        confirmDeleteContent.classList.add('translate-y-[-100%]')
        setTimeout(() => {
            confirmDelete.classList.remove('opacity-0');
            confirmDeleteContent.classList.remove("translate-y-[-100%]");
        }, 10)
    } else {
        confirmDeleteContent.classList.add("-translate-y-[100%]");
        confirmDelete.classList.add('opacity-0');
        setTimeout(() => {
            confirmDeleteContent.classList.remove("-translate-y-[100%]");
            confirmDelete.classList.add('hidden');
            confirmDelete.classList.remove('flex');
        }, 300);
    }
}
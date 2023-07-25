const modalForm = document.getElementById("ModalForm");
const modalFormContent = document.getElementById("ModalFormContent");
modalFormContent.addEventListener("click", (e) => { e.stopPropagation() });
function closeModalForm(){
    modalForm.classList.remove("flex");
    modalForm.classList.add("hidden");
}
function openModalForm(){
    modalForm.classList.remove("hidden");
    modalForm.classList.add("flex");
}
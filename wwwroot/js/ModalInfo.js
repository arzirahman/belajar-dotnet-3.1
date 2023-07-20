const modalInfo = document.getElementById("ModalInfo");
const modalInfoContent = document.getElementById("ModalInfoContent");
modalInfoContent.addEventListener("click", (e) => { e.stopPropagation() });
function closeModalInfo(){
    modalInfo.classList.remove("flex");
    modalInfo.classList.add("hidden");
}
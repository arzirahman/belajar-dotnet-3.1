const modalInfo = document.getElementById("ModalInfo");
const modalInfoContent = document.getElementById("ModalInfoContent");
modalInfoContent.addEventListener("click", (e) => { e.stopPropagation() });
function closeModalInfo(){
    modalInfoContent.classList.add("-translate-y-[100%]");
    modalInfo.classList.add('opacity-0');
    setTimeout(() => {
        modalInfoContent.classList.remove("-translate-y-[100%]");
        modalInfo.classList.add('hidden');
        modalInfo.classList.remove('flex');
    }, 300);
}
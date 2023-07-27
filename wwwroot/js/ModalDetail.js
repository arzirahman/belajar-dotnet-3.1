const modalDetail = document.getElementById("ModalDetail");
const modalDetailContent = document.getElementById("ModalDetailContent");
modalDetailContent.addEventListener("click", (e) => { e.stopPropagation() });
function modalDetailToggle(data){
    if (modalDetail.classList.contains("hidden")) {
        modalDetail.classList.remove('hidden');
        modalDetail.classList.add('flex');
        const button = `
            <button onclick="modalDetailToggle()" class="bg-[#2D4263] w-full hover:scale-105 transition-all duration-300 p-3 mt-2 font-bold rounded-md">Close</button>
        `;
        modalDetailContent.innerHTML = data + button;
        modalDetail.classList.add('opacity-0');
        modalDetailContent.classList.add('translate-y-[-100%]')
        setTimeout(() => {
            modalDetail.classList.remove('opacity-0');
            modalDetailContent.classList.remove("translate-y-[-100%]");
        }, 10)
    } else {
        modalDetailContent.classList.add("-translate-y-[100%]");
        modalDetail.classList.add('opacity-0');
        setTimeout(() => {
            modalDetailContent.classList.remove("-translate-y-[100%]");
            modalDetail.classList.add('hidden');
            modalDetail.classList.remove('flex');
        }, 300);
    }
}
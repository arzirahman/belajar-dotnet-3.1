const modalDetail = document.getElementById("ModalDetail");
const modalDetailContent = document.getElementById("ModalDetailContent");
modalDetailContent.addEventListener("click", (e) => { e.stopPropagation() });
function modalDetailToggle(data){
    console.log(data);
    if (modalDetail.style.display === "none" || !modalDetail.style.display) {
        modalDetail.style.display = "flex";
        const button = `
            <button onclick="modalDetailToggle()" class="bg-[#2D4263] w-full hover:scale-105 transition-all duration-300 p-3 mt-2 font-bold rounded-md">Close</button>
        `;
        modalDetailContent.innerHTML = data + button;
    } else {
        modalDetail.style.display = "none";
    }
}
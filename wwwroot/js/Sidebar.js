document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.getElementById("sidebar");
    const sidebarBtn = document.getElementById("sidebarBtn");
    const sidebarHidden = document.getElementById("sidebarHidden");
    const handleSidebar = () => {
        if (window.innerWidth < 768){
            sidebar.classList.add("-translate-x-[100%]");
            sidebarHidden.classList.remove("hidden");
            sidebarHidden.classList.add("flex");
        } else {
            sidebar.classList.remove("-translate-x-[100%]");
            sidebarHidden.classList.remove("flex");
            sidebarHidden.classList.add("hidden");
        }
    }
    handleSidebar();
    document.addEventListener("click", () => { handleSidebar() })
    sidebar.addEventListener("click", (e) => { e.stopPropagation() });
    sidebarBtn.addEventListener("click", (e) => {
        e.stopPropagation();
        sidebar.classList.remove("-translate-x-[100%]");
        sidebarHidden.classList.remove("flex");
        sidebarHidden.classList.add("hidden");
    })
});
const fileInput = document.getElementById("fileInput");
const profileImage = document.getElementById("profileImage");
const defaultProfileImage = document.getElementById("defaultProfileImage");
fileInput.addEventListener("change", function(event) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.onload = function() {
        profileImage.classList.remove("hidden");
        defaultProfileImage.classList.add("hidden");
        profileImage.src = reader.result;
    };
    if (file) {
        reader.readAsDataURL(file);
    }
});
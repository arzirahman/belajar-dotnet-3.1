const fileInput = document.getElementById('fileInput');
const saveButton = document.getElementById('saveButton');
fileInput.addEventListener('change', function () {
    if (fileInput.files.length > 0) {
        saveButton.style.display = 'block';
    } else {
        saveButton.style.display = 'none';
    }
});
function viewCarDetail(Name, Brand, Color, Price){
    const data = { Name, Brand, Color, Price: formatMoney(Price) };
    let content = '';
    Object.keys(data).forEach((key) => {
        content += `
            <div class="flex gap-2">
                <div>${key}</div>
                <div>:</div>
                <div>${data[key]}</div>
            </div>`
        ;
    })
    content = `
        <div class="flex flex-col gap-4">${content}</div>
    `;
    modalDetailToggle(content);
}
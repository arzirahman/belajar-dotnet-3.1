// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const confirmDelete = document.getElementById("ConfirmDelete");
const confirmDeleteContent = document.getElementById("ConfirmDeleteContent");
const confirmDeleteId = document.getElementById("ConfirmDeleteId");
const confirmDeleteForm = document.getElementById("ConfirmDeleteForm");
confirmDeleteContent.addEventListener("click", (e) => { e.stopPropagation() });
function ConfirmDeleteToggle(id = '', path = ''){
    if (confirmDelete.style.display === "none" || !confirmDelete.style.display) {
        confirmDeleteId.value = id;
        confirmDeleteForm.action = path;
        confirmDelete.style.display = "flex";
    } else {
        confirmDelete.style.display = "none";
    }
}

var lineChartConfig = {
    type: 'line',
    data: {
        labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May'],
        datasets: [{
            label: 'Data',
            data: [10, 20, 15, 25, 30],
            backgroundColor: 'rgba(0, 123, 255, 0.5)',
            borderColor: 'rgba(0, 123, 255, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true,
                ticks: {
                    color: 'white', // Mengubah warna teks sumbu Y menjadi putih
                },
                grid: {
                    color: 'rgba(255, 255, 255, 0.2)' // Mengubah warna garis grid menjadi putih
                }
            },
            x: {
                ticks: {
                    color: 'white' // Mengubah warna teks sumbu X menjadi putih
                },
                grid: {
                    color: 'rgba(255, 255, 255, 0.2)' // Mengubah warna garis grid menjadi putih
                }
            }
        },
        plugins: {
            legend: {
                labels: {
                    color: 'white' // Mengubah warna teks legenda menjadi putih
                }
            }
        }
    }
};

// Inisialisasi grafik garis
var lineChart = new Chart('lineChart', lineChartConfig);
var lineChart = new Chart("lineChart", {
    type: 'line',
    data: {
        labels: lineData.map(car => car.name),
        datasets: [{
            label: 'Data',
            data: lineData.map(car => car.price),
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
                    color: 'white',
                },
                grid: {
                    color: 'rgba(255, 255, 255, 0.2)'
                },
                title: {
                    display: true,
                    text: 'Car Price',
                    color: 'white'
                },
            },
            x: {
                ticks: {
                    color: 'white'
                },
                grid: {
                    color: 'rgba(255, 255, 255, 0.2)'
                },
                title: {
                    display: true,
                    text: 'Car Name',
                    color: 'white'
                },
            }
        },
        plugins: {
            legend: {
                labels: {
                    color: 'white'
                }
            },
            title: {
                display: true,
                text: 'Car History',
                color: 'white',
                font: {
                    size: 18
                }
            },
        }
    }
});

var barChart = new Chart("barChart", {
    type: 'bar',
    data: {
        labels: barData.map(car => car.name),
        datasets: [{
            label: 'Data',
            data: barData.map(car => car.price),
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
                    color: 'white',
                },
                grid: {
                    color: 'rgba(255, 255, 255, 0.2)'
                },
                title: {
                    display: true,
                    text: 'Car Price',
                    color: 'white'
                },
            },
            x: {
                ticks: {
                    color: 'white'
                },
                grid: {
                    color: 'rgba(255, 255, 255, 0.2)'
                },
                title: {
                    display: true,
                    text: 'Car Name',
                    color: 'white'
                },
            }
        },
        plugins: {
            legend: {
                labels: {
                    color: 'white'
                }
            },
            title: {
                display: true,
                text: 'Top 5 Cars',
                color: 'white',
                font: {
                    size: 18
                }
            },
        }
    }
});
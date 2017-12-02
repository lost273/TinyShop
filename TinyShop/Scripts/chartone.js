//var ctx = document.getElementById("bar_chart").getContext('2d');
var ctx = $("#bar_chart");
var myChart = new Chart(ctx, {
    type: 'horizontalBar',
    data: {
        labels: namesArrayOne,//for test ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"]
        datasets: [
            {
                label: dateRequestOne,
                data: totalArrayOne,//for test [12, 19, 3, 5, 2, 3]
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(112, 128, 144, 0.2)',
                    'rgba(0, 0, 205, 0.2)',
                    'rgba(0, 100, 0, 0.2)',
                    'rgba(124, 252, 0, 0.2)',
                    'rgba(255, 255, 0, 0.2)',
                    'rgba(205, 92, 92, 0.2)',
                    'rgba(210, 105, 30, 0.2)',
                    'rgba(255, 105, 180, 0.2)',
                    'rgba(125, 38, 205, 0.2)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)',
                    'rgba(112, 128, 144, 1)',
                    'rgba(0, 0, 205, 1)',
                    'rgba(0, 100, 0, 1)',
                    'rgba(124, 252, 0, 1)',
                    'rgba(255, 255, 0, 1)',
                    'rgba(205, 92, 92, 1)',
                    'rgba(210, 105, 30, 1)',
                    'rgba(255, 105, 180, 1)',
                    'rgba(125, 38, 205, 1)'
                ],
                borderWidth: 1
            },
            {
                label: dateRequestTwo,
                data: totalArrayTwo,//for test [12, 19, 3, 5, 2, 3]
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(112, 128, 144, 0.2)',
                    'rgba(0, 0, 205, 0.2)',
                    'rgba(0, 100, 0, 0.2)',
                    'rgba(124, 252, 0, 0.2)',
                    'rgba(255, 255, 0, 0.2)',
                    'rgba(205, 92, 92, 0.2)',
                    'rgba(210, 105, 30, 0.2)',
                    'rgba(255, 105, 180, 0.2)',
                    'rgba(125, 38, 205, 0.2)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)',
                    'rgba(112, 128, 144, 1)',
                    'rgba(0, 0, 205, 1)',
                    'rgba(0, 100, 0, 1)',
                    'rgba(124, 252, 0, 1)',
                    'rgba(255, 255, 0, 1)',
                    'rgba(205, 92, 92, 1)',
                    'rgba(210, 105, 30, 1)',
                    'rgba(255, 105, 180, 1)',
                    'rgba(125, 38, 205, 1)'
                ],
                borderWidth: 1
            }
        ]
    },
    options: {
        title: {
            display: true,
            text: dateRequestOne + ' ' + dateRequestTwo
        },
        maintainAspectRatio: false,
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero:true
                }
            }]
        },
        legend: { display: false }
    }
});
﻿//var ctx = document.getElementById("bar_chart").getContext('2d');
var ctx = $("#bar_charttwo");
var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: months,//string data
        datasets: [
            {
                label: periodOne,
                fill: false,
                data: yearArrayOne,//number data
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255,99,132,1)',
            },
            {
                label: periodTwo,
                fill: false,
                data: yearArrayTwo,//number data
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
            }
        ]
    },
    options: {
        maintainAspectRatio: false,
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero:true
                }
            }]
        },
        legend: { display: true }
    }
});
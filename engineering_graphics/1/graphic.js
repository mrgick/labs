function Graphic() {
    var ctx = document.getElementById('myChart');
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [],
            datasets: [{
                label: "sin(x)",
                data: [],
                backgroundColor: 'rgb(255, 99, 132)',
                borderColor: 'rgb(255, 99, 132)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
    
    //TODO: привести в порядок код
    //TODO: вынести настройку в html форму
    //TODO: добавить цвета и чтоб норм выглядело :D
    
    x_start = -6
    //y_start = -100
    x_end = 6
    //y_end = 100
    step = 1
    
    function f(x) {
        return Math.pow(x, 2);
    }

    for (var x = x_start; x<=x_end; x+=step) {
        myChart.data.labels.push(''+x.toFixed(2));
        myChart.data.datasets[0].data.push(''+f(x).toFixed(2));
       }

    myChart.update();
}

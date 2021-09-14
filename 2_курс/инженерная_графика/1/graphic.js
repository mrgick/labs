var myChart

function Graphic() {
    var ctx = document.getElementById('myChart');
    myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [],
            datasets: [{
                label: "x^2",
                data: get_data(-6, 6, 1, 'x^2'),
                backgroundColor: 'rgb(255, 99, 132)',
                borderColor: 'rgb(255, 99, 132)',
                borderWidth: 1
            }]
        },
        options: {
            //responsive: false
        }
    })

}

function get_data(x_start, x_end, step, math_formula) {

    var data = []

    for (var x = x_start; x <= x_end; x += step) {
        data.push({
            x: '' + x.toFixed(2),
            y: '' + math.parse(math_formula).compile().evaluate({ x: x }).toFixed(2)
        })
    }
    return data
}

function add_graphic() {

    var math_formula = 'sin(x)'
    var x_start = -6
    var x_end = 6
    var step = 1
    var color = 'rgb(43, 99, 132)'


    var dataset = {
        label: math_formula,
        data: get_data(x_start, x_end, step, math_formula),
        backgroundColor: color,
        borderColor: color,
        borderWidth: 1
    }

    myChart.data.datasets.push(dataset)
    myChart.update()
}



window.addEventListener("load", Graphic)
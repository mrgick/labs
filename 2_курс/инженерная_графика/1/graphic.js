var myChart

function Graphic() {
    var ctx = document.getElementById('myChart');
    myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [],
            datasets: [{
                label: "x^2",
                data: get_data(-10, 10, 1, 'x^2'),
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
    try {
        var formData = $('form').serializeArray()
        var data = {}
        for (var i = 0; i < formData.length; i++) {
            data[formData[i].name] = formData[i].value;
        }

        console.log(data)
        var math_formula = data["math_formula"]
        var x_start = parseInt(data["x_start"], 10)
        var x_end = parseInt(data["x_end"], 10)
        var step = parseInt(data["x_step"], 10)
        var color = data["graphic_color"]


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
    catch (err) {
        console.error(err)
        alert(err)
    }
}




window.addEventListener("load", Graphic)
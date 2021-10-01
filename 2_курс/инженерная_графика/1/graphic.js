var myChart

function Graphic() {
    var ctx = document.getElementById('myChart');
    myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [0],
            datasets: [{
                label: "log(x,e)",
                data: get_data(1, 20, 0.5, 'log(x,e)'),
                backgroundColor: '#FF0000',
                borderColor: '#FF0000',
                borderWidth: 1
            },
            {
                label: "log(x,10)",
                data: get_data(1, 20, 0.5, 'log(x,10)'),
                backgroundColor: '#4100FF',
                borderColor: '#4100FF',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    title: {
                    display: true,
                    align: 'end',
                    text: 'y'
                  }
                },
                x: {
                    title: {
                    display: true,
                    align: 'end',
                    text: 'x'
                  }
                }
              }
            //responsive: false
        }
    })

    let canvas = document.getElementById('myChart')
    let ctx2 = canvas.getContext("2d")
    ctx.font = "40px arial";
    //ctx.fillText("Hello world", 10, 50);
    //ctx.beginPath()
    //ctx.moveTo(0, y_center)


}

function get_data(x_start, x_end, step, math_formula) {
    try {
        var expression = math.parse(math_formula).compile()
        var data = []

        for (var x = x_start; x <= x_end; x += step) {
            y = expression.evaluate({ x: x })
            data.push({
                x: '' + x.toFixed(2),
                y: '' + (+y).toFixed(2)
            })
        }
        return data
    }
    catch (err) {
        console.error(err)
        alert(err)
    }
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
        var x_start = parseFloat(data["x_start"], 10)
        var x_end = parseFloat(data["x_end"], 10)
        var step = parseFloat(data["x_step"], 10)
        var color = data["graphic_color"]

        var x_y = get_data(x_start, x_end, step, math_formula)

        var dataset = {
            label: math_formula,
            data: x_y,
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

function remove_last() {
    try {
        myChart.data.datasets.pop()
        myChart.update()
    }
    catch (err) {
        console.error(err)
        alert(err)
    }
}

function remove_graphics() {
    try {
        myChart.data.datasets = []
        myChart.data.labels = []
        myChart.update()
    }
    catch (err) {
        console.error(err)
        alert(err)
    }
}


window.addEventListener("load", Graphic)
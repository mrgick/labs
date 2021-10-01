let canvas, ctx
let max_dots = 3
let dots_list = []
let x_center, y_center

let fill_color = "#ffffff"
let line_color = "#000000"
let dot_color = "#fff00f"

let custom_dot_color = "#0000ff"
let custom_dot = {
    x: 201,
    y: 201
}

function new_polygon() {
    max_dots = parseInt(document.getElementById("dots_number").value)
    dots_list = []
    clear_canvas()
}

function cos(x) {
    return Math.cos(x)//parseFloat(Math.cos(x).toFixed(3))
}

function sin(x) {
    return Math.sin(x)//parseFloat(Math.sin(x).toFixed(3))
}


function get_new_xy(p, a) {
    a = - a * Math.PI / 180
    //console.log("SIN(A)=", sin(a));
    //console.log("COS(A)=", cos(a));

    //console.log(a);
    let x = p.x - custom_dot.x
    let y = p.y - custom_dot.y
    //a = 1.57 //- a * Math.PI / 180
    //console.log(x, y);
    let new_x = ((x * cos(a)) - (y * sin(a)))
    y = ((x * sin(a)) + (y * cos(a)))
    //console.log(x, y);
    return {
        x: (new_x + custom_dot.x),
        y: (y + custom_dot.y)
    }
}


function rotate() {
    let angle = parseInt(document.getElementById("angle_of_rotation").value)
    let new_dots = []
    clear_canvas()
    for (let i = 0; i < dots_list.length; i++) {
        p = dots_list[i]
        new_dots.push(get_new_xy(p, angle))
        draw_dot(new_dots[i])
    }
    dots_list = new_dots
    draw_polygon()
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

let forever
async function rotate_forever() {
    forever = true
    while (forever == true) {
        rotate()
        await sleep(30)
    }
}


function clear_canvas() {
    // заполняем белым
    ctx.fillStyle = fill_color
    ctx.fillRect(0, 0, canvas.getAttribute("height"), canvas.getAttribute("width"))
    // рисуем оси
    ctx.strokeStyle = line_color
    ctx.beginPath()
    ctx.moveTo(0, y_center)
    ctx.lineTo(canvas.getAttribute("width"), y_center)
    ctx.moveTo(x_center, 0)
    ctx.lineTo(x_center, canvas.getAttribute("height"))
    ctx.closePath()
    ctx.stroke()
    draw_dot(custom_dot, custom_dot_color)
}

function draw_dot(p, color = dot_color) {
    ctx.strokeStyle = line_color
    ctx.fillStyle = color
    ctx.beginPath()
    console.log(p.x, p.y)
    ctx.arc(p.x, p.y, 5, 0, Math.PI * 2, true)
    ctx.closePath()
    ctx.fill()
    ctx.stroke()
}

function draw_polygon() {
    console.log(dots_list)
    ctx.strokeStyle = line_color
    ctx.beginPath()
    for (let i = 0; i < dots_list.length; i++) {
        p = dots_list[i]
        ctx.moveTo(p.x, p.y)
        if (i != dots_list.length - 1) {
            p = dots_list[i + 1]
        }
        else {
            p = dots_list[0]
        }
        ctx.lineTo(p.x, p.y)
    }
    ctx.closePath()
    ctx.stroke()
}

function getMousePos(canvas, evt) {
    let rect = canvas.getBoundingClientRect()

    if (dots_list.length == max_dots) {
        
        custom_dot = {
            x: evt.clientX - rect.left,
            y: evt.clientY - rect.top
        }
        draw_dot(custom_dot, custom_dot_color)
        return
    }

    dots_list.push({
        x: evt.clientX - rect.left,
        y: evt.clientY - rect.top
    })

    if (dots_list.length <= max_dots) {
        draw_dot(dots_list.at(-1))
    }

    if (dots_list.length == max_dots) {
        draw_polygon()
    }

}

window.onload = function () {
    canvas = document.getElementById("graph")
    x_center = canvas.getAttribute("height") / 2
    y_center = canvas.getAttribute("width") / 2
    ctx = canvas.getContext("2d")
    clear_canvas()

    canvas.addEventListener("click", function (evt) {
        getMousePos(canvas, evt)
    }, false)

    let checkb = document.getElementById("rotate_forever")
    checkb.addEventListener("change", function(e) {
        if (this.checked) {
            rotate_forever()
        } else {
            forever = false
        }
    });

}



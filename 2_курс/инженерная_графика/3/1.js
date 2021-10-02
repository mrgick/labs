let canvas, ctx
let max_dots = 4
let dots_list = []

let clear_color = "#ffffff"
let line_color = "#000000"
let dot_color = "#fff00f"


function auto_check() {
    
}

function decompose_to_triangles() {
    
}


function new_polygon() {
    if (document.getElementById("dots_number").value == "") {
        max_dots = 4
    } else {
        max_dots = parseInt(document.getElementById("dots_number").value)
        if (max_dots < 4) {
            max_dots = 4
        }
    }
    dots_list = []
    clear_canvas()
}

function clear_canvas() {
    ctx.fillStyle = clear_color
    ctx.fillRect(0, 0, canvas.getAttribute("height"), canvas.getAttribute("width"))
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

function get_mous_pos(canvas, evt) {
    let rect = canvas.getBoundingClientRect()

    if (dots_list.length == max_dots) {
        return
    }

    dots_list.push({
        x: evt.clientX - rect.left,
        y: evt.clientY - rect.top
    })

    if (dots_list.length <= max_dots) {
        draw_dot(dots_list[dots_list.length - 1])
    }

    if (dots_list.length == max_dots) {
        draw_polygon()
    }

}


window.onload = function () {
    canvas = document.getElementById("graph")
    ctx = canvas.getContext("2d")
    clear_canvas()

    canvas.addEventListener("click", function (evt) {
        get_mous_pos(canvas, evt)
    })

}

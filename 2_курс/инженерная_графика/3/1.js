let canvas, ctx
let max_dots = 4
let dots_list = []

let clear_color = "#ffffff"
let line_color = "#000000"
let dot_color = "#fff00f"


function auto_check() {

}

function decompose_to_triangles() {

    function draw_line(p1, p2) {
        ctx.strokeStyle = line_color
        ctx.beginPath()
        ctx.moveTo(p1.x, p1.y)
        ctx.lineTo(p2.x, p2.y)
        ctx.closePath()
        ctx.stroke()
    }

    function determinant_of_a_matrix(p1, p2, p3) {


        l = (p1.x * p2.y * 1) + (p2.x * p3.y * 1) + (p3.x * p1.y * 1)
        r = (1 * p2.y * p3.x) + (1 * p3.y * p1.x) + (1 * p1.y * p2.x)
        return (l - r)
    }


    if (dots_list == []) {
        return
    }

    // обход должен быть против часовой
    if (dots_list[1].y <= dots_list[2].y){
        dots_list.reverse()
    }

    let i1, i2, i3, s_d = 0

    for (let i = 0; i < dots_list.length; i++) {
        i1 = i - 1
        i2 = i
        i3 = i + 1

        if (i1 == -1) {
            i1 = dots_list.length - 1
        }

        if (i3 == dots_list.length) {
            i3 = 0
        }

        let D = determinant_of_a_matrix(dots_list[i1], dots_list[i2], dots_list[i3])
        if (D > 0) {
            console.log(i, D);
            s_d = i
        }

    }
    //console.log(determinant_of_a_matrix({ x: 1, y: 2 }, { x: 2, y: 3 }, { x: 4, y: 5 }));

    for (let i = 0; i < dots_list.length; i++) {
        draw_line(dots_list[s_d], dots_list[i])
    }
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

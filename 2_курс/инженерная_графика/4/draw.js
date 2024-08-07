const fill_colour = "#56CEDB",
    axis_colour = "#BF0071",
    line_colour = "#EAE1DB",
    dot_colour = "#FF9900",
    side_colour = "#F0F000"

const colours = [
    "#39D1DA",
    "#39D1DA",
    "#39D1DA",
    "#39D1DA",
    "#39D1DA",
    "#39D1DA"]



function get_ctx() {
    canvas = document.getElementById("plain")
    ctx = canvas.getContext("2d")
    return ctx
}


// очищение поля внутри canvas
function clear_canvas() {
    ctx = get_ctx()
    ctx.fillStyle = fill_colour
    ctx.fillRect(0, 0, canvas.getAttribute("height"), canvas.getAttribute("width"))
    var blueprint_background = new Image();
}


function draw_dot(p, colour = dot_colour) {
    ctx = get_ctx()
    ctx.strokeStyle = line_colour // line colour
    ctx.fillStyle = colour        // fill colour

    ctx.beginPath()
    ctx.arc(p.x, p.y, 5, 0, Math.PI * 2, true)
    ctx.closePath()

    ctx.fill()
    ctx.stroke()
}


function draw_axises() {
    ctx = get_ctx()
    ctx.strokeStyle = axis_colour
    let O = {
        x: canvas.getAttribute("height") / 2,
        y: canvas.getAttribute("width") / 2
    }
    res = {
        x: canvas.getAttribute("height"),
        y: canvas.getAttribute("width")
    }
    L = 3000 / dist

    ctx.beginPath()
    ctx.lineTo(O.x, O.y)
    ctx.lineTo(O.x + L * X.dx, O.y + L * X.dy) // --> X
    ctx.lineTo(O.x, O.y)
    ctx.lineTo(O.x + L * Y.dx, O.y + L * Y.dy) // --> Y
    ctx.lineTo(O.x, O.y)
    ctx.lineTo(O.x + L * Z.dx, O.y + L * Z.dy) // --> Z
    ctx.closePath()

    ctx.fill()
    ctx.stroke()
}


function to_2d(point) {
    let O = {
        x: canvas.getAttribute("height") / 2,
        y: canvas.getAttribute("width") / 2
    }
    return {
        x: O.x + (point[0] * X.dx + point[1] * Y.dx + point[2] * Z.dx) * scale,
        y: O.y + (point[0] * X.dy + point[1] * Y.dy + point[2] * Z.dy) * scale
    }
}


function draw_polygon(coords, colour = side_colour) {
    ctx = get_ctx()
    ctx.strokeStyle = line_colour
    ctx.fillStyle = colour
    ctx.lineWidth = 2

    ctx.beginPath()
    p = to_2d(coords[0])
    ctx.lineTo(p.x, p.y)
    for (let i = 1; i < coords.length; ++i) {
        p = to_2d(coords[i])
        ctx.lineTo(p.x, p.y)
    }
    ctx.closePath()


    ctx.fill()
    ctx.stroke()
}


function check_equality(p1, p2, e = 1e-5) {
    if (p1[0] - p2[0] < e && p1[1] - p2[1] < e && p1[2] - p2[2] < e) return true
    else return false
}

let dot_drawing = false

function draw_cube() {
    clear_canvas()

    let longest = 0
    let longest_dot = -1
    let c = to_decard([dist, theta, phi])
    let dot, tmp

    ctx = get_ctx()
    for (let i = 0; i < cube.length; ++i) {
        for (let j = 0; j < cube[i].length; ++j) {
            dot = cube[i][j]
            tmp = distance_beetween(c, dot)
            if (tmp > longest) {
                longest = tmp
                longest_dot = dot
            }
        }
    }

    //console.log(c, tmp, longest_dot);

    let add = true
    let new_cube = []

    //проверка граней
    for (let i = 0; i < cube.length; ++i) {
        add = true
        for (let j = 0; j < cube[i].length; ++j) if (check_equality(cube[i][j], longest_dot)) add = false
        if (add) new_cube.push(cube[i])
        //else new_cube.unshift(cube[i])
    }

    for (let i = 0; i < new_cube.length; ++i) {
        let j = cube.indexOf(new_cube[i])
        draw_polygon(new_cube[i], colours[j])
    }
    if (dot_drawing == true) {
        draw_dot(to_2d(longest_dot), "#FFFFFF")
    }
    c = to_sphere(longest_dot)
    //console.log(c[1]*180/Math.PI, c[2]*180/Math.PI);

}
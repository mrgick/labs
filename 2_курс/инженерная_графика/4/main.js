const P = Math.PI / 2;

let dist = 10                 // distance to viewer
theta = 45 / 180 * Math.PI // 1st angle to viewer
phi = 45 / 180 * Math.PI // 2nd angle to viewer
ratio = (P - theta) / P    // ratio

X = {
    dx: 1 * Math.cos(theta),
    dy: 1 * Math.sin(theta - phi * (1 - ratio))
}
Y = {
    dx: 0,
    dy: -1 * Math.sin(phi)
}
Z = {
    dx: -1 * Math.cos(P - theta),
    dy: 1 * Math.sin(P - theta - phi * ratio)
}
// all dy were inverted (multiplied by -1)

function set_view() {
    dist = i_dist.value % 360
    theta = i_theta.value % 360 / 180 * Math.PI
    phi = i_phi.value % 360 / 180 * Math.PI

    while (theta < 0) theta += P
    ratio = (P - theta % P) / P

    //Math.tan(P/2+Math.floor(theta/P)*P)

    if ((90 <= i_theta.value && i_theta.value < 180) ||
        (270 <= i_theta.value && i_theta.value < 360)) {
        X.dx = 1 * Math.cos(theta)
        X.dy = 1 * Math.sin(theta + phi * ratio)
        Y.dx = 0
        Y.dy = -1 * Math.sin(phi)
        Z.dx = -1 * Math.cos(P - theta)
        Z.dy = 1 * Math.sin(P - theta + phi * (1 - ratio))
    }
    else {
        X.dx = 1 * Math.cos(theta)
        X.dy = 1 * Math.sin(theta - phi * (1 - ratio))
        Y.dx = 0
        Y.dy = -1 * Math.sin(phi)
        Z.dx = -1 * Math.cos(P - theta)
        Z.dy = 1 * Math.sin(P - theta - phi * ratio)
    }
    scale = 1000 / dist

}

let delay = 10
function set_delay() {
    delay = parseFloat(rotation_delay.value)
}


function to_decard(point) {
    return [
        point[0] * Math.sin(point[2]) * Math.sin(point[1]),
        point[0] * Math.cos(point[2]),
        point[0] * Math.sin(point[2]) * Math.cos(point[1])
    ]
}


function to_sphere(point = [0, 0, 0]) {
    let r = (point[0] ** 2 + point[1] ** 2 + point[2] ** 2) ** 0.5
    return [
        r,
        Math.asin(point[1] / ((point[0] ** 2 + point[1] ** 2) ** 0.5)),
        Math.acos(point[2] / r)
    ]
}


function distance_beetween(p1, p2) {
    // сделано в России!
    // меньше погрешность
    return (Math.abs(p1[0] - p2[0]) + Math.abs(p1[1] - p2[1]) + Math.abs(p1[2] - p2[2]))
    //return ((p1[0] - p2[0])**2 + (p1[1] - p2[1])**2 + (p1[2] - p2[2])**2)**.5
}


function set_vector() {
    vector[0] = parseFloat(i_X.value)
    vector[1] = parseFloat(i_Y.value)
    vector[2] = parseFloat(i_Z.value)
}

function show_dot() {
    if (dot_drawing == false) {
        dot_drawing = true
    } else {
        dot_drawing = false
    }
}

async function main() {
    function sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
    while (true) {
        //i_theta.value = parseInt(i_theta.value) + 1
        //i_phi.value = parseInt(i_phi.value) - 1

        //if (i_theta.value > 360) i_theta.value = 0;
        //if (i_phi.value > 360) i_phi.value = 0;

        //set_view()
        //clear_canvas()
        //draw_axises()
        //draw_figure()
        //rotate_3d()
        rotate_cube()
        draw_cube()
        await sleep(delay)

    }
    //rotate_3d()
    //draw_cube()


}

window.onload = async function () { await main() }

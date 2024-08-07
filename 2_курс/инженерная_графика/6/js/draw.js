const fill_color = "#FFFFFF",
    dot_color = "#FFFFFF"

function draw() {
    let ctx = plain.getContext("2d")
    ctx.clearRect(0, 0, plain.width, plain.height);
}

function put_pixel(x, y, color = dot_color) {
    let ctx = plain.getContext("2d")
    ctx.fillStyle = color
    ctx.fillRect(x, y, 1, 1);
}

function to_2d(point) {
    const P = Math.PI / 2

    let dist = 30                // distance to viewer
    let theta = 0 / 180 * Math.PI // 1st angle to viewer
    let phi = 90 / 180 * Math.PI // 2nd angle to viewer
    let ratio = (P - theta) / P    // ratio
    let scale = 1000 / dist

    let X = {
        dx: 1 * Math.cos(theta),
        dy: 1 * Math.sin(theta - phi * (1 - ratio))
    }

    let Y = {
        dx: 0,
        dy: -1 * Math.sin(phi)
    }

    let Z = {
        dx: -1 * Math.cos(P - theta),
        dy: 1 * Math.sin(P - theta - phi * ratio)
    }

    let O = {
        x: plain.width / 2,
        y: plain.height / 2
    }

    return {
        x: O.x + (point[0] * X.dx + point[1] * Y.dx + point[2] * Z.dx) * scale,
        y: O.y + (point[0] * X.dy + point[1] * Y.dy + point[2] * Z.dy) * scale
    }
}
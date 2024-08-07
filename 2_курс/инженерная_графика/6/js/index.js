let x_angle = 25, y_angle = 15

async function create() {

    function sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    let stepx = 0.01, stepz = 0.1
    let color = "FFFFFF", color_step = -10
    let delay = 0

    if (x_step.value) {
        stepx = parseFloat(x_step.value)
    }

    if (z_step.value) {
        stepz = parseFloat(z_step.value)
    }

    if (color_hex.value) {
        color = parseInt(color_hex.value, 16).toString(16)
    }

    if (color_st.value) {
        color_step = parseInt(color_st.value)
    }

    if (delay_num.value) {
        if (delay_num.value > 0) {
            delay = parseFloat(delay_num.value)
        }
        else {
            delay = 0
        }
    }

    let ctx = plain.getContext("2d")
    ctx.clearRect(0, 0, plain.width, plain.height);

    let max_y = {}
    let min_y = {}
    for (let z = 2 * Math.PI; z > -2 * Math.PI; z -= stepz) {
        for (let x_ = -2 * Math.PI; x_ < 2 * Math.PI; x_ += stepx) {
            y_ = F(x_, z)
            dot = [x_, y_, z]
            dot = rotate_dot(x_, y_, z, x_angle, y_angle)
            dot = to_2d(dot, 1)
            //console.log(dot);

            //put_pixel(dot.x, dot.y)

            let x = parseInt(dot.x)
            let y = dot.y
            //console.log(x, y)

            if (max_y[x] && min_y[x]) {
                if (y > max_y[x]) {
                    put_pixel(dot.x, y, '#' + color)
                    max_y[x] = y
                } else if (y < min_y[x]) {
                    put_pixel(dot.x, y, '#' + color)
                    min_y[x] = y
                }
            }
            else {
                put_pixel(dot.x, y, '#' + color)
                max_y[x] = y
                min_y[x] = y
            }

        }
        color = (parseInt(color, 16) + color_step).toString(16)
        if (delay > 0) {
            await sleep(delay)
        }
        //console.log(color);
    }
}

window.onload = function () {
    create()
}
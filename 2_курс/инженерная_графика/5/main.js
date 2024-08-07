const fill_color = "#FFFFFF"

function get_R() {
    R = parseFloat(r_num.value)
    if (R < 1) {
        R = 1
    }
    return R
}

function get_delay() {
    delay = parseFloat(delay_num.value)
    if (delay < 0) {
        delay = 0
    }
    return delay
}

async function create() {

    function sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    let sz = Math.floor(plain.width / 32)
    let R = get_R() * sz
    if (plain.height < parseInt(R * 2.1) ||
        plain.width < parseInt(R * 2.1)) {
        plain.width = parseInt(R * 2.1)
        plain.height = parseInt(R * 2.1)
    }

    let ctx = plain.getContext("2d")
    ctx.clearRect(0, 0, plain.width, plain.height);
    ctx.fillStyle = fill_color

    let delay = get_delay()
    O = {
        x: plain.width / 2,
        y: plain.height / 2
    }

    /* 
    Алгоритм отсюда:
    https://ru.wikipedia.org/wiki/Алгоритм_Брезенхэма#Рисование_окружностей
    */

    let x = 0
    let y = R
    let delta = 1 - 2 * R
    let error = 0

    O.x -= O.x % sz,
        O.y -= O.y % sz

    while (y >= x) {
        ctx.fillRect(O.x + x, O.y + y, sz, sz)
        ctx.fillRect(O.x + x, O.y - y, sz, sz)
        ctx.fillRect(O.x - x, O.y + y, sz, sz)
        ctx.fillRect(O.x - x, O.y - y, sz, sz)
        ctx.fillRect(O.x + y, O.y + x, sz, sz)
        ctx.fillRect(O.x + y, O.y - x, sz, sz)
        ctx.fillRect(O.x - y, O.y + x, sz, sz)
        ctx.fillRect(O.x - y, O.y - x, sz, sz)
        error = 2 * (delta + y) - sz
        if ((delta < 0) && (error <= 0)) {
            x += sz
            delta += 2 * x + sz
            continue
        }
        if ((delta > 0) && (error > 0)) {
            y -= sz
            delta -= 2 * y + sz
            continue
        }
        x += sz
        y -= sz
        delta += 2 * (x - y)
        if (delay > 0) {
            await sleep(delay)
        }
    }
}
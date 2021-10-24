function main() {
    let y_start = 11 / 9,
        x_start = 0,
        x_end = 1,
        h = 0.05

    let n = (x_end - x_start) / h,
        x = x_start,
        y = [],
        k = []

    y[1] = y_start
    for (i = 1; i <= n + 1; i++) {
        k[1] = h * f(x, y[i])
        k[2] = h * f(x + h / 2, y[i] + k[1] / 2)
        k[3] = h * f(x + h / 2, y[i] + k[2] / 2)
        k[4] = h * f(x + h, y[i] + k[3])
        y[i + 1] = y[i] + (k[1] + 2 * k[2] + 2 * k[3] + k[4]) / 6
        console.log(`x=${x.toFixed(2)}, y=${y[i].toFixed(6)}`)
        x += h
    }

}

function f(x, y) {
    return Math.exp(2 * x) * (x - 1) + y
}

main()
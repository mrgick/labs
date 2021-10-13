function main() {
    let y_start = 11 / 9,
        x_start = 0,
        x_end = 1,
        h = 0.05
    let x = [x_start], y = [y_start]
    let k = [], res = {}
    res[x[0]] = y[0]

    for (let i = 0; i < 3; i++) {
        k[1] = h * f(x[i], y[i])
        k[2] = h * f(x[i] + h / 2, y[i] + k[1] / 2)
        k[3] = h * f(x[i] + h / 2, y[i] + k[2] / 2)
        k[4] = h * f(x[i] + h, y[i] + k[3])
        y[i + 1] = y[i] + (k[1] + 2 * k[2] + 2 * k[3] + k[4]) / 6
        x[i + 1] = x[i] + h
        res[x[i + 1]] = y[i + 1]
    }

    let func = []
    for (let i = 0; i < 4; i++) {
        func[i] = f(x[i], y[i])
    }

    do {
        y[3] = y[3] + h / 24 * (55 * func[3] - 59 * func[2] + 37 * func[1] - 9 * func[0])
        x[3] = x[3] + h
        func[0] = func[1]
        func[1] = func[2]
        func[2] = func[3]
        func[3] = f(x[3], y[3])
        res[x[3]] = y[3]
    } while (x[3] <= x_end)

    for (key in res) {
        console.log(`${parseFloat(key).toFixed(2)}:${res[key].toFixed(4)}`);
    }

}

function f(x, y) {
    return Math.exp(2 * x) * (x - 1) + y
}

main()
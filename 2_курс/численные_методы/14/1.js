function main() {
    let eps = 0.1, N = 20, a = [0.004] // из-за расхождения пришлось подрегулировать a[0], x[0] и y[0]
    let x = [-0.3], y = [-0.3], F = []
    let i, k = 0
    do {
        k++
        F[0] = f(x[0], y[0])
        i = 1
        do {
            F[1] = F[0]
            x[1] = x[0] - i * a[0] * f_dx(x[0], y[0])
            y[1] = y[0] - i * a[0] * f_dy(x[0], y[0])
            F[0] = f(x[1], y[1])
            i++
        } while (F[1] > F[0])
        if (k > N) break
        a[k] = a[0] * (i - 1)
        if (a[1] > 0) {
            x[0] = x[0] - a[k] * f_dx(x[0], y[0])
            y[0] = y[0] - a[k] * f_dy(x[0], y[0])
            grad = f_dx(x[0], y[0]) + f_dy(x[0], y[0])
            console.log(`${k})x=${x[0].toFixed(2)}, y=${y[0].toFixed(2)}, f(x,y)=${f(x[0],y[0]).toFixed(2)}, grad=${grad.toFixed(2)}, a[k]=${a[k].toFixed(2)}`);
        } else {
            a[0] = a[0] / 10
        }
    } while (f_dx(x[0], y[0]) ** 2 + f_dy(x[0], y[0]) ** 2 > eps**2)
}

function f(x, y) {
    return 25 * x + 0.9 * y + Math.exp(5.76 * x ** 2 + 0.35 * y ** 2)
}

function f_dx(x, y) {
    return 25 + 5.76 * 2 * x * Math.exp(5.76 * x ** 2 + 0.35 * y ** 2)
}

function f_dy(x, y) {
    return 0.9 + 0.35 * 2 * y * Math.exp(5.76 * x ** 2 + 0.35 * y ** 2)
}

main()
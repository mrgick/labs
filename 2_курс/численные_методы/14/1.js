function main() {
    let eps = 0.1, N = 20, a0 = 0.1
    let x0 = -0.3, y0 = -0.3
    let n = 0, i = 0
    let ak, F, F_old, xn

    while (true) {
        gradient = grad(x0, y0)
        if (Math.abs(gradient) <= eps) break;

        i = 1
        F = f(x0, y0)
        while (true) {
            xn = x0 - i * a0 * f_dx(x0, y0)
            yn = y0 - i * a0 * f_dy(x0, y0)
            F_old = F
            F = f(xn, yn)
            if (F > F_old) break;
            i++
        }
        
        if (n >= N) break;

        ak = a0 * (i - 1);
        if (ak > 0) {
            x0 = x0 - ak * f_dx(x0, y0)
            y0 = y0 - ak * f_dy(x0, y0)
            n++
            console.log(`${n})x=${x0.toFixed(4)}, y=${y0.toFixed(4)}, f(x,y)=${f(x0, y0).toFixed(4)}, grad=${grad(x0, y0).toFixed(4)}, ak=${ak.toFixed(2)}`)
        }
        else {
            a0 /= 10
        }
    }

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

function grad(x, y) {
    return (f_dx(x, y) ** 2 + f_dy(x, y) ** 2) ** 0.5
}

main()
function main() {
    let x = -5, y = -5,
        eps = 10 ** (-2) // здесь должно быть -2 по условию, но при -3 или -4 значение лучше, следует поиграться

    let h = 1, dx=0, dy=0, g = eps, i = 0
    do {
        i++
        x = x - h * dx
        y = y - h * dy
        F = f(x, y)
        dx = (f(x + g, y) - f(x, y)) / g
        dy = (f(x, y + g) - f(x, y)) / g
        console.log(`i=${i.toFixed()}, x=${x.toFixed(3)}, y=${y.toFixed(3)}, df/dx=${dx.toFixed(3)}, df/dy=${dy.toFixed(3)}, grad=${grad(x,y).toFixed(3)}, f(x,y)=${f(x,y).toFixed(3)}`)
    } while (Math.abs(grad(dx, dy)) > eps);
    console.log(`Amount of iterations:${i.toFixed()}, x=${x.toFixed(3)}, y=${y.toFixed(3)}, f(x,y)=${f(x,y).toFixed(3)}`)
}

function grad(dx, dy) {
    return Math.sqrt(dx ** 2 + dy ** 2)
}

function f(x, y) {
    return (
        0.15 * Math.pow(x, 2) +
        0.2 * Math.pow(y, 2) +
        0.28 * x * y +
        Math.sin(0.06 * Math.pow(x - 0.18, 2)) +
        Math.cos(0.098 * Math.pow(y - 0.31, 2))
    )
}

main()
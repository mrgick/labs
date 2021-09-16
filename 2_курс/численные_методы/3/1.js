function f(x) {
    return (Math.exp(x) * (1 + Math.sin(x)) / (1 + Math.cos(x)))
}


function method_rectangles(a, n, h, t = 0) {
    let sum, Δ, I

    switch (t) {
        case 1:
            Δ = h / 2
            break;
        case 2:
            Δ = h
            break;
        default:
            Δ = 0
            break;
    }

    sum = 0
    for (let i = 1; i < n; i++) {
        sum += f(a + i * h + Δ)
    }

    return h * sum
}


function method_trapezes(a, b, n, h) {
    let sum = 0
    for (let i = 1; i < n; i++) {
        sum += f(a + i * h)
    }
    return h / 2 * (f(a) + f(b) + 2 * sum)
}


function method_simpsona(a, b, n, h) {
    let sum = 0
    for (let i = 1; i < n; i++) {
        sum += (3 - Math.pow(-1, i)) * f(a + i * h)
    }
    return h / 3 * (f(a) + f(b) + sum)
}


function main() {
    let a, b, n, h, result
    a = 0
    b = 1.5
    n = 132
    h = (b - a) / n
    result = method_rectangles(a, n, h, 0)
    console.log(`Left rectangles: I = ${result.toFixed(7)}`)

    result = method_rectangles(a, n, h, 1)
    console.log(`Center rectangles: I = ${result.toFixed(7)}`)

    result = method_rectangles(a, n, h, 2)
    console.log(`Right rectangles: I = ${result.toFixed(7)}`)

    result = method_trapezes(a, b, n, h)
    console.log(`Trapezes: I = ${result.toFixed(7)}`)

    result = method_simpsona(a, b, n, h)
    console.log(`Simpson: I = ${result.toFixed(7)}`)

}


if (require.main === module) {
    main()
}

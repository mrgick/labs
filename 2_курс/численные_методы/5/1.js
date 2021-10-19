function f(x) {
    return (Math.log(0.1 * x) + Math.exp(0.4 * x)) / (Math.sin(x) + 1.5)
}

function sympson(b, e) {

    let a = 0.1,
        N = 2

    let z = f(a) + f(b),
        I_old,
        I_new = 0,
        h,
        even = 0,            // чётное
        odd = f((a + b) / 2) // нечётное

    do {
        even += odd
        h = (b - a) / N
        odd = 0
        for (let i = 1; i <= N - 1; i += 2) {
            odd += f(a + h * i)
        }
        I_old = I_new
        I_new = (h / 3) * (z + 4 * even + 2 * odd)
        N *= 2
    } while (Math.abs(I_new - I_old) > e)
    return I_new
}


function main() {
    let a = 0.2,
        b = 8,
        e = 0.01,
        e1 = 0.2

    let i = 0,
        x

    do {
        x = (a + b) / 2
        if ((sympson(a, e1) * sympson(x, e1)) < 0)
            b = x
        else
            a = x
        console.log(`i = ${i} a = ${a.toFixed(4)} f(a) = ${sympson(x, e).toFixed(5)}`)
        i++
        e1 = 0.47 * e1
        //console.log(e1);
    } while (Math.abs(b - a) > e)
}


main()
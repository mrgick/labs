function f(x) {
    return (Math.log(0.1 * x) + Math.exp(0.4 * x)) / (Math.sin(x) + 1.5)
}

function sympson(b, e) {
    let n = 2,
        a = 0.1,
        Sum_odd, // нечетное
        Sum_even, // четное
        I_old,
        h, z

    Sum_odd = 0
    I_new = 0
    z = f(a) + f(b)

    do {
        h = (b - a) / n
        Sum_even = 0
        for (let i = 1; i <= n - 1; i += 2) {
            Sum_even += f(a + i * h);
        }
        Sum_odd += Sum_even
        I_old = I_new
        I_new = (h / 3) * (z + 4 * Sum_even + 2 * Sum_odd)
        n *= 2
    } while (Math.abs(I_old - I_new) > e)

    return I_old

}

function main() {
    let a = 0.2,
        b = 8,
        e = 0.001

    let i = 0,
        x

    do {
        x = (a + b) / 2
        if ((sympson(a, e) * sympson(x, e)) < 0)
            b = x
        else
            a = x
        console.log(`i = ${i} a = ${a} f(a) = ${sympson(x, e)}`)
        i++
    } while (Math.abs(b - a) > e)
}

main()
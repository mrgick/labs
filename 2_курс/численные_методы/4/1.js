function f(x) {
    return (Math.exp(x) * (1 + Math.sin(x)) / (1 + Math.cos(x)))
}


function main() {

    let a = 0,
        b = 1.5,
        e = 10 ** -5,
        N = 2

    let z = f(a) + f(b),
        counter = 0,
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
        counter++
        N *= 2
        console.log(`n = ${counter} I_old = ${I_old.toFixed(5)} I_new = ${I_new.toFixed(5)}`)
    } while (Math.abs(I_new - I_old) > e) 

    console.log(`Answer = ${I_new.toFixed(5)}`)
}


if (require.main === module) {
    main()
}
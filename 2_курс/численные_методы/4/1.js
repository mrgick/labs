function f(x) {
    return (Math.exp(x) * (1 + Math.sin(x)) / (1 + Math.cos(x)))
}


function main() {

    let a = 0,
        b = 1.5,
        e = 0.00001,
        N = 2

    let z = f(a) - f(b),
        counter = 0,
        I_old = e + 1,
        I_new = 0,
        h,
        odd, // нечётное
        even // чётное

    for (; Math.abs(I_new - I_old) > e; N *= 2) {
        odd = 0
        even = 0

        h = (b - a) / N

        for (let i = 1; i <= N - 1; i += 2) {
            even += f(a + h * i)
            odd += f(a + h * (i + 1))
        }

        I_old = I_new
        I_new = (h / 3) * (z + 4 * even + 2 * odd)
        counter++
        
        console.log(`n = ${counter} I_old = ${I_old.toFixed(5)} I_new = ${I_new.toFixed(5)}`)
    }
    console.log(`Answer = ${I_new}`)
}


if (require.main === module) {
    main()
}
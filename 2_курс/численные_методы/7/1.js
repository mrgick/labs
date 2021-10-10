function main() {
    let a = [
        [28, 9, 70, 43],
        [7, 28, 43, 3],
        [75, 98, 76, 37],
        [86, 40, 35, 87]
    ],
        u2 = [1, 0, 0, 0],
        e = 10 ** (-4),
        l2 = 1,
        u1, l1

    do {
        u1 = u2
        u2 = matrix_multiply(a, u2)
        l1 = l2
        l2 = vector_length(u2) / vector_length(u1)
        u2 = normolize_vector(u2)
    } while (Math.abs(l2 - l1) > e)

    console.log(`l = ${l2.toFixed(3)}\nvector=[`)
    for (let i = 0; i < u2.length; i++) {
        console.log(`        ${u2[i].toFixed(3)},`)
    }
    console.log('       ]');

}

function matrix_multiply(a, b) {
    let c = []
    for (let i = 0; i < a.length; i++) {
        c[i] = 0
        for (let j = 0; j < a[i].length; j++) {
            c[i] += a[i][j] * b[j]
        }
    }
    return c
}

function vector_length(u) {
    let sum = 0
    for (let i = 0; i < u.length; i++) {
        sum += u[i] ** 2
    }
    sum = Math.sqrt(sum)
    return sum
}

function normolize_vector(u) {
    let s = vector_length(u)
    for (let i = 0; i < u.length; i++) {
        u[i] = u[i] / s
    }
    return u
}

main()
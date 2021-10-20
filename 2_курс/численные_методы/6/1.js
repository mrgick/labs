function main() {
    let a, b
    a = [
        [28, 9, 70, 43],
        [7, 28, 43, 3],
        [75, 98, 76, 37],
        [86, 40, 35, 87]
    ]
    b = [64, 54, 65, 33]

    const n = 4

    for (let s = 0; s < n - 1; s++) {

        for (let j = s + 1; j < n; j++) {
            a[s][j] = a[s][j] / a[s][s]
        }

        b[s] = b[s] / a[s][s]

        for (let i = s + 1; i < n; i++) {
            for (let j = s + 1; j < n; j++) {
                a[i][j] = a[i][j] - a[i][s] * a[s][j]
            }
            b[i] = b[i] - b[s] * a[i][s]
        }
    }

    let x = [0, 0, 0, 0]

    x[n - 1] = b[n - 1] / a[n - 1][n - 1]

    for (let s = n - 2; s >= 0; s--) {

        let sum = 0
        for (let k = s + 1; k < n; k++) {
            sum += a[s][k] * x[k]
        }

        x[s] = b[s] - sum
    }

    for (let i = 0; i < n; i++) {
        console.log(`X${i + 1} = ${x[i].toFixed(5)}`)
    }
}
main()
//метод Гаусса
function gaus(a, b, n) {

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

    let x = []

    x[n - 1] = b[n - 1] / a[n - 1][n - 1]

    for (let s = n - 2; s >= 0; s--) {

        let sum = 0
        for (let k = s + 1; k < n; k++) {
            sum += a[s][k] * x[k]
        }

        x[s] = b[s] - sum
    }
    return x
}

//вычисление полинома
function calculate_P(o, x, m) {
    let p = [], sum
    for (let i = 0; i < 8; i++) {
        sum = 0
        for (let j = 0; j < (m + 1); j++) {
            sum += o[j] * Math.pow(x[i], j)
        }
        p[i] = sum
    }
    return p

}

// вычисление и вывод отклонения
function calculate_sg(y_new, y_old) {
    let sg = []
    for (i = 0; i < 8; i++) {
        sg[i] = y_old[i] - y_new[i]
        console.log(`i=${i} P=${y_new[i].toFixed(3)} sg=${sg[i].toFixed(3)}`)
    }
}


function main() {

    let x = [0.77, 0.9, 0.94, 0.96, 0.99, 1, 1.17, 1.3, 3],
        y = [11.6, 7.3, 3.1, 1.7, 0.3, -0.1, -2.6, 0.5, 3],
        a = [],
        b = [],
        m = 4,
        sum

    for (let i = 1; i <= m + 1; ++i) {
        a[i - 1] = []
        for (j = 1; j <= m + 1; ++j) {
            sum = 0
            for (k = 0; k < 8; ++k) {
                sum += Math.pow(x[k], (i + j - 2))
            }
            a[i - 1][j - 1] = sum
        }
    }

    for (i = 1; i <= m + 1; ++i) {
        sum = 0
        for (k = 0; k < 8; ++k) {
            sum += y[k] * (Math.pow(x[k], i - 1))
        }
        b[i - 1] = sum
    }

    let o = gaus(a, b, 5)
    let Y = calculate_P(o, x, m)
    calculate_sg(Y, y)
}

main()
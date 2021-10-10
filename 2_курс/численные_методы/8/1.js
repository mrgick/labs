function main() {
    let x = [5, 6, 8, 11, 13, 16, 20, 23],
        y = [46, 42, 38, 72, 61, 33, 24, 0],
        x_min = 6,
        x_max = 22,
        dx = 1,
        n = x.length,
        x_tmp


    while (x_min <= x_max) {
        x_tmp = calc_L(x_min, x, y, n)
        console.log(`x=${x_min}, f(x)=${x_tmp.toFixed(3)}`);
        x_min += dx
    }

}

function calc_L(x_tmp, x, y, n) {
    let sum = 0, multiply
    for (let i = 0; i < n; i++) {
        multiply = 1
        for (let j = 0; j < n; j++) {
            if (j != i) {
                multiply *= (x_tmp - x[j]) / (x[i] - x[j])
            }
        }
        sum += y[i] * multiply
    }
    return sum
}

main()
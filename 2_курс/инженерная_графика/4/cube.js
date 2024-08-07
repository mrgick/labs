let scale = 1000 / dist
//colours = ['#FFFFFF', '#FFFFFF', '#FFFFFF', '#FFFFFF', '#FFFFFF', '#FFFFFF']

let figure =
    [
        [[0, 0, 0], [1, 0, 0], [0, 1, 0]],
        [[0, 0, 0], [0, 1, 0], [0, 0, 1]],
        [[0, 0, 0], [0, 0, 1], [1, 0, 0]],
        [[1, 0, 0], [0, 1, 0], [0, 0, 1]]
        /*
            [[0, 0, 0], [1, 0, 0], [0, 1, 0], [0, 0, 0]],
            [[0, 0, 0], [1, 0, 0], [0, 1, 0], [0, 0, 0]],
            [[0, 0, 0], [1, 0, 0], [0, 1, 0], [0, 0, 0]],
            [[0, 0, 0], [1, 0, 0], [0, 1, 0], [0, 0, 0]],
            [[0, 0, 0], [1, 0, 0], [0, 1, 0], [0, 0, 0]],
            [[0, 0, 0], [1, 0, 0], [0, 1, 0], [0, 0, 0]],
            [[0, 0, 0], [1, 0, 0], [0, 1, 0], [0, 0, 0]],
            [[0, 0, 0], [1, 0, 0], [0, 1, 0], [0, 0, 0]], */
    ]

let vector = [0, 1, 0]

let cube = [
    [[0, 0, 0], [1, 0, 0], [1, 1, 0], [0, 1, 0]],
    [[0, 0, 0], [0, 1, 0], [0, 1, 1], [0, 0, 1]],
    [[0, 0, 0], [0, 0, 1], [1, 0, 1], [1, 0, 0]],
    [[1, 1, 1], [0, 1, 1], [0, 0, 1], [1, 0, 1]],
    [[1, 1, 1], [1, 1, 0], [0, 1, 0], [0, 1, 1]],
    [[1, 1, 1], [1, 0, 1], [1, 0, 0], [1, 1, 0]]
    
]

cube = [
    [[-1, -1, -1], [1, -1, -1], [1, 1, -1], [-1, 1, -1]],
    [[-1, -1, -1], [-1, 1, -1], [-1, 1, 1], [-1, -1, 1]],
    [[-1, -1, -1], [-1, -1, 1], [1, -1, 1], [1, -1, -1]],
    [[1, 1, 1], [-1, 1, 1], [-1, -1, 1], [1, -1, 1]],
    [[1, 1, 1], [1, 1, -1], [-1, 1, -1], [-1, 1, 1]],
    [[1, 1, 1], [1, -1, 1], [1, -1, -1], [1, 1, -1]]
    
]

let angle = 1

function rotate_cube() {
    // Подробнее:
    // https://en.wikipedia.org/wiki/Rotation_matrix#Rotation_matrix_from_axis_and_angle
    function create_migrate_matrix(angle, axis) {
        let theta = angle * Math.PI / 180
        let mod = (axis[0] ** 2 + axis[1] ** 2 + axis[2] ** 2) ** 0.5
        let x = axis[0] / mod,
            y = axis[1] / mod,
            z = axis[2] / mod
        let c = Math.cos(theta),
            s = Math.sin(theta),
            t = 1 - c

        //console.log(x,y,z,c,s,t);

        let M = [
            [t * x * x + c, t * x * y - s * z, t * x * z + s * y],
            [t * x * y + s * z, t * y * y + c, t * y * z - s * x],
            [t * x * z - s * y, t * y * z + s * x, t * z * z + c]
        ]
        return M
    }

    // Умножение вектора точки на матрицу M
    function rotate_dot(dot, M) {
        let new_dot = dot.slice()
        for (let i = 0; i < 3; i++) {
            new_dot[i] = 0
            for (let j = 0; j < 3; ++j) {
                new_dot[i] += dot[j] * M[i][j]
            }
        }
        return new_dot
    }


    let axis = vector.slice()
    if (parseInt(axis[0]) == 0 &&
        parseInt(axis[1]) == 0 &&
        parseInt(axis[2]) == 0 ) {
        return
    }

    let M = create_migrate_matrix(angle, axis)

    let dot
    for (let i = 0; i < cube.length; ++i) {
        for (let j = 0; j < cube[i].length; ++j) {
            dot = cube[i][j].slice()
            cube[i][j] = rotate_dot(dot, M)
        }
    }
}

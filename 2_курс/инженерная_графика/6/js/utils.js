function F(x, z) {
    let R = (x * x + z * z) ** (0.5)
    return 8 * Math.cos(1.2 * R) / (R + 1)
}

function degrees_to_radians(degrees) {
    return degrees * (Math.PI / 180);
}


function rotate_dot(x, y, z, x_angle=25, y_angle=15) {
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


    let axis, angle, M, dot

    // поворот вокруг оси х
    axis = [1, 0, 0]
    angle = x_angle
    M = create_migrate_matrix(angle, axis)
    dot = rotate_dot([x, y, z], M)
    
    // поворот вокруг оси у
    axis = [0, 1, 0]
    angle = y_angle
    M = create_migrate_matrix(angle, axis)
    dot = rotate_dot(dot, M)

    return dot
}

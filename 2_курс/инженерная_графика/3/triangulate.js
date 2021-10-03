function Point(x, y, c) {
    this.x = x
    this.y = y
    this.c = c
}

function angle(a, b, c) {
    let temp = Math.atan2((c.y - b.y), (c.x - b.x)) - Math.atan2((a.y - b.y), (a.x - b.x))
    return (temp < 0 ? Math.PI * 2 + temp : temp)
}

function Triangle(a, b, c) {
    this.a = a
    this.b = b
    this.c = c
    this.area = function () {
        return (a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y)) / 2
    }
}

function measureAngles(points) {
    for (let i = 0; i < points.length + 1; i++) {
        points[(i + 1) % points.length].angle = angle(points[i % points.length],
            points[(i + 1) % points.length],
            points[(i + 2) % points.length]) * 180 / Math.PI
    }

}

function isInsideTriangle(triangle, point) {
    let areaA, areaB, areaC
    areaA = (new Triangle(triangle.a, triangle.b, point)).area()
    areaB = (new Triangle(triangle.b, triangle.c, point)).area()
    areaC = (new Triangle(triangle.c, triangle.a, point)).area()
    return (Math.sign(areaA) == Math.sign(areaB) &&
        Math.sign(areaA) == Math.sign(areaC))
}

function triangulate(points) {

    if (points < 3) return

    if (angle(points[0], points[1], points[2]) > Math.PI) {
        points.reverse()
    }

    let i = 1
    let a, b, c
    let triangles = []

    measureAngles(points)
    while (points.length > 3) {
        a = points[i % points.length]
        b = points[(i + 1) % points.length]
        c = points[(i + 2) % points.length]

        if (angle(a, b, c) < Math.PI) {
            let tempTriangle = new Triangle(a, b, c)
            let isEar = true
            for (let j = i + 3; j < points.length + i + 3; j++) {
                if (isInsideTriangle(tempTriangle, points[j % points.length]))
                    isEar = false
            }
            if (isEar) {
                points.splice((i + 1) % points.length, 1)
                triangles.push(tempTriangle)
            }
        }
        i++
    }
    triangles.push(new Triangle(points[0], points[1], points[2]))
    console.log(triangles)
    drawTriangles(triangles)

}

function drawTriangles(triangles) {
    for (let i = 0; i < triangles.length; i++) {
        drawTriangle(triangles[i])
    }
}

function drawTriangle(triangle) {
    p1 = triangle.a
    p2 = triangle.b
    p3 = triangle.c
    ctx.fillStyle = getRandomColor()
    ctx.strokeStyle = line_color 
    ctx.beginPath()
    ctx.moveTo(p1.x, p1.y)
    ctx.lineTo(p2.x, p2.y)
    ctx.lineTo(p3.x, p3.y)
    ctx.closePath()
    ctx.fill()
    ctx.stroke()
}

function getRandomColor() {
    let letters = 'BCDEF'
    let color = '#'
    for (let i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 5)]
    }
    return color
}

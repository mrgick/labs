let all_points, points1, points2, points3, points4, points5, points6, points7, points8

let examle_number = 0
function get_example() {
    console.log(examle_number);
    if (examle_number > all_points.length - 1) {
        examle_number = 0
    }
    let res = all_points[examle_number]
    examle_number++
    return res.slice()
}

points1 = [
    { x: 270, y: 300 },
    { x: 270, y: 200 },
    { x: 400, y: 160 },
    { x: 500, y: 230 },
    { x: 380, y: 370 },
    { x: 420, y: 260 },
    { x: 360, y: 280 },
    { x: 360, y: 210 }
]

points2 = [
    { x: 280, y: 340 },
    { x: 200, y: 270 },
    { x: 270, y: 80 },
    { x: 480, y: 70 },
    { x: 550, y: 210 },
    { x: 490, y: 310 }
]

points3 = [
    { x: 210, y: 360 },
    { x: 160, y: 220 },
    { x: 320, y: 90 },
    { x: 270, y: 210 },
    { x: 430, y: 180 },
    { x: 480, y: 400 }
]

points4 = [
    { x: 140, y: 320 },
    { x: 130, y: 100 },
    { x: 360, y: 50 },
    { x: 210, y: 210 },
    { x: 420, y: 280 },
    { x: 480, y: 410 },
    { x: 400, y: 300 },
    { x: 390, y: 380 }
]

points5 = [
    { x: 210, y: 380 },
    { x: 150, y: 100 },
    { x: 290, y: 200 },
    { x: 600, y: 170 },
    { x: 470, y: 390 },
    { x: 350, y: 270 }
]

points6 = [
    { x: 190, y: 400 },
    { x: 120, y: 160 },
    { x: 200, y: 210 },
    { x: 310, y: 90 },
    { x: 460, y: 90 },
    { x: 290, y: 310 },
    { x: 590, y: 120 },
    { x: 550, y: 400 }
]

points7 = [
    { x: 220, y: 410 },
    { x: 120, y: 110 },
    { x: 480, y: 60 },
    { x: 320, y: 200 },
    { x: 560, y: 190 },
    { x: 390, y: 360 },
    { x: 600, y: 440 }
]

points8 = [
    { x: 270, y: 430 },
    { x: 170, y: 340 },
    { x: 400, y: 60 },
    { x: 290, y: 240 },
    { x: 490, y: 200 },
    { x: 580, y: 390 },
    { x: 400, y: 420 }
]

all_points = [points1, points2, points3, points4, points5, points6, points7, points8]
    
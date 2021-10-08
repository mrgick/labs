import math


def f(x):
    """
        10ln(x) + 0.2x^2 - 4.5x + 1 = 0
    """
    return 10 * math.log(x) + 0.2 * pow(x, 2) - 4.5 * x + 1


def half_divide(a, b, ε):
    while b - a > ε:
        x = (a + b) / 2
        if f(a) * f(x) <= 0:
            b = x
        else:
            a = x
    return x


def main():
    ε = 10**(-3)
    a = 1
    b = 18
    step = (b - a) / 20
    while a < b:
        if f(a) * f(a + step) <= 0:
            print(f"a={a:.{3}f} b={a+step:.{3}f}")
            x = half_divide(a, a + step, ε)
            print(f"x={x:.{3}f}")
        a += step


if __name__ == "__main__":
    main()
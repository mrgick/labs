import math


def φ(x):
    return x + 2*(math.log(x)-x+1.8)


def main():
    x = 2
    ε = 10**(-5)

    while True:
        Δ = φ(x)
        if abs(x-Δ) < ε:
            break
        x = Δ
        print(x, Δ)


if __name__ == "__main__":
    main()

#include "stdio.h"

int fib(int n)
{
    int f1 = 0;
    int f2 = 1;
    int fn;
    if (n < 1)
    {
        return 0;
    }
    if (n == 1)
    {
        return 1;
    }
    if (n > 1)
    {
        for (int j = 1; j < n; j++)
        {
            fn = f1 + f2;
            f1 = f2;
            f2 = fn;
        }
    }
    return fn;
}

int fib_rec(int n)
{
    switch (n)
    {
    case 0:
        return 0;
    case 1:
        return 1;
    default:
        return fib_rec(n - 1) + fib_rec(n - 2);
    }
}

void cout_max_fib_n_in_int()
{
    // p.s. max int is 2147483647
    int f1 = 0,
        f2 = 1,
        fn = 1,
        n = 1;
    while (fn > 0)
    {
        fn = f1 + f2;
        f1 = f2;
        f2 = fn;
        n++;
        //printf("%d %d\n", fn, n);
    }
    n -= 1;
    printf("Max fibonacci N = %d\nfibonacci(N) = %d\n", n, f1);
}

int main()
{
    int N;
    scanf("%d", &N);
    printf("fib_rec(N) = %d\n", fib_rec(N));
    printf("fib(N) = %d\n", fib(N));
    printf("Finding max fibonacci N:\n");
    cout_max_fib_n_in_int();
    return 0;
}

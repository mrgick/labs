#include <stdio.h>
#include <cmath>

int main() {
    double x;
	int n;
	long double xn;
	printf("Enter x and n separated by space or enter.\n");
    scanf("%lf %d", &x, &n);
    xn = pow(x, n);
	printf("x^n = %llf\n", xn);
    return 0;
}
#include <stdio.h>
#include <cmath>

int main() {
    double a, e, xn, xn1 = 1;
    printf("Enter a and ε separated by the space or enter.\n");
	scanf("%lf %lf", &a, &e);
		
	do {
		xn = xn1;
		xn1 = (xn + a/xn)/2;
	} while (abs(xn1-xn) >= e);

	printf("sqrt(a) = %lf | Heron's formula: %lf\n", sqrt(a), xn1);
    return 0;
}
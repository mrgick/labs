#include "stdio.h"
#include <cmath>

int main(){
    double a, b, c, x1, x2, D;
    printf("Enter 3 coefficients separated by spaces or enters.\n");
    scanf("%lf %lf %lf", &a, &b, &c);

    if (a == 0){
		x1 = b/(-c);
		printf("x = %lf\n", x1);
		return 0;
	}
    
	D = b*b-4*a*c;
	if (D < 0) {
		printf("There are no roots in the equation.\n");
	}
    else {
        x1 = (-b-sqrt(D))/(2*a);
        x2 = (-b+sqrt(D))/(2*a);
        printf("x1 = %.4lf, x2 = %.4lf\n", x1, x2);
    }
    return 0;
}
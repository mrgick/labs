#include <stdio.h>
#include <functional>
#include <cmath>

long double fact(double N){
	if (N < 0) {
        return 0;
    }
	else if (N == 0) {
        return 1;
    }
	else {
        return N*fact(N-1);
    }
}

double exp_in_minus_x(double x, double n){
    return pow(-x,n)/fact(n);
}

double count(double xn, double e,
             std::function<double(double, double)> func){
    double sum = 0;
    double n = 0;
    while((abs(func(xn,n)) > e)) {
        sum += func(xn,n);
        n++;
    }
    return sum;
}

int main() {
    double xn, xk, dx, e;
    long double res;
    printf("Enter xn, xk, dx, ε:\n");
	//xn = -10; xk = 5; dx = 1; e = 0.000001;
    scanf("%lf %lf %lf %lf", &xn, &xk, &dx, &e);
	printf("----------------------------------\n");
    printf("|    x     |   e^-x   |   f(x)   |\n");
    printf("----------------------------------\n");
    while (xn <= xk) {
        //x
        printf("|%10.4lf", xn);
        //e^-x
        printf("|%10.4lf|%10.4lf", 
               exp(-xn), count(xn, e, &exp_in_minus_x));
        printf("|\n");
        xn += dx;   
    }
    printf("----------------------------------\n");
    return 0;
}
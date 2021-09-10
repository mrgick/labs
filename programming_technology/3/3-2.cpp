#include <stdio.h>
#include <math.h>

int main() {

    double y;

	printf("┌───┬──────────┐\n");
    printf("│ x │y = sin(x)│\n");
    printf("├───┼──────────┤\n");
	for (int x = 0; x <= 180; x += 15){
		y = x*M_PI/180;
		printf("│%3d│%.8lf│\n", x, sin(y));
	}
    printf("└───┴──────────┘\n");
    return 0;
}
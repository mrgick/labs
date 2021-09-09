#include <stdio.h>
#include <math.h>

int main() {

    double c;

	printf("┌───┬──────────┐\n");
    printf("│ F │    C     │\n");
    printf("├───┼──────────┤\n");
	for (int f = 0; f <= 300; f += 20){
		c = (5.0/9.0) * (f-32);
		printf("│%3d│%10.6lf│\n", f, c);
	}
    printf("└───┴──────────┘\n");
    
    return 0;
}
/* help for you.
line equation from two points:
https://math.semestr.ru/line/equation.php
*/

#include "stdio.h"

int main(){
    double x, y;
    printf("Enter x:\n");
    scanf("%lf", &x);
    if (x <= -2) {
        y = 0;
    }
    else if (x <= -1) {
        y = -x - 2;
    }
    else if (x <= 1){
        y = x;
    }
    else if (x <= 2){
        y = -x + 2;
    }
    else {
        y = 0;
    }
    printf("y=%.2lf\n",y);
    return 0;
}

#include "stdio.h"

struct Point {
    int x;
    int y;
};

Point enter_point(const char* str){
    int x, y;
    printf("%s:\nx=", str);
    scanf("%d", &x);
    printf("y=");
    scanf("%d", &y);
    return {x, y};
}

void print_point(Point p, const char* str){
    printf("Point(%d, %d) - %s\n", p.x, p.y, str);
}

int main(){
    
    Point a, b, p;

    printf("Coordinates of the rectangle.\n");
    a = enter_point("First corner");
    b = enter_point("Second corner");
    p = enter_point("Arbitrary point coordinates");

    printf("\n");
    print_point(a,"First corner");
    print_point(b, "Second corner");
    print_point(p, "Arbitrary point coordinates");

    if ( (p.x>=a.x && p.x<=b.x && p.y<=a.y && p.y>=b.y) ||
         (p.x>=b.x && p.x<=a.x && p.y<=b.y && p.y>=a.y) ){
        printf("The point belong to a rectangle.\n");
    }
    else {
        printf("The point does not belong to a rectangle.\n");
    }

    return 0;
}

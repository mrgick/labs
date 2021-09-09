#include <iostream>
using namespace std;

int enter_number(const char* str){
    int num;
    cout << "Enter " << str << ":" << endl;
    cin >> num;
    return num;
}

void print_point(const char* str, int x, int y){
    cout << str << " (" << x << ", " << y << ")\n";
}

int main(){
    int x1,y1,x2,y2,x,y;
    cout << "Coordinates of the rectangle.\n";
    cout << "Left upper corner.\n";
    x1 = enter_number("x1");
    y1 = enter_number("y1");
    cout << "Right lower corner.\n";
    x2 = enter_number("x2");
    y2 = enter_number("y2");
    cout << "Point coordinates.\n";
    x = enter_number("x");
    y = enter_number("y");

    print_point("Left upper corner:", x1, y1);
    print_point("Right lower corner:", x2, y2);
    print_point("Point coordinates:", x, y);

    if (x>=x1 && x<=x2 && y<=y1 && y>=y2){
        cout << "The point belong to a rectangle.\n";
    }
    else {
        cout << "The point does not belong to a rectangle.\n";
    }
    return 0;
}
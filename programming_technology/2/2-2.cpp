#include <iostream>
using namespace std;

int main(){
    int year;
    cout << "Enter year (positive number), please:\n";
    cin >> year;
    if (year < 0) {
        cout << "It is a negative number!";
    }
    else if (year%400==0 || year%4==0 && year%100!=0) {
        cout << "It is leap year!";
    }
    else {
        cout << "It is not leap year...";
    }

    cout << endl;

    return 0;
}
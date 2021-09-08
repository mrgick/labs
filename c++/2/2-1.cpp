#include <iostream>

using namespace std;

int main()
{
    double a, b, c;
    cout << "Enter three positive numbers:\n";
    cin >> a;
    cin >> b;
    cin >> c;

    if (a==0 || b==0 || c==0){
		cout << "false";
		return 0;
	}

    if (a<0 || b<0 || c<0){
		cout << "Negative!";
		return 0;
	}

    if (a<=b+c && b<=a+c && c<=a+b) {
        cout << "true\n";
    }
    else {
        cout << "false\n";
    }

    cin.get();
    return 0;
}
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
		cout << "false\n";
		return 0;
	}

    if (a<0 || b<0 || c<0){
		cout << "Negative!\n";
		return 0;
	}

    if (a<=b+c && b<=a+c && c<=a+b) {
        cout << "true\n";
    }
    else {
        cout << "false\n";
    }

    cin.ignore().get();
    return 0;
}

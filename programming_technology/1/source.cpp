#include <iostream>
#include <clocale>

using namespace std;

int main()
{
    setlocale(LC_ALL, ".1251");
    cout << "Привет мир!\n";
    cin.get();
    return 0;
}
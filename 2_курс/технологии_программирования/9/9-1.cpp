#include <iostream>
#include <fstream>
#include <string.h>
/*
Использую проверку внутри, чтобы не было скопировано два
раза последнее значение. Подробнее: https://vk.cc/c65kwo
*/

int main()
{
    char filename[30];
    char extension[5] = ".out";
    std::cin >> filename;
    std::ifstream f1("test1.txt");
    std::ofstream f2(strcat(filename, extension));
    char tmp;
    while (true)
    {
        f1.get(tmp);
        if (f1.eof()) break;
        f2 << tmp;
    }
    f1.close();
    f2.close();
    return 0;
}

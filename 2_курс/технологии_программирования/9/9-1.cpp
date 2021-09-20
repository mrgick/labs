#include <iostream>
#include <fstream> 

/*
Использую getline, чтобы не было скопировано два раза
последнее значение. Подробнее: https://vk.cc/c65kwo
*/

int main()
{
    char filename[30];
	std::cin >> filename;
    std::ifstream f1("test1.txt");
    std::ofstream f2(filename);
    char input_line[81];
	while (f1.getline(input_line, 80)) {
		f2 << input_line << std::endl;
	}
    f1.close();
	f2.close();
    return 0;
}

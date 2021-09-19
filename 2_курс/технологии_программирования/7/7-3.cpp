#include <iostream>
#include <algorithm>
#include <string>

bool is_space(int j)
{
    return j == ' ';
}

int main()
{

    setlocale(LC_ALL, "ru_RU.UTF-8");

    std::wstring text;
    std::getline(std::wcin, text);

    std::cout << "Words:" << 1 + count_if(text.begin(), text.end(), is_space) << "\n";

    return 0;
}
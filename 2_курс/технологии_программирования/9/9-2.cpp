#include <iostream>
#include <fstream>

int main()
{
    std::ifstream f1("test1.txt");
    std::ofstream f2("test2.out");
    std::string tmp, word;
    getline(std::cin, word);

    while (true)
    {
        getline(f1, tmp);
        if (f1.eof()) break;
        if (tmp.find(word) != std::string::npos)
        {
            f2 << tmp << std::endl;
        }
    }

    f1.close();
    f2.close();
    return 0;
}

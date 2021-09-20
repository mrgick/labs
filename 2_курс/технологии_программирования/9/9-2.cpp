#include <iostream>
#include <fstream>

int main()
{
    std::ifstream f1("test1.txt");
    std::ofstream f2("test2.txt");
    std::string tmp, word;
    getline(std::cin, word);

    while (!f1.eof())
    {
        getline(f1, tmp);
        if (tmp.find(word) != std::string::npos)
        {
            f2 << tmp << std::endl;
        }
    }

    f1.close();
    f2.close();
    return 0;
}

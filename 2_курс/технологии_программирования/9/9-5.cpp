#include <iostream>
#include <fstream>

int main()
{
    std::ifstream f1;
    std::ofstream f2;
    f1.open("test1.txt", std::ios::binary);
    f2.open("test2.txt", std::ios::binary);
    char tmp1, tmp2;
    bool tmp2_is_empty = true;

    while (true)
    {
        f1.get(tmp1);
        if (f1.eof())
        {
            break;
        }
        if (tmp2_is_empty == true)
        {
            tmp2 = tmp1;
            tmp2_is_empty = false;
            f2 << tmp1;
            continue;
        }

        if (tmp1 == ' ' and tmp2 == ' ')
        {
            tmp2 = ' ';
            continue;
        }
        else if (tmp1 != ' ' and tmp2 == ' ')
        {
            f2 << tmp1;
        }
        else if (tmp1 == '\t')
        {
            f2 << ' ';
        }
        else
        {
            f2 << tmp1;
        }

        //std::cout << tmp2 << tmp1;
        tmp2 = tmp1;
    }

    f1.close();
    f2.close();

    return 0;
}

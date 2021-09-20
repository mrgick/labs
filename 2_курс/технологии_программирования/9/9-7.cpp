#include <fstream>
#include <iostream>

struct word
{
    std::string self;
    std::string other;
};

word find_next_word(std::ifstream &file)
{
    std::string word = std::string(),
                other = std::string();
    char tmp;
    while (true)
    {
        if (file.eof())
        {
            break;
        }

        file.get(tmp);

        if (isalpha(tmp) or isdigit(tmp) or tmp == '\'' or tmp == '-')
        {
            word += tmp;
        }
        else
        {
            other += tmp;
            if (word != std::string())
            {
                break;
            }
        }
    }
    return {word, other};
}

int main()
{
    
    std::ifstream f1;
    std::ifstream f2;
    std::ofstream f3;

    f1.open("test1.txt", std::ios::binary);
    f2.open("test2.txt", std::ios::binary);
    f3.open("test2.out");

    word word1, word2;

    while (true)
    {
        word1 = find_next_word(f1);
        word2 = find_next_word(f2);
        f3 << word1.self << ' ' << word2.self << '\n';
        if (word1.self == std::string() and word2.self == std::string())
            break;
    }

    f1.close();
    f2.close();
    f3.close();
    return 0;
}

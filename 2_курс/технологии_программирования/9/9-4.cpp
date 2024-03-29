#include <fstream>

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
    std::ofstream f2;
    f1.open("test1.txt", std::ios::binary);
    f2.open("test2.out", std::ios::binary);

    word word1, word2;

    while (!f1.eof())
    {
        word1 = find_next_word(f1);
        word2 = find_next_word(f1);
        f2 << word2.self + word1.other + word1.self + word2.other;
    }

    f1.close();
    f2.close();

    return 0;
}

#include <iostream>
#include <fstream>

std::string find_next_word(std::ifstream &file)
{
    std::string word = std::string();
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
        else if (word != std::string())
        {
            break;
        }
    }
    return word;
}

int main()
{
    std::ifstream f1;
    f1.open("test1.txt", std::ios::binary);

    std::string word, longest = std::string();

    while (!f1.eof())
    {
        word = find_next_word(f1);
        if (word.length() > longest.length())
        {
            longest = word;
        }
    }

    f1.clear();
    f1.seekg(0, std::ios::beg);
    int counter = 0;

    while (!f1.eof())
    {
        word = find_next_word(f1);
        if (word == longest)
        {
            counter++;
        }
    }

    f1.close();
    std::cout << "The longest is " 
              << longest 
              << ", counter = " 
              << counter 
              << std::endl;
    return 0;
}

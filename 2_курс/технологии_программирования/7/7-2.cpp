#include <iostream>
#include <algorithm>
#include <string>
#include <cctype>

/*
Русский алфавит:
абвгдеёжзийклмнопрстуфхцчшщъыьэюя
АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ
*/

bool is_char_in_str(int j, std::wstring rus)
{
    for (int q = 0; q < rus.length(); q++)
    {
        if (j == rus[q])
        {
            return true;
        }
    }
    return false;
}

int main()
{

    std::wstring vowels = L"аеёиоуыэюяАЕЁИОУЫЭЮЯ",
                 consonant = L"бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ",
                 lower = L"абвгдеёжзийклмнопрстуфхцчшщъыьэюя",
                 upper = L"АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    
    setlocale(LC_ALL, "ru_RU.UTF-8");
    
    std::wstring text;
    std::getline(std::wcin, text);

    //std::cout << text.length() << std::endl;

    int count_vowels = 0,
        count_consonant = 0,
        count_upper = 0,
        count_lower = 0;
    for (int i = 0; i < text.length(); i++)
    {
        if (is_char_in_str(text[i], vowels) == true)
        {
            count_vowels++;
        }
        else if (is_char_in_str(text[i], consonant) == true)
        {
            count_consonant++;
        }
        if (is_char_in_str(text[i], lower) == true)
        {
            count_lower++;
        }
        else if (is_char_in_str(text[i], upper) == true)
        {
            count_upper++;
        }

        //std::cout << text[i] << std::endl;
    }

    std::cout << "Vowels:" << count_vowels << "\n";
    std::cout << "Consonant:" << count_consonant << "\n";
    std::cout << "Upper:" << count_upper << "\n";
    std::cout << "Lower:" << count_lower << "\n";

    return 0;
}

#include <iostream>
#include <algorithm>
#include <string>
#include <cctype>

/*
English alphabet:
abcdefghijklmnopqrstuvwxyz
ABCDEFGHIJKLMNOPQRSTUVWXYZ
*/

bool is_vowel(int j)
{
    std::string vowels = "aeouiAEOUI";
    return vowels.find(j) != std::string::npos;
}

bool is_consonant(int j)
{
    std::string consonant = "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ";
    return consonant.find(j) != std::string::npos;
}

int main()
{
    std::string text;
    std::getline(std::cin, text);
    std::cout << "Vowels:" << count_if(text.begin(), text.end(), is_vowel) << "\n";
    std::cout << "Consonant:" << count_if(text.begin(), text.end(), is_consonant) << "\n";
    std::cout << "Upper:" << count_if(text.begin(), text.end(), isupper) << "\n";
    std::cout << "Lower:" << count_if(text.begin(), text.end(), islower) << "\n";
    return 0;
}

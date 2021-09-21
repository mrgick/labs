// Частотный словарь (с использованием vector и string)
// Преимущества: список и строка не могут переполниться!
#include <algorithm>
#include <iostream>
#include <string>
#include <vector>
using namespace std;

const int NOTFOUND = -1;

struct Elem
{               // Описание структуры из 2-х компонент:
    string str; // строка для хранения слова
    int cnt;    // счетчик кол-ва появлений слова
};

vector<Elem> list; // Динамический массив структур

// Функция сравнения двух элементов структур
bool cmp_by_cnt(const Elem &lh, const Elem &rh)
{
    if (lh.cnt == rh.cnt)
    {
        vector<string> Sort_Items;
        Sort_Items.push_back(lh.str);
        Sort_Items.push_back(rh.str);
        sort(Sort_Items.begin(), Sort_Items.end());
        if (Sort_Items[0] == lh.str)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    else
    {
        return lh.cnt < rh.cnt;
    }
}
// Получить очередное слово с клавиатуры и записать в str
// Возвращает true, если это слово не "quit"
// (string& - строка передаётся по ссылке!)
bool GetWord(string &str)
{
    cin >> str;
    return str != "quit";
}

// Поиск слова Str в массиве List
// Возвращает индекс найденного элемента или NOTFOUND
// (const string& - строку нельзя модифицировать)
int Search(const string &str)
{
    for (int i = 0; i < list.size(); ++i)
    {
        if (str == list[i].str)
            return i;
    }
    return NOTFOUND;
}
void PrintList()
{
    for (int i = 0; i < list.size(); ++i)
        cout << "\nСлово <" << list[i].str << "> встретилось " << list[i].cnt << " раз";
    cout << endl;
}

int main()
{
    string s; // Буфер для хранения очередного слова
    while (GetWord(s))
    {
        int pos = Search(s);
        if (pos != NOTFOUND)
        { // Слово уже встречалось
            list[pos].cnt++;
        }
        else
        {
            Elem tmp;
            tmp.str = s;
            tmp.cnt = 1;
            list.push_back(tmp);
        }
    }
    sort(list.begin(), list.end(), cmp_by_cnt);
    PrintList();
    return 0;
}
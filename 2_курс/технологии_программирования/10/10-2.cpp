#include <algorithm>
#include <string>
#include <vector>
#include <fstream>
#include <iostream>
#include <iomanip>

struct PRICE
{
    std::string name;
    std::string shop;
    int cost;
};

void load_from_file(std::vector<PRICE> &list, const char *file_name)
{
    std::ifstream file(file_name, std::ios::binary);
    file.clear();
    file.seekg(0, std::ios::beg);
    std::vector<std::string> line;
    std::string elem;
    char tmp;
    while (true)
    {
        tmp = file.get();
        if (file.eof())
            break;

        if (tmp == ':')
        {
            line.push_back(elem);
            elem = std::string();
        }
        else if (tmp == '\n')
        {
            PRICE p_tmp;
            p_tmp.name = line[0];
            p_tmp.shop = line[1];
            p_tmp.cost = std::stoi(elem);
            list.push_back(p_tmp);
            elem = std::string();
            line.clear();
        }
        else
        {
            elem += tmp;
        }
    }
    file.close();
}

bool sort_by_name(const PRICE left, const PRICE right)
{
    std::vector<std::string> sort_items;
    sort_items.push_back(left.name);
    sort_items.push_back(right.name);
    sort(sort_items.begin(), sort_items.end());
    if (sort_items[0] == left.name)
    {
        return true;
    }
    else
    {
        return false;
    }
}

template <typename T>
void print_element(T t, const int &width, char d, const char separator = ' ')
{
    std::cout << std::left << std::setw(width) << std::setfill(separator) << t;
    std::cout << d;
}

template <typename C>
void print_price_element(std::string name, std::string shop, C cost,
                         int w_n = 21, int w_s = 13, int w_c = 6)
{
    std::cout << "|";
    print_element(name, w_n, '|');
    print_element(shop, w_s, '|');
    print_element(cost, w_c, '|');
    std::cout << "\n";
}

void print_hor_sep(int width = 44)
{
    print_element('-', width, '\n', '-');
}

void print_list(std::vector<PRICE> &list, const char *table_name, int bigger = 0)
{
    print_element(' ', 18, ' ');
    std::cout << table_name << "\n";
    print_hor_sep();
    print_price_element("NAME", "SHOP", "SHOP");
    print_hor_sep();
    for (int i = 0; i < list.size(); ++i)
    {
        if (bigger == 0 or list[i].cost > bigger)
        {
            print_price_element(list[i].name, list[i].shop, list[i].cost);
        }
    }
    print_hor_sep();
    std::cout << "\n";
}

int main()
{
    std::vector<PRICE> list;
    const char file_prices[] = "prices.txt";
    load_from_file(list, file_prices);
    print_list(list, "TABLE");
    sort(list.begin(), list.end(), sort_by_name);
    print_list(list, "SORTED");
    print_list(list, "BIGGER", 1000);
    return 0;
}

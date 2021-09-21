#include <algorithm>
#include <string>
#include <vector>
#include <fstream>
#include <iostream>
#include <iomanip>

struct STUDENT
{
    std::string name;
    std::string group;
    std::vector<int> log;
};

void load_from_file(std::vector<STUDENT> &list, const char *file_name)
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
            STUDENT p_tmp;
            p_tmp.name = line[0];
            p_tmp.group = line[1];
            for (int i = 0; i < elem.size(); i++)
            {
                if (elem[i] != ',')
                {
                    p_tmp.log.push_back(int(elem[i]) - 48);
                }
            }

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

double get_average(std::vector<int> marks)
{
    double sum;
    for (int i = 0; i < marks.size(); i++)
    {
        sum += marks[i];
    }
    return sum / marks.size();
}

bool sort_by_average_mark(const STUDENT left, const STUDENT right)
{
    if (get_average(left.log) < get_average(right.log))
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
                         int w_n = 11, int w_s = 5, int w_c = 9)
{
    std::cout << "|";
    print_element(name, w_n, '|');
    print_element(shop, w_s, '|');
    print_element(cost, w_c, '|');
    std::cout << "\n";
}

void print_hor_sep(int width = 29)
{
    print_element('-', width, '\n', '-');
}

std::string get_str_marks(std::vector<int> &marks_list)
{
    std::string marks = std::string();
    char tmp;
    for (int i = 0; i < marks_list.size(); i++)
    {
        marks += std::to_string(marks_list[i]);
        if (i != marks_list.size() - 1)
        {
            marks += ',';
        }
    }
    return marks;
}

bool have_bigger(int bigger, std::vector<int> marks)
{
    for (int i = 0; i < marks.size(); i++)
    {
        if (marks[i] > bigger)
            return true;
    }
    return false;
}

void print_list(std::vector<STUDENT> &list, const char *table_name, int bigger = 0)
{
    std::string tmp;
    print_element(' ', 10, ' ');
    std::cout << table_name << "\n";
    print_hor_sep();
    print_price_element("NAME", "GROUP", "MARKS");
    print_hor_sep();
    for (int i = 0; i < list.size(); ++i)
    {
        if (bigger == 0 or have_bigger(bigger, list[i].log) == true)
        {
            tmp = get_str_marks(list[i].log);
            print_price_element(list[i].name, list[i].group, tmp);
        }
    }
    print_hor_sep();
    std::cout << "\n";
}

int main()
{
    std::vector<STUDENT> list;
    const char file_prices[] = "students.txt";
    load_from_file(list, file_prices);
    print_list(list, "STUDENTS");
    sort(list.begin(), list.end(), sort_by_average_mark);
    print_list(list, "SORTED");
    print_list(list, "BIGGER", 4);
    return 0;
}

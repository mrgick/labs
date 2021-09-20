#include <iostream>
#include <fstream>

void copy_from_file_to_file(std::ifstream &file_in,
                            std::ofstream &file_out,
                            int *counter)
{
    std::string tmp;

    while (true)
    {

        getline(file_in, tmp);

        if (file_in.eof())
        {
            break;
        }
        //std::cout << *counter << ")" << tmp << "\n";
        file_out << *counter << ") " << tmp << "\n";
        *counter += 1;
    }
}

int main()
{
    std::ifstream f1("test1.txt");
    std::ifstream f2("test2.txt");
    std::ofstream f3("test3.txt");
    int counter = 1;
    std::cout << "Starting copying.\n"; 
    copy_from_file_to_file(f1, f3, &counter);
    std::cout << "========";
    copy_from_file_to_file(f2, f3, &counter);
    std::cout << "========\n";
    std::cout << "Ending   copying.\n"; 

    f1.close();
    f2.close();
    f3.close();
    return 0;
}

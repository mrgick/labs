#include "stdio.h"
#include <cstdlib>
#include <ctime>
#include <algorithm>

void fill_array(int array[], const int array_length,
                int start, int end)
{
    for (int i = 0; i < array_length; i++)
    {
        array[i] = start + (rand() % (abs(start) + end + 1));
    }
}

void print_array(int array[], const int array_length)
{
    printf("array = [");
    for (int i = 0; i < array_length; i++)
    {
        printf("%d", array[i]);
        if (i != array_length - 1)
        {
            printf(", ");
        }
    }
    printf("]\n");
}

bool module_greater(const int left, const int right)
{
    return abs(left) < abs(right);
}

bool odd_greater(const int l, const int r)
{
    
    if (l % 2 == 0 and r % 2 == 0)
    {
        return l < r;
    }
    else if ((l % 2 != 0 and r % 2 == 0))
    {
        return false;
    }
    else if ((l % 2 == 0 and r % 2 != 0))
    {
        return true;
    }
    else
    {
        return l < r;
    }
}

int main()
{
    srand(time(0));
    const int n = 15;
    int arr[n];

    printf("Пункт а\n");
    fill_array(arr, n, -90, 90);
    print_array(arr, n);

    printf("Пункт б\n");
    std::sort(arr, arr + n);
    print_array(arr, n);

    printf("Пункт в\n");
    std::sort(arr, arr + n, module_greater);
    print_array(arr, n);

    printf("Пункт г\n");
    std::sort(arr, arr + n, odd_greater);
    print_array(arr, n);

    return 0;
}

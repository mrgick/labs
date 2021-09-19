#include "stdio.h"
#include <cstdlib>
#include <ctime>

struct array_peaks
{
    int min_i;
    int max_i;
};

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

void count_positive_negative(int array[], const int array_length)
{
    int positive = 0, negative = 0;
    for (int i = 0; i < array_length; i++)
    {
        if (array[i] > 0)
        {
            positive++;
        }
        else if (array[i] < 0)
        {
            negative++;
        }
    }
    printf("positive: %d; negative: %d\n", positive, negative);
}

void count_average_nearest(int array[], const int array_length)
{
    int sum = 0, nearest = array[0];
    double average;

    for (int i = 0; i < array_length; i++)
    {
        sum += array[i];
    }
    average = sum / array_length;

    for (int i = 0; i < array_length; i++)
    {
        if (abs(average - array[i]) < abs(average - nearest))
        {
            nearest = array[i];
        }
    }
    printf("average: %.2lf; nearest: %d\n", average, nearest);
}

array_peaks find_max_min_index(int array[], const int array_length)
{
    int max_i = 0, min_i = 0;
    for (int i = 0; i < array_length; i++)
    {
        if (array[i] > array[max_i])
        {
            max_i = i;
        }
        if (array[i] < array[min_i])
        {
            min_i = i;
        }
    }
    return {min_i, max_i};
}

void count_sum_between_min_max(int array[], const int array_length,
                               const int min_i, const int max_i)
{
    int sum = 0;
    int start, end;
    if (max_i < min_i)
    {
        start = max_i;
        end = min_i;
    }
    else
    {
        start = min_i;
        end = max_i;
    }
    for (int i = start + 1; i < end; i++)
    {
        sum += array[i];
    }
    printf("min: %d; max: %d; sum_between: %d\n",
           array[min_i], array[max_i], sum);
}

void print_histogram(int array[], const int array_length,
                     const int min_i, const int max_i)
{
    int count;
    const int min_left = array[min_i],
              max_right = array[max_i],
              center = 0;
    const char space[] = "                               ",
               star[] = "*******************************";

    printf("%.*s histogram\n", (abs(min_left) - 5), space);

    for (int i = 0; i < array_length; i++)
    {
        if (array[i] < 0)
        {
            count = abs(array[i] - min_left);
            if (count != 0)
            {
                printf("%.*s", count, space);
            }
            count = abs(array[i] - center);
            printf("%.*s|", count, star);
        }
        else
        {
            count = abs(center - min_left);
            printf("%.*s|", count, space);
            count = abs(array[i] - center);
            if (count != 0)
            {
                printf("%.*s", count, star);
            }
        }
        printf("\n");
    }
}

int main()
{
    srand(time(0));
    const int n = 10;
    int arr[n];

    // Пункт а
    fill_array(arr, n, -30, 30);
    print_array(arr, n);

    // Пункт б
    count_positive_negative(arr, n);

    // Пункт в
    count_average_nearest(arr, n);

    // Пункт г
    array_peaks arr_peaks = find_max_min_index(arr, n);
    count_sum_between_min_max(arr, n, arr_peaks.min_i, arr_peaks.max_i);

    // Пункт д
    print_histogram(arr, n, arr_peaks.min_i, arr_peaks.max_i);

    return 0;
}

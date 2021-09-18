#include "stdio.h"
#include <cstdlib>
#include <ctime>

int main()
{
    srand(time(0));
    int arr[10];

    // Пункт а
    printf("arr = [");
    for (int i = 0; i < 10; i++)
    {
        arr[i] = -30 + (rand() % 61);
        printf("%d", arr[i]);
        if (i != 9)
        {
            printf(", ");
        }
    }
    printf("]\n");

    // Пункт б
    int positive = 0, negative = 0;
    for (int i = 0; i < 10; i++)
    {
        if (arr[i] > 0)
        {
            positive++;
        }
        else if (arr[i] < 0)
        {
            negative++;
        }
    }
    printf("positive: %d; negative: %d\n", positive, negative);

    // Пункт в
    int sum = 0;
    double average;
    for (int i = 0; i < 10; i++)
    {
        sum += arr[i];
    }
    average = sum / 10;
    int nearest = arr[0];
    for (int i = 0; i < 10; i++)
    {
        if (abs(average - arr[i]) < abs(average - nearest))
        {
            nearest = arr[i];
        }
    }
    printf("average: %.2lf; nearest: %d\n", average, nearest);

    // Пункт г
    int max_i = 0, min_i = 0;
    for (int i = 0; i < 10; i++)
    {
        if (arr[i] > arr[max_i])
        {
            max_i = i;
        }
        if (arr[i] < arr[min_i])
        {
            min_i = i;
        }
    }
    sum = 0;
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
        sum += arr[i];
    }
    printf("min: %d; max: %d; sum_between: %d\n", arr[min_i], arr[max_i], sum);

    // Пункт д
    int min_left = arr[min_i],
        center = 0,
        max_right = arr[max_i];
    //printf("%d %d %d\n", min_left, center, max_right);
    
    printf("histogram:\n");
    
    for (int i = 0; i < 10; i++)
    {
        //printf("%d\n", arr[i]);
        if (arr[i] < 0)
        {
            for (int z = min_left; z < arr[i]; z++)
            {
                printf(" ");
            }
            for (int z = arr[i]; z < center; z++)
            {
                printf("*");
            }
            printf("|");
        }
        else{
            for (int z = min_left; z < center; z++)
            {
                printf(" ");
            }
            printf("|");
            for (int z = center; z < arr[i]; z++)
            {
                printf("*");
            }
        }
        printf("\n");
    }

    return 0;
}

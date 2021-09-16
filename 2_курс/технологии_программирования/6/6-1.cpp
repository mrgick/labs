#include "stdio.h"
#include <cstdlib>
#include <ctime>

int main()
{
    srand(time(0));
    int arr[10];
    for (int i = 0; i<10; i++){
        arr[i] = -30 + (rand() % 61);
        printf("%d ", arr[i]);
    }
    printf("\n");

    return 0;
}

#include "stdio.h"
#include <cstdlib>
#include <ctime>

bool in_array(int arr[4], int x) 
{
    int in = false;
    for (int n=0;n<4;n++) {
        if (x == arr[n]) {
            in = true;
            break;
        }
    }
    return in;
}

void print_arr4(int arr[4]) {
    for (int i=0;i<4;i++) {
        printf("%d",arr[i]);
    }
    printf("\n");
}

int main()
{
    srand(time(0));
    int inp[4], num[4] = {rand()%10, 0, 0, 0};
    int tmp, input, bulls, cows;
    
    //intro
    printf("Bulls and Cows!\n");
    printf("Guess four different numbers.\n");

    //filling array with different numbers.
    for (int i=1;i<4;i++) {
        do {
            tmp = rand()%10;
        } while(in_array(num, tmp) == true);
        num[i] = tmp;
    }

    //print_arr4(num);
    
    while (true) {
        scanf("%d", &input);
        //transfer number to array with nums
        for (int i=3; i>=0; i--) {
            inp[i] = input%10;
            input /= 10;
        }

        //print_arr4(inp);

        //count bulls and cows
        bulls = 0;
        cows = 0;
        for (int i=0; i<4; i++) {
            if (num[i] == inp[i]) {
                bulls++;
            }
            else if (in_array(num, inp[i]) == true) {
                cows++;
            }
        }

        if (bulls == 4) {
            printf("Guessed\n");
            break;
        }
        else {
            printf("Bulls:%d Cows:%d\n", bulls, cows);
        }
    }
    return 0;
}

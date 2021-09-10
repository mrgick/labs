#include "stdio.h"
#include <string.h>
#include <iostream>

int main() 
{
    int tmp = 50, num = 50;
    char input[1];
    printf("Make a number in your head.\n");
    printf("guessed - g, smaller - s, bigger - b\n");

    do {
        printf("Is it %d?\n", num);
        std::cin >> input;
        
        if (tmp < 1){
            tmp = 1;
        }
        else {
            tmp = abs(tmp/2);
        }

        if (strcmp(input, "b") == 0) {  
            num += tmp;
        }
        else if (strcmp(input, "s") == 0) {
            num -= tmp;
        }
        
    } while (strcmp(input, "g") != 0);
    return 0;
}

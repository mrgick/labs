#include <stdio.h>

int main() {
    int counter = 0;
    for (int i = 100000; i <= 999999; i++){
        if (i/100000 + i/10000%10 + i/1000%10 == 
            i/100%10 + i/10%10 + i%10) {
            counter++;
        }
    }
    printf("%d\n",counter);
    return 0;
}

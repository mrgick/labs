#include "stdio.h"

int main(){
    int s, m10 = 0, m5 = 0, m2 = 0, m1 = 0, coins;
    printf("Enter money:\n");
    scanf("%d", &s);
    if (s < 1 or s > 100){
        printf("The money must be in the range of 1 to 100.\n");
        return 1;
    }
    
    m10 = s/10;
    s = s%10;
    if (s != 0){
        m5 = s/5;
        s = s%5;
        if (s != 0){
            m2 = s/2;
            s = s%2;
            if (s != 0){
                m1 = s/1;
            }
        }
    }
    coins = m10 + m5 + m2 + m1;
    printf("\nCoins - %d\n\n10: %d\n5:  %d\n2:  %d\n1:  %d\n", 
            coins, m10, m5, m2, m1);

    return 0;
}
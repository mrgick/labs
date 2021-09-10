#include <stdio.h>
#include <cmath>

int main() {
    int n, tmp, len = 0;
    scanf("%d", &n);
    tmp = n;
    while (tmp != 0) {
        len++;
        tmp /= 10;
    }
    tmp = 0;
    while (len != 0) {
        tmp = n%abs(pow(10,len))/abs(pow(10,len-1));
        printf("%d ", tmp);
        len--;
    }
    printf("\n");

    return 0;
         
}
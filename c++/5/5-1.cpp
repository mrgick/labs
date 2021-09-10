#include "stdio.h"
#include <cstdlib>
#include <ctime>

int main() 
{
    srand(time(0));
    int answer, random = rand()%100;

    scanf("%d", &answer);
    //printf("Random=%d\n", random);
    while (true){
		if (random == answer){
			printf("Guessed\n");
			break;
		} else if (random < answer){
			printf("Bigger\n");
			scanf("%d", &answer);
		} else if (random > answer){
			printf("Smaller\n");
			scanf("%d", &answer);
		}
	}
 
    return 0;
}

#include <stdio.h>
#include <stdlib.h>
#include <ctime>
#include <unistd.h>

int main() 
{

	srand(static_cast<unsigned int>(time(0)));
	int k = 3;
	int i = 0;
	float S = 0;
	float a[k];
	float CC;
	float sec = 1;

first_entry:	
	a[i]= 40.00;

	if (!(i == k-1)){
		S=S+a[i];
		i=i+1;
		goto first_entry;

	} else {
		S=S+a[i];
		CC=S/k;
		printf("%.2f\n",CC);
		printf("\e[1;1H\e[2J");
		sleep(sec);

second_entry:		
		i = 0;
		
third_entry:

		S=S-a[i];
		a[i]= CC - 1 -float(rand()%10)/10;
		S=S+a[i];
		CC=S/k;

		if (CC<0){
			printf("0\n");
			return 0;
		}

		printf("%.2f\n",CC);
		printf("\e[1;1H\e[2J");
		sleep(sec);
		i=i+1;

		if (!(i > k-1)){
			goto third_entry;

		} else {
			goto second_entry;
		}
	}
}

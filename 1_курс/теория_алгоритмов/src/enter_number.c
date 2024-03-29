//Программа выводит среднее скользящее, a[i] - вводится руками)


#include <stdio.h>
#include <stdlib.h>
#include <ctime>

//вывод значений массива a[k] (отладка программы)
#define DEBUG
#ifdef DEBUG
	#define SETDBG \
		printf("Значения массива a[k]:\n");\
		for (int j = 0; j < k; j++)\
			printf(" %.2f", a[j]);\
		printf("\n");
#elif
	#define SETDBG
#endif


int main(int argc, char* args[]) 
{

	// устанавливаем значение системных часов в качестве стартового числа
	srand(static_cast<unsigned int>(time(0)));

	// объявление переменных
	int k = 3;
	int i = 0; // объявляем i=0, так как массивы считаются с 0
	float S = 0;
	float a[k];
	float CC = 0;

//первая точка входа (стрелка слева)	
first_entry:	
	
	//ввод числа 
	printf("Введите число (a[i]): ");
	scanf("%f", &a[i]);

	// ветка нет
	if (!(i == k-1)){
		S=S+a[i];
		printf("Нет данных\n");
		i=i+1;
		goto first_entry;

//SETDBG;

	//ветка да
	} else {
		S=S+a[i];
		CC=S/k;
		
		//вывод CC
		//printf("Значение CC: ");
		printf("%.2f\n",CC);

//SETDBG;

//вторая точка входа (стрелка справа)			
second_entry:		
		i = 0;// индекс массива начинается с нуля

//третья точка входа (стрелка слева)			
third_entry:

		S=S-a[i];

		//ввод числа 
		printf("Введите число (a[i]): ");
		scanf("%f", &a[i]);

		S=S+a[i];
		CC=S/k;
		
		//вывод CC
		//printf("Значение CC: ");
		printf("%.2f\n",CC);

		i=i+1;

//SETDBG;

		// ветка нет
		if (!(i > k-1)){
			goto third_entry;
		
		// ветка да
		} else {
			goto second_entry;
		}
	}
		
	return 0;
}

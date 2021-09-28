#include <iostream>
#include <cmath>

using namespace std;

// Определяем сходимость
double find_shod(double a[4][4])
{
    double shod = 0;
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            shod += pow(a[i][j], 2);
        }
    }
    shod = pow(shod, 0.5);
    return shod;
}

// Подгоняем матрицу под сходимость
void make_shod(double a[4][4], double b[4], double shod)
{
    int koef = round(shod) + 2;
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            a[i][j] = a[i][j] / koef;
        }
        b[i] = b[i] / koef;
    }
}

// Переставляем строки в матрице так, чтобы
// по диагонали были наибольшие коэффициенты
void move_lines(double a[4][4], double b[4])
{
    double tmp_a[4][4];
    double tmp_b[4];
    int max, index;
    for (int i = 0; i < 4; i++)
    {
        max = 0;
        for (int j = 0; j < 4; j++)
        {
            if (a[j][i] > max)
            {
                max = a[j][i];
                index = j;
            }
        }
        for (int j = 0; j < 4; j++)
        {
            tmp_a[i][j] = a[index][j];
            a[index][j] = 0;
            tmp_b[i] = b[index];
        }
    }

    // change original arrays
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            a[i][j] = tmp_a[i][j];
        }
        b[i] = tmp_b[i];
    }
}

// Вывод системы
void print_a_b(double a[4][4], double b[4])
{
    cout << "A" << endl;
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            cout << a[i][j] << " ";
        }
        cout << endl;
    }
    cout << "B" << endl;
    for (int i = 0; i < 4; i++)
    {
        cout << b[i] << " ";
    }
    cout << endl;
}

void solve(double a[4][4], double b[4], double x[4])
{
}

int main()
{
    int counter = 1, j, i;
    double b[4] = {64, 54, 65, 33},
           x[4] = {0, 0, 0, 0},
           y[4],
           delta = 0,
           e = 0.1;
    double a[4][4] = {
        {28, 9, 70, 43},
        {7, 28, 43, 3},
        {75, 98, 76, 37},
        {86, 40, 35, 87},
    };
    move_lines(a, b);
    make_shod(a, b, find_shod(a));

    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            if (i!=j){
                a[i][j] = a[i][j] / a[i][i];
            }
        }
        b[i] = b[i] / a[i][i];
        a[i][i] = 0;
    }

    while (true)
    {
        for (int i = 0; i < 4; i++)
        {
            delta = 0;
            for (int j = 0; j < 4; j++)
            {
                delta += a[i][j] * x[j];
            };
            y[i] = b[i] - delta;
        };
        for (int i = 0; i < 4; i++)
        {
            if (abs(x[i] - y[i]) > delta)
            {
                delta = abs(x[i] - y[i]);
            }
        }
        if (delta < e)
        {
            break;
        };

        for (int i = 0; i < 4; i++)
        {
            x[i] = y[i];
        };
        move_lines(a, b);
        make_shod(a, b, find_shod(a));
        counter++;
    };

    for (i = 0; i < 4; i++)
    {
        cout << "X" << i + 1 << " = " << y[i] << endl;
    };

    cout << "Amount of iterations = " << counter << endl;

    return 0;
}
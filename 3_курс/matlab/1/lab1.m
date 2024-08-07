%% Заданная выборка.
n = 15;
x = [-0.9, -0.8, -0.7, -0.6, -0.5, -0.4, -0.3, -0.2, -0.1, 0, 0.1, 0.2, 0.3, 0.4, 0.5];
y = [3.911, 3.893, 4.704, 4.993, 4.935, 5.477, 5.384, 5.489, 5.202, 5.714, 6.524, 6.348, 6.516, 7.136, 7.069];
%% Точечные оценки параметров a0 и a1 модели.
a = polyfit(x, y, 1);
a0 = a(2);
a1 = a(1);
fprintf('a0=%.6f a1=%.6f\n',a0,a1);
%% Линейная однофакторная детерминированная модель.
yr = a0 + a1 * x; 
% График линейной однофакторной детерминированной модели с изображенными на нем экспериментальными точками.
figure;
plot(x, yr, '- R', x, y, '* B');
legend('yr');
%% Среднее значение входного и выходного сигнала.
xMean = mean(x);
yMean = mean(y);
fprintf('xMean=%.6f yMean=%.6f\n',xMean,yMean);
%% Среднеквадратичное отклонение выходного сигнала.
s2 = 1/(n-2)*sum(power(y-yr,2));
fprintf('s2=%.6f\n',s2);
%% Квантиль распределения Стьюдента.
alpha = 0.1;
t = tinv(1-alpha/2, n-2);
%% 90%-доверительный интервал (левая и правая границы) для параметра а0.
tmp = t*sqrt(s2) * sqrt(1/n + power(xMean,2)/sum(power(x-xMean,2)));
a0left = a0 - tmp;
a0right = a0 + tmp;
fprintf('a0left=%.6f a0right=%.6f\n',a0left,a0right);
%% 90%-доверительный интервал (левая и правая границы) для параметра а1.
tmp = t*sqrt(s2) / sqrt(sum(power(x-xMean,2)));
a1left = a1 - tmp;
a1right = a1 + tmp;
fprintf('a1left=%.6f a1right=%.6f\n',a1left,a1right);
%% Квантили X2-распределения.
xleft = chi2inv(alpha/2, n-2);
xright = chi2inv(1-alpha/2, n-2);
%% 90%-доверительный интервал для среднеквадратичного отклонения выходного сигнала.
sleft = (n-2)*s2 / xright;
sright = (n-2)*s2 / xleft;
fprintf('sleft=%.6f sright=%.6f\n',sleft,sright);
%% 90%-доверительный коридор.
tmp = t*sqrt(s2) * sqrt(1/n + power(x-xMean,2)/sqrt(sum(power(x-xMean,2))));
yleft_i = yr + tmp;
yright_i = yr - tmp;
figure;
plot(x, yr, '- R', x, yleft_i, '-- G', x, yright_i, '-- B');
legend('yr', 'yleft', 'yright');
%% Квантиль распределения Фишера с 2 и N-2 степенями свободы.
f = finv(1-alpha, 2, n-2);
%% 90%-доверительная область.
tmp = 2*f*sqrt(s2) * sqrt(1/n + power(x-xMean,2)/sqrt(sum(power(x-xMean,2))));
yleft_i = yr - tmp;
yright_i = yr + tmp;
figure;
plot(x, yr, '- R', x, yleft_i, '--G', x, yright_i, '--B');
legend('yr', 'yleft', 'yright');

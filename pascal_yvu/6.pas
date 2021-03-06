program six;
Uses Math;
var E,i,j,S,l,SUM :real;
begin
    E := 0.001;
    SUM := 0; //переменная внешней суммы
    S := 0; //переменная внутренней суммы
    l := 1; //нужен для проверки точности, так же указываем l>E, чтобы запустить цикл while
    
    i := 1;
    while i<6 do begin
	    //внутренний цикл
	    l := 1;
	    j := 1;
	    while l > E do begin
	        l := sqrt(sqr(i)+sqr(j))/power(i+j,3.8);
	        S := S + l;
	        j := j+1;
	    end;
	    //---------------
	    SUM := SUM + (1/sin(i))*S;
	    i := i + 1;
	end;
    writeln('SUM=',SUM:0:3);
end.

program six;
Uses Math;
var E,S,l,SUM :real;
	i,j:integer;
begin
    E := 0.001;
    SUM := 0;
    for i:=1 to 5 do begin
	    l := 1;
	    j := 1;
	    S := 0;
	    while abs(l) > E do begin
	        l := sqrt(sqr(i)+sqr(j))/power(i+j,3.8);
	        S := S + l;
	        j := j+1;
	    end;
	    SUM := SUM + (1/sin(i))*S;
		writeln(i,') SUM=',SUM:0:3, ', S=', S:0:3);
	end;
    writeln('In the total: SUM=',SUM:0:3);
end.
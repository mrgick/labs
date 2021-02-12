program five;
Uses Math;
var x,shx,chx :real;
begin
	x:=1;
	writeln('┌────┬─────────┬─────────┐');
	writeln('│X   │1/√sh(X) │1/√ch(X) │');
	writeln('├────┼─────────┼─────────┤');
	while x < 2.01 do begin
		shx := 1/sqrt(Sinh(x));
		chx := 1/sqrt(Cosh(x));
		write('│',x:0:2,'│',shx:0:7,'│',chx:0:7,'│');
		writeln();
		x := x+0.05;	
    end;
    writeln('└────┴─────────┴─────────┘');
end.
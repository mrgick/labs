program nine;
Uses Math;

type
	mas = array[1..4] of real;
	mat = array[1..4,1..4] of real;

var 
	B:mas = ( 8,   4.2, 8.8, 5.5);
	C:mas = (-1,   6,  -1.8, 6.7);
	D:mas = ( 0.7,-1.1, 5.1, 6);
	X,Y,Z:mat;

procedure F(P,Q:mas; var R:mat;n:string);
var i,j:integer;
begin
	writeln('Matrix ',n);
	for i:=1 to 4 do
	begin
		for j:=1 to 4 do
		begin
			R[i][j]:=1/(power(P[i],2)+power(Q[j],3)+i+j);
			write(R[i][j]:0:4,' ');
		end;
		writeln('');
	end;
	writeln('');
end;

begin
	F(B,C,X,'X');
	F(B,D,Y,'Y');
	F(C,D,Z,'Z');
end.



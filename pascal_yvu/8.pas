program eight;
Uses Math;
type mas = array[1..4] of real;
const C: mas = (0.85, 1.4, 1.12, 3.24);
	  N: integer = 4;
var X, Y, Z: real;

Function F(C:mas; N:integer; U:real; j:integer):real;
var P:real;
	i:integer;
begin
	P:=1;
	for i:=1 to N do
		P:=P*(C[i]-U)/power(i,j);
	F:=P;
end;
	
begin
	X := F(C,N,0,1);
	Y := F(C,N,X,2);
	Z := F(C,N,Y,3);
	writeln('X=',X:0:4,' Y=',Y:0:4,' Z=',Z:0:4);
end.



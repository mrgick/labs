program eight;
Uses Math;
type mas = array[1..4] of real;
const C: mas = (0.85, 1.4, 1.12, 3.24);
	  N: integer = 4;
var X, Y, Z: real;

Function F(C:mas; N:integer; U:real; j:integer):real;
var Fak:real;
	i:integer;
begin
	Fak:=1;
	for i:=1 to N do
		Fak:=(C[i]-U)/power(i,j);
	F:=Fak;
end;
	
begin
	X := F(C,N,0,1);
	Y := F(C,N,X,2);
	Z := F(C,N,Y,3);
	writeln('X=',X:0:2,' Y=',Y:0:3,' Z=',Z:0:4);
end.



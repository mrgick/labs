program eight;
Uses Math;

type mas = array[1..4] of real;

const C: mas = (0.85, 1.4, 1.12, 3.24);
const N: integer = 4;

var X, Y, Z: real;

Function F(C:mas; N:integer; switch:string; X,Y:real):real;
	var Fak:real;
	var i:integer;
	begin
		Fak:=1;
		for i:=1 to N do
			// костыль, но я ничего придумать другого не смог, но под тз подходит)
			case switch of
			'X':Fak:=C[i]/i;
			'Y':Fak:=(C[i]-X)/power(i,2);
			'Z':Fak:=(C[i]-Y)/power(i,3);
			end;
		F:=Fak;
	end;

begin
	X := F(C,N,'X',0,0);
	Y := F(C,N,'Y',X,0);
	Z := F(C,N,'Z',X,Y);
	writeln('X=',X:0:2,' Y=',Y:0:6,' Z=',Z:0:6);
end.



program three;
Uses math;
var k: integer;
    s: real;
begin
	s := 0;
	k:=1;
	while k<=30 do 
	begin
        s := s + power(k,0.3) + exp(0.3*k);
        k := k+1;
    end;
    writeln('s=',s:0:6);
end.

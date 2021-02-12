program three;
Uses Math;
var k: integer;
var s: real;
begin
	s := 0;
    for k := 1 to 30 do
        s := s + power(k,0.3) + Exp(0.3*k);
    writeln('S=',s:0:6);
end.
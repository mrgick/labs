program three;
Uses math;
var k: integer;
    s: real;
begin
	s := 0;
    for k := 1 to 30 do
        s := s + power(k,0.3) + exp(0.3*k);
    writeln('s=',s:0:6);
end.

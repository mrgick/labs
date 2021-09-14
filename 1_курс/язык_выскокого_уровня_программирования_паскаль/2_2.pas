program two;
Uses Math;
var x,y:real;
begin
  readln(x);
  readln(y);
  //x := 1;
  //y := 2;
  if (x<1) or (x>7) or (y<2) or (y>7) or (sqr(x-4)+sqr(y-2)<4) 
  then writeln('Введенная точка не принадлежит фигуре')
  else writeln('Введенная точка принадлежит фигуре');
end.


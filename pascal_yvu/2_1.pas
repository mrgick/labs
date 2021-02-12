program two;
Uses Math;
var x,y:real;
begin
  readln(x);
  readln(y);
  //x := 2;
  //y := 0;
  if (x>=1) and (x<=7) and (y>=2) and (y<=7) and (sqr(x-4)+sqr(y-2)>=4) 
  then writeln('Введенная точка принадлежит фигуре')
  else writeln('Введенная точка не принадлежит фигуре');
end.


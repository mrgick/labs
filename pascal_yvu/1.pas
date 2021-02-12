program one;
Uses Math;
var x,y,w,u :real;
begin
  w := 2.1;
  y := w*w - 2.5;
  x := sqrt(y+w)/tan(w*y);
  u := (y*y-w*w)/cos(y*w) + (x*x-w*w)/sin(x*w);
  writeln('u(w)=',u:0:6);
end.


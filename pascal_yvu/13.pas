Program thirteen;
uses  cthreads,ptcGraph,SysUtils,Math;

const 
  n:integer = 100;
  

var 
  i,py:real;
  grD,grM,x0,y0:integer;

Function F1(x:real):real;
begin
  F1:=power(x+Exp(1.0),0.066666667);
end;

Function F2(x:real):real;
begin
  F2:=power(x+Exp(1.0),0.04);
end;

begin

//создание окна -> https://www.freepascal.org/docs-html/rtl/graph/modes.html
grD:=D4bit;
grM:=m1024x768;

//grD:=detect;
initgraph(grD,grM,'');

//задание цвета
setbkcolor(0);
cleardevice;
setcolor(15);

//начало координат
X0:=getmaxX div 2 - 400;
Y0:=getmaxY div 2 + 300;


//оси х и у
line(0,y0,getmaxX,y0);
line(x0,getmaxy,x0,0);

//подписи
outtextXY(x0-12,y0+10,'0');
outtextXY(getmaxX-10,y0+10,'X');
outtextXY(x0-12,10, 'Y');


// первый график
i:=0;
py:=F1(i);
outtextXY(x0-26,Y0-round(py*400-300), FloatToStrF(py,ffgeneral,2,1));
while i<100.1 do
  begin  
  putpixel(X0+round(i*8),Y0-round(F1(i)*400-300),13);
  i:=i+0.1;
  end;
i:=i-0.1;
py:=F1(i);
outtextXY(X0+round(i*8),Y0-round(py*400-555), '100');
outtextXY(X0-26,Y0-round(py*400-300), FloatToStrF(py,ffgeneral,2,1));

// второй график
i:=0;
py:=F2(i);
outtextXY(x0-35,Y0-round(py*400-300), FloatToStrF(py,ffgeneral,3,1));
while i<100.1 do
  begin
  putpixel(X0+round(i*8),Y0-round(F2(i)*400-300),12);
  i:=i+0.1;
  end;
i:=i-0.1;
py:=F2(i);
outtextXY(X0-26,Y0-round(py*400-300), FloatToStrF(py,ffgeneral,2,1));


outtextXY(X0+450,Y0-630, 'Graphics:');
setcolor(13);
outtextXY(X0+400,Y0-600, '(X+E)^(1/15)');

setcolor(12);
outtextXY(X0+500,Y0-600, '(X+E)^(1/25)');

sleep(5000);
end.


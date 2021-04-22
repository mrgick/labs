Program thirteen;
uses cthreads,ptcGraph,SysUtils,Math,Crt;

const
  xmin:integer = 0;
  xmax:integer = 100;
  step:integer = 1;

var 
  Fmax,Fmin:real;
  grD,grM,x0,y0,xstep,tmpx,tmpy1,tmpy2,px,py1,py2,x:integer;

function F1(x:real):real;
begin
  F1:=power(x+Exp(1.0),1/15);
end;

function F2(x:real):real;
begin
  F2:=power(x+Exp(1.0),1/25);
end;

function count_y(y,Fmin,Fmax:real):integer;
begin
  count_y:=round(Y0-50-400*(y-Fmin)/(Fmax-Fmin));
end;

procedure print_val(x0,y0,x1,y1,x3,y3:integer;num:string);
begin
  line(x0,y0,x1,y1);
  outtextXY(x3,y3,num);
end;

begin

  //поиск экстремумов функции
  x:=xmin;
  Fmin:=F1(x);
  Fmax:=Fmin;
  Repeat
    if F1(x)>Fmax then Fmax:=F1(x);
    if F2(x)>Fmax then Fmax:=F2(x);
    if F1(x)<Fmin then Fmin:=F1(x);
    if F2(x)<Fmin then Fmin:=F2(x);
    x:=x+step;
  until x>xmax;

  //корректировка
  writeln('min=',Fmin:8:5,' max=',Fmax:8:5);
  read(Fmin,Fmax);

  //создание окна 
  //Более подробно -> https://www.freepascal.org/docs-html/rtl/graph/modes.html
  grD:=D4bit;
  grM:=m1024x768;
  initgraph(grD,grM,'');

  //задание цвета фона и очистка экрана
  setbkcolor(0);
  cleardevice;

  //определение начала координат (div - деление нацело)
  X0:=getmaxX div 2 - 400;
  Y0:=getmaxY div 2 + 300;

  //вывод заголовков
  //оси х и у
  setcolor(15);
  line(0,y0,getmaxX,y0);
  line(x0,getmaxy,x0,0);
  //названия осей
  outtextXY(x0-12,y0+10,'O');
  outtextXY(getmaxX-20,y0+10,'X');
  outtextXY(x0-12,10, 'Y');
  //названия графиков
  outtextXY(X0+450,Y0-630, 'Graphics:');
  setcolor(2);
  outtextXY(X0+400,Y0-600, '(X+E)^(1/15)');
  setcolor(12);
  outtextXY(X0+500,Y0-600, '(X+E)^(1/25)');
  //Минимальный и максимальный y
  setcolor(15);
  //Fmin
  tmpy1:=count_y(Fmin,Fmin,Fmax);
  print_val(X0-3,tmpy1,X0+3,tmpy1,X0-35,tmpy1,FloatToStrF(Fmin,ffgeneral,3,2));
  //Fmax
  tmpy2:=count_y(Fmax,Fmin,Fmax);
  print_val(X0-3,tmpy2,X0+3,tmpy2,X0-35,tmpy2,FloatToStrF(Fmax,ffgeneral,3,2));

  //вывод графиков
  xstep:=round(500/((xmax-xmin)/step));
  x:=xmin;
  px:=X0;
  tmpy1:=0;tmpx:=0;tmpy2:=0;
  Repeat
    py1:=count_y(F1(x),Fmin,Fmax);
    py2:=count_y(F2(x),Fmin,Fmax);

    //вывод первого графика
    setcolor(2);
    if x>=1 then begin
      line(tmpx,tmpy1,px,py1);
      setcolor(12);
      line(tmpx,tmpy2,px,py2)
    end
    else begin
      putpixel(px,py1,2);
      putpixel(px,py2,12)
    end;

    //вывод значений осей
    //ось х
    setcolor(15);
    if (x<>0) and (x mod 10 = 0) then begin
      print_val(px,y0+5,px,y0-5,px-1,y0+10,InttoStr(x));
    end;

    //ось y
    tmpy1:=round(F1(x)*1000);
    if (tmpy1=1301) or (tmpy1=1202) or (tmpy1=1109) then begin
      print_val(X0-3,py1,X0+3,py1,X0-27,py1,FloatToStrF(F1(x),ffgeneral,2,1));
    end;


    //writeln(tmpx-px);
    tmpx:=px;
    tmpy1:=py1;
    tmpy2:=py2;

    px:=px+xstep;
    x:=x+step;
  until x>xmax;

  ReadKey;
//  sleep(10000);
end.


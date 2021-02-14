// Более подробно -> https://www.cyberforum.ru/turbo-pascal/thread77419.html

Program Spisok_dn;
type
  Tinf=integer; {тип данных, который будет храниться в элементе списка}
  List=^TList;  {Указатель на элемент типа TList}
  TList=record {А это наименование нашего типа "запись" обычно динамические структуры описываются через запись}
    data:TInf;  {данные, хранимые в элементе}
    next,    {указатель на следующий элемент списка}
    prev:List;   {указатель на предыдущий элемент списка}
  end;
 
{Процедура добавления нового элемента в двунаправленный список}
procedure AddElem(var nach,ends:List;znach1:TInf);
begin
  if nach=nil then {не пуст ли список, если пуст, то}
  begin
    Getmem(nach,SizeOf(TList)); {создаём элемент, указатель nach уже будет иметь адрес}
    nach^.next:=nil; {никогда не забываем "занулять" указатели}
    nach^.prev:=nil; {аналогично}
    ends:=nach; {изменяем указатель конца списка}
  end
  else {если список не пуст}
  begin
    GetMem(ends^.next,SizeOf(Tlist)); {создаём новый элемент}
    ends^.next^.prev:=ends; {связь нового элемента с последним элементом списка}
    ends:=ends^.next;{конец списка изменился и мы указатель "переставляем"}
    ends^.next:=nil; {не забываем "занулять" указатели}
  end;
  ends^.data:=znach1; {заносим данные}
end;
 

{процедура печати списка
полностью расписана при работе со стеком}
procedure Print(spis1:List;turn:byte);
begin
  if turn=1 
    then writeln('вывод списка от начала к концу')
    else writeln('вывод списка от конца к началу');
  if spis1=nil then
  begin
    writeln('Список пуст.');
    exit;
  end;
  while spis1<>nil do
  begin
    Write(spis1^.data, ' ');
    if turn=1 
    then spis1:=spis1^.next
    else spis1:=spis1^.prev
  end;
  writeln();
  writeln();
end;


{процедура удаления списка
 полностью расписана при работе со стеком}
Procedure FreeStek(spis1:List);
var
  tmp:List;
begin
  while spis1<>nil do
  begin
    tmp:=spis1;
    spis1:=spis1^.next;
    FreeMem(tmp,SizeOf(Tlist));
  end;
end;
 
{Функция поиска в списке
 полностью расписана при работе со стеком}
Function SearchElemZnach(spis1:List;znach1:TInf):List;
begin
  if spis1<>nil then
    while (Spis1<>nil) and (znach1<>spis1^.data) do
      spis1:=spis1^.next;
  SearchElemZnach:=spis1;
end;
 
{процедура удаления элемента в двунаправленном списке}
Procedure DelElem(var spis1,spis2:List;tmp:List);
var
  tmpi:List;
begin
  if (spis1=nil) or (tmp=nil) then
    exit;
  if tmp=spis1 then {если удаляемый элемент первый в списке, то}
  begin
    spis1:=tmp^.next; {указатель на первый элемент переставляем на следующий элемент списка}
    if spis1<>nil then {если список оказался не из одного элемента, то}
      spis1^.prev:=nil {"зануляем" указатель}
    else {в случае, если элемент был один, то}
      spis2:=nil; {"зануляем" указатель конца списка, а указатель начала уже "занулён"}
    FreeMem(tmp,SizeOf(TList));
  end
  else
    if tmp=spis2 then {если удаляемый элемент оказался последним элементом списка}
    begin
      spis2:=spis2^.prev; {указатель конца списка переставляем на предыдущий элемент}
      if spis2<>nil then {если предыдущий элемент существует,то}
        spis2^.next:=nil {"зануляем" указатель}
      else {в случае, если элемент был один в списке, то}
        spis1:=nil; {"зануляем" указатель на начало списка}
      FreeMem(tmp,SizeOf(TList));
    end
    else {если же удаляется список не из начали и не из конца, то}
    begin
      tmpi:=spis1;
      while tmpi^.next<>tmp do {ставим указатель tmpi на элемент перед удаляемым}
        tmpi:=tmpi^.next;
      tmpi^.next:=tmp^.next; {меняем связи}
      if tmp^.next<>nil then
        tmp^.next^.prev:=tmpi; {у элемента до удаляемого и после него}
      FreeMem(tmp,sizeof(TList));
    end;
end;
 

{процедура удаления элемента по позиции
 полностью расписана при работе со стеком}
Procedure DelElemPos(var spis1,spis2:List;posi:integer);
var
  i:integer;
  tmp:List;
begin
  if posi<1 then
    exit;
  if spis1=nil then
  begin
    Write('Список пуст');
    exit
  end;
  i:=1;
  tmp:=spis1;
  while (tmp<>nil) and (i<>posi) do
  begin
    tmp:=tmp^.next;
    inc(i)
  end;
  if tmp=nil then
  begin
    Writeln('Элемента с порядковым номером ' ,posi, ' нет в списке.');
    writeln('В списке всего ' ,i-1, ' элементов.');
    exit
  end;
  DelElem(spis1,spis2,tmp);
  Writeln('Элемент на позиции ',posi,' удалён');
  writeln();
end;
 
//запись в файл от начала к концу списка
procedure File_write(spis1:List);
var f: text;
begin

  assign (f, 'file.txt');
  rewrite(f);

  if spis1=nil then
  begin
    writeln(f, '');
    exit;
  end;
  while spis1<>nil do
  begin
    writeln(f, spis1^.data);
    spis1:=spis1^.next
  end;
  close(f);
  writeln('Запись в файл осуществлена');
  writeln();
end;



var
  SpisNach, {указатель на начало списка и}
  SpisEnd:List;   {указатель на конец списка. Эти два указателя неотъемлимая часть в двунаправленном списке}
  i:integer;



begin
  //инициализация списка (создание)
  SpisNach:=nil;
  SpisEnd:=nil;

  //заполнение списка
  for i:=1 to 10 do
    AddElem(SpisNach,SpisEnd,random(100));
  
  
  //вывод списка от начала к концу (просмотр вправо)
  Print(SpisNach,1);

  //удаление 4-ой позиции 
  DelElemPos(SpisNach,SpisEnd,4);

  //вывод списка от конца к началу (просмотр влево)
  Print(SpisEnd,0);

  //запись в файл
  File_write(SpisNach);

  //освобождение памяти использованного под список (удаление)
  FreeStek(SpisNach);

end.


//Целый и неуд марк нач
//Создание списка
//Удаление и вставка элемента
//Просмотр вправо и влево
//Запись в файл
//https://www.cyberforum.ru/turbo-pascal/thread77419.html
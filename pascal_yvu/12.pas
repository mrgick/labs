program twelve;
uses crt;

type list=^tlist;
  tlist=record
    data:integer;
    prev,next:list;
  end;

var first, { указатель на начало }
  left,right:list;
  input:char;

{ процедура сдвига влево }
procedure go_left; 
  begin
    right:=left;
    left:=right^.prev; { сдвиг указателей }
    write(left^.data, ' '); { вывод информационного поля }
    if left=first then
      writeln; { начало }    
  end;

{ процедура сдвига вправо }
procedure go_right; 
  begin
    left:=right;
    right:=left^.next;
    if left=first then
      writeln;
    write(left^.data, ' ');
  end;

{ процедура удаления }
procedure delete; 
  begin
    writeln();
    if left^.prev=left^.next then { не менее 2х элементов }
      writeln('Невозможно удалить - осталось два элемента.')
    else if left=first then
      writeln('Невозможно удалить - начало списка.')
    else begin
      right^.prev:=left^.prev;  { изменение указателей }
      left^.prev^.next:=left^.next;
      writeln('Удалено значение: ',left^.data);
      dispose(left);    { возвращение памяти }
      left:=right^.prev;
    end;
  end;

{ процедура вставки }
procedure insert;  
  var val:integer;
      node:list;
  begin
    writeln();
    writeln('Введите значение для вставки: ');
    readln(val);
    new(node);
    node^.data:=val;
    node^.prev:=left;   { связи из нового узла }
    node^.next:=right;
    right^.prev:=node;   { указание на новый узел }
    left^.next:=node;
    right:=node;
  end;

{ создание списка }
procedure create_list; 
  var x: integer;
  begin
    if (right<>nil) or (left<>nil) then begin
      writeln('Список не пуст. Следовательно, он уже создан.');    
    end
    else begin
      new(right); new(left);

      writeln('Введите первое значение:');
      readln(x);
      left^.data:=x;   { первый правый узел }
      writeln('Введите второе значение:');
      readln(x);
      right^.data:=x;   { первый левый узел }
      right^.prev:=left;
      right^.next:=left;
      left^.prev:=right;
      left^.next:=right;
      first:=left;
    end;
  end;

{ запись в файл }
procedure write_to_file;
  var f: text;
      tmp:list;
  begin
    assign (f, 'file.txt');
    rewrite(f);
    tmp := first;
    repeat
      writeln(f, tmp^.data);
      tmp:=tmp^.next
    until tmp=first;
    close(f);
    writeln();
    writeln('Запись в файл осуществлена.');
  end;

{ вывод меню }
procedure help;
  begin
    writeln('Меню взаимодействия со списком:');
    writeln('"l" - просмотр влево');
    writeln('"r" - просмотр вправо');
    writeln('"i" - вставить элемент');
    writeln('"d" - удалить элемент');
    writeln('"w" - записать список в файл');
    writeln('"h" - справка');
    writeln('"e" - выход из программы');
  end;


begin
  writeln('Для начала вам нужно создать список.');
  writeln('Для этого нажмите букву "c".');
  if ReadKey <> 'c' then exit
  else
  writeln('Происходит создание списка.');
  create_list;
  help;
  repeat
    input:=ReadKey;
    case input of
      'l': go_left;
      'r': go_right;
      'd': delete;
      'i': insert;
      'w': write_to_file;
      'h': help;
      'e': writeln('Завершение программы.')
        else
      writeln('Неверный ввод. "h" - справка.');
      end;
  until input='e';
end.
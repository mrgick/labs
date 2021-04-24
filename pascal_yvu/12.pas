program twelve;
uses crt;

type list=^tlist;
  tlist=record
    data:integer;
    prev,next:list;
  end;
 
procedure help;
  begin
    writeln('Справка:');
    writeln('"l" - просмотр влево');
    writeln('"r" - просмотр вправо');
    writeln('"i" - вставить элемент');
    writeln('"d" - удалить элемент');
    writeln('"w" - записать список в файл');
    writeln('"h" - справка');
    writeln('"e" - выход из программы');
  end;
 
procedure create_list(var n:list);
  begin
    new(n);
    write('Введите первое значение для списка: ');
    readln(n^.data);
  end;
 
procedure find_first(var n:list);
  begin
    while n^.prev <> nil do begin
      n:=n^.prev;
    end;
  end;
 
procedure print(n:list);
  var tmp:list;
  begin
    tmp:=n;
    find_first(tmp);
 
    while tmp<>nil do
      begin
      write(tmp^.data,' ');
      if tmp = n then write('| ');
      tmp:=tmp^.next;
    end;
 
    writeln();
  end;
 
procedure insert(var n:list); 
  var node:list;
      x:integer;
  begin
    write('Введите значение: ');
    readln(x);
    new(node);
    node^.next:=n^.next;
    node^.prev:=n;
    node^.data:=x;
    if n^.next<>nil then n^.next^.prev:=node;
    n^.next:=node;
    n:=node;
  end;
 
procedure delete(var n:list); 
var tmp:list;
  begin
    if (n^.prev=nil) and (n^.next=nil) then begin
      dispose(n);
      writeln('Список пуст');
      create_list(n);
    end
    else begin
      if n^.prev=nil then begin
        n:=n^.next;
       dispose(n^.prev);
        n^.prev:=nil;
      end
      else if n^.next=nil then begin
        n:=n^.prev;
       dispose(n^.next);
        n^.next:=nil;
      end 
      else begin
       tmp:=n;
        n^.next^.prev:=n^.prev;
        n^.prev^.next:=n^.next;
        n:=n^.prev;
        dispose(tmp);
      end;
    end;
  end;
 
procedure go_right(var n:list);
  begin
    if n^.next=nil then writeln('Конец списка')
    else begin
      n:=n^.next;
    end;
  end;
 
procedure go_left(var n:list);
  begin
    if n^.prev=nil then writeln('Начало списка')
    else begin
      n:=n^.prev;
    end;
  end;
 
procedure write_to_file(n:list);
  var f: text;
      tmp:list;
  begin
    tmp:=n;
    find_first(tmp);
    assign(f, 'file.txt');
    rewrite(f);
    while tmp<>nil do begin
      writeln(f, tmp^.data);
      tmp:=tmp^.next;
    end;
    close(f);
    writeln('Запись в файл осуществлена.');
  end;
 
var n:list;
input:char;

begin
  writeln('Для начала вам нужно создать список.');
  writeln('Для этого нажмите букву "c".');
  if ReadKey <> 'c' then exit
  else
  writeln('Происходит создание списка.');
  create_list(n);
  help;
  repeat
    print(n);
    input:=ReadKey;
    case input of
      'l': go_left(n);
      'r': go_right(n);
      'd': delete(n);
      'i': insert(n);
      'w': write_to_file(n);
      'h': help;
      'e': writeln('Завершение программы.')
        else
      writeln('Неверный ввод. "h" - справка.');
      end;
  until input='e';
end.

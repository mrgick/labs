program dads;
type
    PList = ^TList;
    TList = record
        inf: byte;
        next, prev: PList;
    end;
var
    ListBegin, ListEnd: PList;
    //a:TList;

procedure ListInit (var ListBegin, ListEnd: PList);
begin
    ListBegin:= nil;
    ListEnd:= nil;
end;

procedure AddToList (inf: byte; var ListBegin, ListEnd: PList);
var
    new_element: PList; 
begin
    new (new_element);
    if ListEnd = nil then
    begin
        ListBegin:= new_element;
        ListEnd:= new_element;
        ListEnd^.next:= nil;
        ListEnd^.prev:= nil;
    end
    else
    begin
        new_element^.prev:= ListEnd;
        new_element^.next:= nil;
        ListEnd^.next:= new_element;
    end;
    ListEnd:= new_element;
    new_element^.inf:= inf;
end;

Procedure PrintSpis(List:PList);
begin
  While List<>nil do
  begin
    Write(List^.inf, ' ');
    List:=List^.next;
  end;
end;

begin
    ListInit(ListBegin, ListEnd);
    AddToList(0,ListBegin, ListEnd);
    AddToList(1,ListBegin, ListEnd);
    AddToList(2,ListBegin, ListEnd);
    PrintSpis(ListBegin)
    //writeln();
end.

//тип данных - целые; список - двусвязный, с неудаляемым маркером начала списка

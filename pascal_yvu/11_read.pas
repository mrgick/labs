program eleven;
type
	mas=array[1..9] of string;
var
    f: text;
    str:mas;
    i,j,max:integer;
 	
Function expand_str(str:string;max:integer) :string;
var i:integer;
begin
while Length(str)<max do
	for i:=Length(str) downto 1 do
		if (Length(str)<max) and (str[i]=' ') then Insert(' ',str,i);
expand_str:=str;
end;

begin

//чтение
assign (f, 'file.txt');
reset(f);
for i:=1 to 9 do
	readln(f, str[i]);
close(f);

//определение длины самой длинной строки (Ух, тавтология))
max:=0;
for i:=1 to 9 do
	if max<length(str[i]) then max:=length(str[i]);

//выравние всех строк по длине
for i:=1 to 9 do
	if length(str[i])<max then
		str[i]:=expand_str(str[i],max);


//запись
assign (f, 'file_new.txt');
rewrite(f);
for i:=1 to 9 do
	writeln(f, str[i]);
close(f);

end.





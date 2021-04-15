program ten;
Uses Math;
var 
	all_str:string = '0123456789ABCDEFGHIJKL';
	input:string;
	i,j:integer;
	num:Extended = 0;

begin
	write('Введите число в 22-ой системе: ');
	readln(input);
	for i:=1 to length(input) do
	begin
		j:=Pos(input[i],all_str);
		if j=0 then
		begin
			writeln('Ошибка.');
			exit;
		end;
		num:= num + (j-1)*power(22,length(input)-i);
	end;
	writeln('Число в 10-ой системе: ',num:0:0);
end.
program seven;
const n = 10;
type
	mas = array [1..n] of real;
var
	a,old: mas;
	l:real;
	i,k,j: integer;

begin
	
	//рандом
	randomize;
	//заполняем массив псевдорандомными числами
	for i:=1 to n do
		a[i] := random(100);

	//---------------------------
	{
	
	//ручной ввод
	for i:=1 to n do
	begin
		write('Введите a[',i:2,'] = ');
		readln(a[i]);
	end;
	}

	//для вывода
	old := a; 

	for i:=2 to n do
	begin
		l := a[i];
		k := i;

		while a[k-1]<l do
		begin
			k:=k-1;
			//доход до границы
			if k <= 1 then begin
   				k:=1;
   				break;
 			end;
		end;

		//вставка
		for j := i-1 downto k do
	        a[j+1] := a[j];
		a[k] := l;
	end;
	
	//вывод
	writeln('┌─────┬───────┬───────┐');
	writeln('│Номер│  До   │ После │');
	writeln('├─────┼───────┼───────┤');
    for i := 1 to n do
        writeln('│','a[',i:2,']│ ', old[i]:5:2,' │ ',a[i]:5:2,' │ ');
    writeln('└─────┴───────┴───────┘');
	
end.


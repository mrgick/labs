program seven;
const n = 10;
type
	mas = array [1..n] of real;
var
	arr,x: mas;
	l:real;
	i,k,j,m: integer;

begin
	//заполняем массив псевдорандомными числами
	for i:=1 to n do
		arr[i] := random(100);
	//---------------------------

	x[1]:=arr[1];

	for i:=2 to n do
	begin
		l := arr[i];
		k := 1;
		while l<=x[k] do
		begin
			k:=k+1;
		end;


 		//вставка в массив
		for j := i-1 downto k do
		begin
	        x[j+1] := x[j];
		end;
		x[k] := l;    
	end;
	
	//вывод
	writeln('First|Second');
    for i := 1 to n do
        writeln(arr[i]:0:2,'│', x[i]:0:2);
end.


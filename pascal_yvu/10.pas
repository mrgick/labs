program ten;
Uses Math;

type
	mas = array[1..5] of Longint;

var 
	nums_str:string;
	nums_arr: mas;
	i,j,len:integer;
	num:Extended = 0;

begin
	//из 22 в 10

	//readln(nums_str);
	nums_str:='12345';
	len:=length(nums_str);
	for i:=1 to len do
	begin
		case nums_str[i] of
		'0':nums_arr[i]:=0;
		'1':nums_arr[i]:=1;
		'2':nums_arr[i]:=2;
		'3':nums_arr[i]:=3;
		'4':nums_arr[i]:=4;
		'5':nums_arr[i]:=5;
		'6':nums_arr[i]:=6;
		'7':nums_arr[i]:=7;
		'8':nums_arr[i]:=8;
		'9':nums_arr[i]:=9;
		'A':nums_arr[i]:=10;
		'B':nums_arr[i]:=11;
		'C':nums_arr[i]:=12;
		'D':nums_arr[i]:=13;
		'E':nums_arr[i]:=14;
		'F':nums_arr[i]:=15;
		'G':nums_arr[i]:=16;
		'H':nums_arr[i]:=17;
		'I':nums_arr[i]:=18;
		'J':nums_arr[i]:=19;
		'K':nums_arr[i]:=20;
		'L':nums_arr[i]:=21;
		else
			writeln('You entered incorrect number!');
			exit;
		end;
	end;
	
	i:=len;
	while i > 0 do
	begin
		j:=len+1-i; // костыль, но без него никак)
		num:= num + nums_arr[i]*power(22,j-1);
		i:= i-1;
	end;

	writeln('number=',num:0:0)

	//вывод массива
	//for i:=1 to 5 do
	//	write(nums_arr[i],'|');
	//writeln();

end.
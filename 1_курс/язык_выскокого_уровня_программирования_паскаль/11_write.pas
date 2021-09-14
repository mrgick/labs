program eleven;
type
	mas=array[1..9] of string;
var
    f: text;
    str:mas = ('The night, the pharmacy, the street,', 
	'The pointless lamppost in the mist.',
	'A quarter century recedes -',
	'There''s no escape. It all persists.',
	' ',
	'You''ll die - and you''ll begin anew,',
	'As in the past, all will repeat:',
	'The icy channel flowing through,', 
	'The lamp, the pharmacy, the street.');
    i:integer;
 	
begin

    
assign (f, 'file.txt');
rewrite(f);

for i:=1 to 9 do
	writeln(f, str[i]);

close(f);

end.





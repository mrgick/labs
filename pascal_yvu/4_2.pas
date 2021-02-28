program four;
Uses Math;
var E,x,k,S,f,i,l :real;
begin
    x := 0.85;
    E := 0.0001;
    k := 1;
    S := 0; 
    l := 1; 
    repeat
        f := 1;
        i := 1;
        repeat
            f := f*i;
            i := i+1;
        until i > k+2;
        l := sqrt(sqr(k)+sqr(x))/f;
        S := S + l;
        k := k+1;
    until l < E;
    writeln('Sum=',S:0:4);
end.
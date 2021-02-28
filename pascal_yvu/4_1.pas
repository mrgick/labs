program four;
Uses Math;
var E,x,k,S,f,i,l :real;
begin
    x := 0.85;
    E := 0.0001;
    k := 1;
    S := 0; 
    l := 1; 
    while l > E do begin
        f := 1;
        i := 1;
        while i <= k+2 do begin
            f := f*i;
            i := i+1;
        end;
        l := sqrt(sqr(k)+sqr(x))/f;
        S := S + l;
        k := k+1;
    end;
    writeln('Sum=',S:0:4);
end.
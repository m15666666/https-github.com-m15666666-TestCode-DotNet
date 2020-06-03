function d = set(d,varargin)  
argin=varargin;
while length(argin)>=2,
    prop=argin{1};
    val=argin{2};
    argin=argin(3:end);
    switch prop
        case 'x'
            d.x=val;
        case 'y'
            d.y=val;
        otherwise
            error('Asset properties:x,y');
    end
end
end

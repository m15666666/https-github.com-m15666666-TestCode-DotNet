function val = get(d,prop_name)
switch prop_name
    case 'x'
        val=d.x;
    case 'y'
        val=d.y;
    otherwise
        error([prop_name,'is not a valid list property']);
end
end
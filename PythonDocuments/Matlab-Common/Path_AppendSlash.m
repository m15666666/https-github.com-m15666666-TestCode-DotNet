% Ìí¼Ó·´Ð±Ïß
function p = Path_AppendSlash( path )
	p = path;
	if ~isempty( p ) && p(end) ~= '\'
		p = [p, '\'];
	end
end

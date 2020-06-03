% È¥µô·´Ð±Ïß
function p = Path_TrimSlash( path )
	p = path;
	if ~isempty( p ) && p(end) == '\'
		p = p(1:end-1);
	end
end

% ��ӷ�б��
function p = Path_AppendSlash( path )
	p = path;
	if ~isempty( p ) && p(end) ~= '\'
		p = [p, '\'];
	end
end

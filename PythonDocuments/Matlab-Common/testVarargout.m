function [nvals, varargout] = testVarargout(mult) 
% ²âÊÔvarargout¡¢nargout

% >> testVarargout(4) 
% ans = 
%         -1 
% >> [a b c d] = testVarargout(4) 
% a = 
%           3 
% b = 
%       -1.7303 
% c = 
%       -6.6623 
% d = 
%         0.5013 

%      nvals is the number of random values returned 
%      varargout contains the random values returned 
nvals = nargout - 1; 
for ii = 1:nargout-1 
        varargout{ii} =randn * mult; 
end

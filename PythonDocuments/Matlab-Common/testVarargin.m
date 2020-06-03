function testVarargin(varargin) 
% ²âÊÔvarargin¡¢nargin

% >> testVarargin 
% There are 0 arguments. 
% The input arguments are: 
% >> testVarargin(6) 
% There are 1 arguments. 
% The input arguments are: 
%         [6] 
% >> testVarargin(1,'test 1',[1 2,3 4]) 
% There are 3 arguments. 
% The input arguments are: 
%         [1]        'test 1'        [1x4 double] 

disp(['There are ' int2str(nargin) ' arguments.']);
disp('The input arguments are:');
disp(varargin);

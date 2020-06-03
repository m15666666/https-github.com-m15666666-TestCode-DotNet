function ret = CheckVar( varName )
% 检查变量是否定义

% function out = fun (in, P)
% % if isempty(P) % 失败
% % if P == [] % 失败
% if ~exist('P','var') % 用'var'限定下更好，不用也可以
% % if nargin<2 % 通过，但我不喜欢
%     % 使用参数P的默认值
% else
%     % 使用传入的参数P
% end
% % ……
% % matlab6.5 测试通过

ret = exist( varName, 'var' )
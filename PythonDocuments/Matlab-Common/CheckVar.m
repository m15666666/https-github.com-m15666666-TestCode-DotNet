function ret = CheckVar( varName )
% �������Ƿ���

% function out = fun (in, P)
% % if isempty(P) % ʧ��
% % if P == [] % ʧ��
% if ~exist('P','var') % ��'var'�޶��¸��ã�����Ҳ����
% % if nargin<2 % ͨ�������Ҳ�ϲ��
%     % ʹ�ò���P��Ĭ��ֵ
% else
%     % ʹ�ô���Ĳ���P
% end
% % ����
% % matlab6.5 ����ͨ��

ret = exist( varName, 'var' )
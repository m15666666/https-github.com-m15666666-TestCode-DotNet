
% MATLAB ������ ��1
% From��http://it.wenda.sogou.com/question/32959517.html

% MATLAB ������
% ���ͷ֣�0 - ���ʱ�䣺 2009��05��28�� 05ʱ54��
% ��ô��y=2*exp��-0.5*x��.*sin(2*pi*x)�İ�����
% �����ߣ� feelamei123 - �м�ħ��ʦ ����  ��Ϊ���� ��Ϊ����
% ��Ѵ�
% >> x = 0:.01:5;
% >> y=2*exp(-0.5*x).*sin(2*pi*x);
% >> f1 = 2*exp(-0.5*x);
% >> f2 = -2*exp(-0.5*x);
% >> plot(x,y,x,f1,':r',x,f2,':r')


x = 0:.01:5;
y=2*exp(-0.5*x).*sin(2*pi*x);
f1 = 2*exp(-0.5*x);
f2 = -2*exp(-0.5*x);
plot(x,y,x,f1,':r',x,f2,':r')
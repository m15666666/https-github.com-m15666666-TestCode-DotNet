% hilbert��һ����֪�źŵİ��������1
% http://zhidao.baidu.com/question/96253375.html?fr=qrl&cid=93&index=2

% ����һ��˭֪����matlab��������ô��hilbterʵ�ֶ�һ����֪�źŵİ���������ܸ���һ���򵥵ĳ����� 
% ���ⲹ�䣺
% �������վ�����ˣ����ǲ�̫�����
% ��������һ���źţ�
% Fs=1024;
% t=0:1/Fs:1;
% x=sin(16*pi*t)+5*sin(100*pi*t)+3*sin(260*pi*t+sin(80*pi*t))+10*sin(260*pi*t)+3*randn(size(t));
% ��ô�����İ����ף�������ָ��һ�£���Ϊ��ǰû���������ֱ���û�пɶԱȵ������������Լ�����Ҳ��֪���Բ��ԣ����ܰѳ���д���ҿ�һ����лл��
% 
% �����ߣ� snkhm - ������ ����
% ��Ѵ�
% 
% �ⷽ����������ò����
% 
% ������һ������ �㿴���ɣ�
% 
% close all
% % 
% fs=30;
% t=0:1/fs:200; 
% x6=sin(2*pi*2*t)+sin(2*pi*4*t);
% x66 = hilbert(x6);
% xx = abs(x66+j*x6);
% figure(1)
% hold on
% plot(t,x6);
% plot(t,xx,'r')
% xlim([0 5])
% hold off
% % �����㷨��δ���Ǳ߽�����
% d = diff(x6);
% n = length(d);
% d1 = d(1:n-1);
% d2 = d(2:n);
% indmin = find(d1.*d2<0 & d1<0)+1;
% indmax = find(d1.*d2<0 & d1>0)+1;
% envmin = spline(t(indmin),x6(indmin),t);
% envmax = spline(t(indmax),x6(indmax),t);
% figure
% hold on
% plot(t,x6);
% plot(t,envmin,'r');
% plot(t,envmax,'m');
% hold off
% xlim([0 5]) 

close all
% 
fs=30;
t=0:1/fs:200; 
x6=sin(2*pi*2*t)+sin(2*pi*4*t);
x66 = hilbert(x6);
xx = abs(x66+j*x6);
figure(1)
hold on
plot(t,x6);
plot(t,xx,'r')
xlim([0 5])
hold off

% �����㷨��δ���Ǳ߽�����
d = diff(x6);
n = length(d);
d1 = d(1:n-1);
d2 = d(2:n);
indmin = find(d1.*d2<0 & d1<0)+1;
indmax = find(d1.*d2<0 & d1>0)+1;
envmin = spline(t(indmin),x6(indmin),t);
envmax = spline(t(indmax),x6(indmax),t);
figure
hold on
plot(t,x6);
plot(t,envmin,'r');
plot(t,envmax,'m');
hold off
xlim([0 5]) 
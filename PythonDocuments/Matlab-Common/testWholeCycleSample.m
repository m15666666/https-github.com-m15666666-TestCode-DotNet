
% �����ڲ������ź�Ϊ�λ���Ƶ��й©
% From��http://bbs.matwav.com/viewthread.php?tid=853252
% testWholeCycleSample

fs=20; 
t=0:1/fs:1; 
% �ź�1
x1=sin(2*pi*t);
% �źŲ���
subplot(2,2,1) 
stem(x1); 
title('�����źţ�����0') 
xlabel('���У�n��') 
grid on 
% fft�׷���
number = 512;
%number = length(x1)
Y = fft(x1,number);

f = fs*(0:(number/2-1))/number; 
subplot(2,2,2) 
plot(f,abs(Y(1:number/2)));hold 
%stem(f,abs(Y(1:number/2)));
title('�����źŵ�FFT') 
xlabel('Ƶ��Hz') 
grid on 

% �ź�2
t=0:1/fs:2; 
x2=sin(2*pi*t);
% �ź�2����
subplot(2,2,3) 
stem(x2); 
title('�����źţ�����0') 
xlabel('���У�n��') 
grid on 
Y = fft(x2,number);
f = fs* (0 :  (number/2-1))/number; 
% fft�׷���
subplot(2,2,4) 
plot(f,abs(Y(1:number/2)));hold 
%stem(f,abs(Y(1:number/2)));
title('�����źŵ�FFT') 
xlabel('Ƶ��Hz') 
grid on 
 

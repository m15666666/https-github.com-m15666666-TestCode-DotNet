
% https://blog.csdn.net/cqfdcw/article/details/84995904
% С����С������С�����ֽ����ź��ع���С��������������ȡ �� С�����ֽ��ʵ�ְ�Ƶ�ʴ�С�ֲ���������(Matlab ������⣩
clear all  
clc

% 4.С����-----�źŷֽ����ع�������1��
fs=1024;  %����Ƶ��
f1=100;   %�źŵĵ�һ��Ƶ��
f2=300;   %�źŵڶ���Ƶ��
t=0:1/fs:1;
s=sin(2*pi*f1*t)+sin(2*pi*f2*t);  %���ɻ���ź�
[tt]=wpdec(s,3,'dmey');  %С�����ֽ⣬3����ֽ�3�㣬'dmey'ʹ��meyrС��
plot(tt)               %��С������ͼ
wpviewcf(tt,1);        %����ʱ��Ƶ��ͼ

wave=s;
t1=wpdec(wave,3,'dmey');
plot(t1);
t2 = wpjoin(t1,[3;4;5;6]);
sNod = read(t1,'sizes',[3,4,5,6]);

cfs3  = zeros(sNod(1,:));
cfs4  = zeros(sNod(2,:));
cfs5  = zeros(sNod(3,:));
cfs6  = zeros(sNod(4,:));


t3 = write(t2,'cfs',3,cfs3,'cfs',4,cfs4,'cfs',5,cfs5,'cfs',6,cfs6);

wave2=wprec(t3);

% 5.С����-----�źŷֽ����ع�������2��
x_input=s;                  %��������
plot(x_input);title('�����ź�ʱ��ͼ��')   %���������ź�ʱ��ͼ��

%%   �鿴Ƶ�׷�Χ
x=x_input;       
fs=128;
N=length(x); %���������
signalFFT=abs(fft(x,N));%��ʵ�ķ�ֵ
Y=2*signalFFT/N;
f=(0:N/2)*(fs/N);
figure;plot(f,Y(1:N/2+1));
ylabel('amp'); xlabel('frequency');title('�����źŵ�Ƶ��');grid on;

% ����������4��С�����ֽ�
wpt=wpdec(x_input,3,'dmey');        %����3��С�����ֽ�
plot(wpt);                          %����С������

for i=0:7
    rex3(:,i+1)=wprcoef(wpt,[3 i]);  %ʵ�ֶԽڵ�С���ڵ�����ع�        
end

figure;                          %���Ƶ�3������ڵ�ֱ��ع����źŵ�Ƶ��
for i=0:7
    subplot(2,4,i+1);
    x_sign=rex3(:,i+1); 
    N=length(x_sign); %���������
    signalFFT=abs(fft(x_sign,N));%��ʵ�ķ�ֵ
    Y=2*signalFFT/N;
    f=(0:N/2)*(fs/N);
    plot(f,Y(1:N/2+1));
    ylabel('amp'); xlabel('frequency');grid on;
    axis([0 50 0 0.03]); title(['С������3��',num2str(i),'�ڵ��ź�Ƶ��']);
end

% �����������ٻ���һ�µ�����8���ڵ��ع��źŵ�Ƶ������
nodes=[7;8;9;10;11;12;13;14];   %��3��Ľڵ��
ord=wpfrqord(nodes);  %С����ϵ�����ţ�ord�����ź�С����ϵ���������ɵľ�����3��ֽ��[1;2;4;3;7;8;6;5]
nodes_ord=nodes(ord); %���ź��С��ϵ��
for i=1:8
    rex3(:,i)=wprcoef(wpt,nodes_ord(i));  %ʵ�ֶԽڵ�С���ڵ�����ع�        
end
 
figure;                         %���Ƶ�3������ڵ�ֱ��ع����źŵ�Ƶ��
for i=0:7
    subplot(2,4,i+1);
    x_sign= rex3(:,i+1); 
    N=length(x_sign); %���������
    signalFFT=abs(fft(x_sign,N));%��ʵ�ķ�ֵ
    Y=2*signalFFT/N;
    f=(0:N/2)*(fs/N);
    plot(f,Y(1:N/2+1));
    maxY = max(Y) * 1.03;
    ylabel('amp'); xlabel('frequency');grid on
    axis([0 fs/2 0 maxY]); title(['С������3��',num2str(i),'�ڵ��ź�Ƶ��']);
end

figure;                         %���Ƶ�3������ڵ�ֱ��ع����źŵ�timewave
for i=0:7
    subplot(4,2,i+1);
    x_sign= rex3(:,i+1); 
    N=length(x_sign); %���������
    maxY = max(x_sign) * 1.03;
    minY = min(x_sign);
    disp(N)
    f=(0:N-1)*(1/fs);
    plot(f, x_sign);
    ylabel('amp'); xlabel('ms');grid on
    axis([0 max(f)*1.01 minY*1.01 maxY*1.01]); title(['С������3��',num2str(i),'�ڵ��ź�timewave']);
end
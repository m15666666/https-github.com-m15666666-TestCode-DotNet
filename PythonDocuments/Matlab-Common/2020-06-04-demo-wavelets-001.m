
% https://blog.csdn.net/cqfdcw/article/details/84995904
% 小波与小波包、小波包分解与信号重构、小波包能量特征提取 暨 小波包分解后实现按频率大小分布重新排列(Matlab 程序详解）
clear all  
clc

% 4.小波包-----信号分解与重构（方法1）
fs=1024;  %采样频率
f1=100;   %信号的第一个频率
f2=300;   %信号第二个频率
t=0:1/fs:1;
s=sin(2*pi*f1*t)+sin(2*pi*f2*t);  %生成混合信号
[tt]=wpdec(s,3,'dmey');  %小波包分解，3代表分解3层，'dmey'使用meyr小波
plot(tt)               %画小波包树图
wpviewcf(tt,1);        %画出时间频率图

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

% 5.小波包-----信号分解与重构（方法2）
x_input=s;                  %输入数据
plot(x_input);title('输入信号时域图像')   %绘制输入信号时域图像

%%   查看频谱范围
x=x_input;       
fs=128;
N=length(x); %采样点个数
signalFFT=abs(fft(x,N));%真实的幅值
Y=2*signalFFT/N;
f=(0:N/2)*(fs/N);
figure;plot(f,Y(1:N/2+1));
ylabel('amp'); xlabel('frequency');title('输入信号的频谱');grid on;

% 接下来进行4层小波包分解
wpt=wpdec(x_input,3,'dmey');        %进行3层小波包分解
plot(wpt);                          %绘制小波包树

for i=0:7
    rex3(:,i+1)=wprcoef(wpt,[3 i]);  %实现对节点小波节点进行重构        
end

figure;                          %绘制第3层各个节点分别重构后信号的频谱
for i=0:7
    subplot(2,4,i+1);
    x_sign=rex3(:,i+1); 
    N=length(x_sign); %采样点个数
    signalFFT=abs(fft(x_sign,N));%真实的幅值
    Y=2*signalFFT/N;
    f=(0:N/2)*(fs/N);
    plot(f,Y(1:N/2+1));
    ylabel('amp'); xlabel('frequency');grid on;
    axis([0 50 0 0.03]); title(['小波包第3层',num2str(i),'节点信号频谱']);
end

% 接下来我们再绘制一下第三层8个节点重构信号的频谱如下
nodes=[7;8;9;10;11;12;13;14];   %第3层的节点号
ord=wpfrqord(nodes);  %小波包系数重排，ord是重排后小波包系数索引构成的矩阵　如3层分解的[1;2;4;3;7;8;6;5]
nodes_ord=nodes(ord); %重排后的小波系数
for i=1:8
    rex3(:,i)=wprcoef(wpt,nodes_ord(i));  %实现对节点小波节点进行重构        
end
 
figure;                         %绘制第3层各个节点分别重构后信号的频谱
for i=0:7
    subplot(2,4,i+1);
    x_sign= rex3(:,i+1); 
    N=length(x_sign); %采样点个数
    signalFFT=abs(fft(x_sign,N));%真实的幅值
    Y=2*signalFFT/N;
    f=(0:N/2)*(fs/N);
    plot(f,Y(1:N/2+1));
    maxY = max(Y) * 1.03;
    ylabel('amp'); xlabel('frequency');grid on
    axis([0 fs/2 0 maxY]); title(['小波包第3层',num2str(i),'节点信号频谱']);
end

figure;                         %绘制第3层各个节点分别重构后信号的timewave
for i=0:7
    subplot(4,2,i+1);
    x_sign= rex3(:,i+1); 
    N=length(x_sign); %采样点个数
    maxY = max(x_sign) * 1.03;
    minY = min(x_sign);
    disp(N)
    f=(0:N-1)*(1/fs);
    plot(f, x_sign);
    ylabel('amp'); xlabel('ms');grid on
    axis([0 max(f)*1.01 minY*1.01 maxY*1.01]); title(['小波包第3层',num2str(i),'节点信号timewave']);
end
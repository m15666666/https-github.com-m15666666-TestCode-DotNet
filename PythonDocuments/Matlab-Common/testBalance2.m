warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testBalance2 ���涯ƽ�����scratch

% ����11
% vb7��ʹ�ù�˾ת��̨��WR(���ؽǶ���ת��������ͬ)�����涯ƽ�����飬ȥ����
% ��ʼ�񶯣�0.76 mm/s , 311��
% 
% ���أ�1.6g, 45��
% �����񶯣�1.28 mm/s , 270��
% �����Ľ��������ͽǶȣ�1.45g, 147��

% Xa0 = AmpDegree2Complex( 0.78, 360 - 311);
% Xa1 = AmpDegree2Complex( 1.28, 360 - 270 );
% U1 = AmpDegree2Complex( 1.6, 45 );
% a1 = ( Xa1 - Xa0 ) / U1;
% U0 = Xa0 / a1;
% [amp, degree] = Complex2AmpDegree( -U0 ) %#ok<NOPTS>

% vb7��ʹ�ù�˾ת��̨��WR(���ؽǶ���ת��������ͬ)�����涯ƽ�����飬��ȥ����
% ��ʼ�񶯣�0.76 mm/s , 326��
% 
% ���أ�1.6g, 0��
% �����񶯣�1.28 mm/s , 270��
% �����Ľ��������ͽǶȣ�1.99g, 143��

Xa0 = AmpDegree2Complex( 0.76, 360 - 326);
Xa1 = AmpDegree2Complex( 1.28, 360 - 270 );
U1 = AmpDegree2Complex( 1.6, 0 );

a1 = ( Xa1 - Xa0 ) / U1;
U0 = Xa0 / a1;

[amp, degree] = Complex2AmpDegree( -U0 - U1 ) %#ok<NOPTS>
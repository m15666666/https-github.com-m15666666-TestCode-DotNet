function ret = AmpDegree2Complex( amp, degree )
% ����ֵ�ͽǶȣ��ȣ�ת��Ϊ����

rad = Degree2Rad( degree );
ret = complex( amp * cos( rad ), amp * sin( rad ) );
function envelop = HilbertEnvelop( timeWave )
% ʹ��ϣ�����ؽ��а���첨

%envelop = abs( hilbert( timeWave ) + j * timeWave );

envelop = abs( hilbert( timeWave ) );

% hilbertMag = abs( hilbert( timeWave ) );
% envelop = abs( hilbertMag + j * timeWave );

envelop = ACCoupling( envelop );
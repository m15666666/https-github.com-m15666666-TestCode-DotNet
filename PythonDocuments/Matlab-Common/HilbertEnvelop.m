function envelop = HilbertEnvelop( timeWave )
% 使用希尔伯特进行包络检波

%envelop = abs( hilbert( timeWave ) + j * timeWave );

envelop = abs( hilbert( timeWave ) );

% hilbertMag = abs( hilbert( timeWave ) );
% envelop = abs( hilbertMag + j * timeWave );

envelop = ACCoupling( envelop );
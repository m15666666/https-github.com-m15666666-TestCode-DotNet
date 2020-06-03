function ret = RMS( timeWave )
% ÓÐÐ§Öµ

ret = sqrt( OriginMoment( timeWave, 2 ) );

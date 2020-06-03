function [] = DrawTimewaveAndFFTFromFile( waveFilePath, fs )

timeWave = load( waveFilePath );
DrawTimewaveAndFFT( timeWave, fs );


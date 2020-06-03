warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testBalance 单面动平衡计算模板

degreeOffset = 0;
Xa0 = AmpDegree2Complex( 5, 29 + degreeOffset);
Xa1 = AmpDegree2Complex( 2.9, 315 + degreeOffset );
U1 = AmpDegree2Complex( 100, 170 );

% [amp1, degree1] = Complex2AmpDegree(Xa1 - Xa0)
a1 = ( Xa1 - Xa0 ) / U1;
U0 = Xa0 / a1;
[amp, degree] = Complex2AmpDegree( -U0 ) %#ok<NOPTS>
% [a1amp, a1degree] = Complex2AmpDegree( ( Xa1 - Xa0 ) / U1 ) %#ok<NOPTS>
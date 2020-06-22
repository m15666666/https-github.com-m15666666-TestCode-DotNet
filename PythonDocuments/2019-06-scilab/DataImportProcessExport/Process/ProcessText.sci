A = csvRead('Data1.txt', [], [], 'string');

//////////////////////////////////////////////
// FIND SENSOR ID & POSITION                //
//////////////////////////////////////////////

// Locate %SENSOR
myMat = strstr(A, '%SENSOR');
indx = find(myMat ~='');

// Split text into part
numIndx = size(indx, 'c');
ID = [];
POS = [];
for i=1:numIndx
    tmp = strsplit(A(indx(i)), ' ');
    ID(i) = tmp(2);
    POS(i) = tmp(4) + ' ' + tmp(5) + ' ' + tmp(6);
end

POS = strsubst(POS, '(', '');
POS = strsubst(POS, ')', '');

// Change string into double
ID = evstr(ID);
POS = evstr(POS);

// Save your data
save('ID.sod', 'ID')
save('POS.sod', 'POS')
// Clear all
clear
// Load your data
load('ID.sod')
load('POS.sod')



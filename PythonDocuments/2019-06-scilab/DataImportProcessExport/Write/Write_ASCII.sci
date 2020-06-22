// Generate data
A = eye(10,10);
header1 = "This is a text file";
header2 = "This file contains 10 rows, 10 columns";

// Transform heterogeneous data into string data
myTmpMat = strcat(string(A), ' ', 'c');
myMat = [header1 ; header2 ;  myTmpMat];

// Write text file
csvWrite(myMat, 'myFile.txt')


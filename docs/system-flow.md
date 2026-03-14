# System Flow Pseudocode

This document describes the overall program flow of the Smart Library Management System (SLMS).

---

## Main Program

START PROGRAM  

LOAD saved users  
INITIALISE library  
LOAD book catalogue  

WHILE program is running  

 DISPLAY login menu  
 INPUT username  
 INPUT password  

 IF login is administrator THEN  
  OPEN administrator menu  

 ELSE  

  IF user account exists THEN  
   LOGIN existing user  
  ELSE  
   CREATE new user account  
  ENDIF  

  OPEN member menu  

 ENDIF  

END WHILE  

END PROGRAM  

---

## User Login Process

DISPLAY login prompt  
INPUT username  
INPUT password  

IF username exists in stored users THEN  
 VALIDATE password  
 LOGIN user  
ELSE  
 CREATE new user account  
ENDIF

\connect recruitment
CREATE TABLE User
(
 Id serial PRIMARY KEY,
 Name VARCHAR (50) NOT NULL,
 Email VARCHAR (100) NOT NULL,
 Address VARCHAR (100) NOT NULL,
 Phone VARCHAR (100) NOT NULL,
 UsrType VARCHAR (100) NOT NULL,
 Money decimal (100) NOT NULL
);
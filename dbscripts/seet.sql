
create table if not exists users(
 id serial PRIMARY KEY,
 "name" VARCHAR (50) NOT NULL,
 email VARCHAR (100) NOT NULL,
 address VARCHAR (100) NOT NULL,
 phone VARCHAR (100) NOT NULL,
 userType VARCHAR (100) NOT NULL,
 money decimal (100) NOT NULL
);


insert into users("name",email,address,phone,userType,money) values('ale', 'a@gmail.com', 'c','3147','Normal',2000)
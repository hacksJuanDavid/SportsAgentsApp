/*DROP USER 'usersportsagents'@'localhost' ;*/
CREATE USER 'usersportsagents'@'localhost' IDENTIFIED BY 'Us0n3rBr4nkUs3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'usersportsagents'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;
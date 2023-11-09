/*DROP USER 'contactportsagents'@'localhost' ;*/
CREATE USER 'contactportsagents'@'localhost' IDENTIFIED BY 'C0nPAG32.S*xnkXs3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'contactportsagents'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;
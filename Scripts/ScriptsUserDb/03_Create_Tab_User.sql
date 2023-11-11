CREATE TABLE `usersportsagentsdb`.`users` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `UserName` VARCHAR(20) NOT NULL,
    `Password` VARCHAR(20) NOT NULL,
    `Email` VARCHAR(45) NOT NULL,
    `Role` VARCHAR(45) NOT NULL,
    PRIMARY KEY (`Id`)
);
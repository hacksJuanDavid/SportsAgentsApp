CREATE TABLE `contactsportsagentsdb`.`contact` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `UserName` VARCHAR(20) NOT NULL,
    `Email` VARCHAR(45) NOT NULL,
    `Message` VARCHAR(45) NOT NULL,
    PRIMARY KEY (`Id`)
);
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS `Whos` (
	`WhoId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Icon`	TEXT,
	`Description`	TEXT
);
INSERT INTO `Whos` (WhoId,Icon,Description) VALUES (1,'fa-user','non-programmer');
INSERT INTO `Whos` (WhoId,Icon,Description) VALUES (2,'fa-flask','non-programmer with a scientific background');
INSERT INTO `Whos` (WhoId,Icon,Description) VALUES (3,'fa-cog','programmer');
CREATE TABLE IF NOT EXISTS `Whats` (
	`WhatId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Icon`	TEXT,
	`Description`	TEXT
);
INSERT INTO `Whats` (WhatId,Icon,Description) VALUES (1,'fa-chalkboard','learn my first programming language');
INSERT INTO `Whats` (WhatId,Icon,Description) VALUES (2,'fa-user-tie','learn a new useful programming language');
INSERT INTO `Whats` (WhatId,Icon,Description) VALUES (3,'fa-lightbulb','learn a new interesting programming language ');
CREATE TABLE IF NOT EXISTS `Paradigm` (
	`ParadigmId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT,
	`Description`	TEXT
);
INSERT INTO `Paradigm` (ParadigmId,Name,Description) VALUES (1,'Functional','Functional programming language');
CREATE TABLE IF NOT EXISTS `Fields` (
	`FieldId`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT,
	`Icon`	TEXT
);
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (1,'Everything','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (2,'Games','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (3,'System Software','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (4,'Finance','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (5,'Automate things','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (6,'Mobile','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (7,'Web','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (8,'Finance','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (9,'Enterprise','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (10,'Science','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (11,'Concurrency','fa-user');
INSERT INTO `Fields` (FieldId,Name,Icon) VALUES (12,'Apple','fa-user');
COMMIT;

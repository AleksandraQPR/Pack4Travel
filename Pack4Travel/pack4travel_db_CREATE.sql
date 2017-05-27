USE pack4travel
GO

IF OBJECT_ID('equipementIems', 'U') IS NOT NULL
	DROP TABLE equipementIems;
IF OBJECT_ID('equipements', 'U') IS NOT NULL
	DROP TABLE equipements;
IF OBJECT_ID('equipementGroups', 'U') IS NOT NULL
	DROP TABLE equipementGroups;
IF OBJECT_ID('items', 'U') IS NOT NULL
	DROP TABLE items;
IF OBJECT_ID('itemGroup', 'U') IS NOT NULL
	DROP TABLE itemGroup;
IF OBJECT_ID('userInfo', 'U') IS NOT NULL
	DROP TABLE userInfo;	


CREATE TABLE userInfo
(
	idUser INT NOT NULL IDENTITY PRIMARY KEY,
	userName VARCHAR(20) NOT NULL,
	mail VARCHAR(40) NOT NULL,
	password VARCHAR(30) NOT NULL,
	activeStatus CHAR(1) NOT NULL
);
GO

CREATE TABLE itemGroup
(
	idItemGroup INT NOT NULL IDENTITY PRIMARY KEY,
	groupName VARCHAR(30) NOT NULL
);
GO

CREATE TABLE items
(
	idItem INT NOT NULL IDENTITY PRIMARY KEY,
	itemName VARCHAR(30) NOT NULL,
	idGroup INT NOT NULL,
	idOwner INT,
	privateStatus CHAR(1) NOT NULL DEFAULT 'N',
	FOREIGN KEY(idGroup) REFERENCES itemGroup(idItemGroup),
	FOREIGN KEY(idOwner) REFERENCES userInfo(idUser)
);
GO

CREATE TABLE equipementGroups
(
	idEquipementGroup INT NOT NULL IDENTITY PRIMARY KEY,
	groupName VARCHAR(30) NOT NULL
);
GO

CREATE TABLE equipementTags
(
	idEquipement INT NOT NULL,
	idEquipementGroup INT NOT NULL,
	FOREIGN KEY(idEquipement) REFERENCES equipements(idEquipement),
	FOREIGN KEY(idEquipementGroup) REFERENCES equipementTags(idEquipementGroup),
);
GO

CREATE TABLE equipements
(
	idEquipement INT NOT NULL IDENTITY PRIMARY KEY,
	equipementName VARCHAR(30) NOT NULL,
	idGroup INT NOT NULL,
	idOwner INT,
	privateStatus CHAR(1) NOT NULL DEFAULT 'N',
	five_stars INT NOT NULL DEFAULT 0,
	four_stars INT NOT NULL DEFAULT 0,
	three_stars INT NOT NULL DEFAULT 0,
	two_stars INT NOT NULL DEFAULT 0,
	one_star INT NOT NULL DEFAULT 0,
	FOREIGN KEY(idGroup) REFERENCES equipementGroups(idEquipementGroup),
	FOREIGN KEY(idOwner) REFERENCES userInfo(idUser)
);
GO

CREATE TABLE equipementIems
(
	idEquipement INT NOT NULL,
	idItem INT NOT NULL,
	FOREIGN KEY(idEquipement) REFERENCES equipements(idEquipement),
	FOREIGN KEY(idItem) REFERENCES items(idItem)
);
GO



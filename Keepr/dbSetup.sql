-- Active: 1673975052405@@SG-KaylaCammack-7102-mysql-master.servers.mongodirector.com@3306@Test

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg TEXT
    ) default charset utf8 COMMENT '';

DROP TABLE accounts;

-- SECTION KEEPS

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description TEXT NOT NULL,
        img VARCHAR(255) NOT NULL,
        views INT NOT NULL DEFAULT 0,
        vaultKeepId INT,
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE keeps;

-- SECTION VAULTS

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description TEXT NOT NULL,
        img VARCHAR(255) NOT NULL,
        isPrivate BOOLEAN NOT NULL DEFAULT false,
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE vaults;

-- SECTION VAULTKEEPS

CREATE TABLE
    IF NOT EXISTS vaultkeeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE vaultkeeps;

SELECT
    vk.id as vaultKeepId,
    vk.keepId as keepId,
    k.*,
    v.*,
    a.*
FROM vaultkeeps vk
    JOIN accounts a ON a.Id = vk.creatorId
    JOIN vaults v ON v.id = 21
    JOIN keeps k ON k.id = vk.keepId
WHERE vk.vaultId = 21;

SELECT v.*, a.*
FROM vaults v
    JOIN accounts a ON v.creatorId = a.id
WHERE
    v.creatorId = "63c9b1c875f8e740f5723648";

SELECT
    -- COUNT (vaultId) AS kept,
    k.*,
    a.*
FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
WHERE k.id = 1
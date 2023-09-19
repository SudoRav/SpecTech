CREATE DATABASE  SpecTech
GO
USE SpecTech
GO


-- =============================================================
-- = Должность
-- =============================================================
CREATE TABLE [dbo].[post]
(
 [ID]       int IDENTITY (1, 1) NOT NULL,
 [postName] nvarchar(40) NOT NULL,
 [access1]  bit NOT NULL,
 [access2]  bit NOT NULL,
 [access3]  bit NOT NULL,
 [access4]  bit NOT NULL,
 [access5]  bit NOT NULL,
 [access6]  bit NOT NULL,
 [salary]   int NOT NULL,


 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([ID] ASC));
GO

-- =============================================================
-- = Пользователи
-- =============================================================
CREATE TABLE [dbo].[users]
(
 [ID]      int IDENTITY (1, 1) NOT NULL,
 [log]     nvarchar(50) NOT NULL,
 [pass]    nvarchar(50) NOT NULL,
 [F]       nvarchar(30) NOT NULL,
 [I]       nvarchar(30) NOT NULL,
 [phone]   nvarchar(20) NOT NULL,
 [email]   nvarchar(50) NOT NULL,
 [ID_post] int NOT NULL,


 CONSTRAINT [PK_127] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_130] FOREIGN KEY ([ID_post])  REFERENCES [dbo].[post]([ID]));
GO


CREATE NONCLUSTERED INDEX [fkIdx_132] ON [dbo].[users] 
 ( [ID_post] ASC )
GO


-- =============================================================
-- = Отсутствие
-- =============================================================
CREATE TABLE [dbo].[absence]
(
 [ID]      int IDENTITY (1, 1) NOT NULL,
 [ID_user]    int NOT NULL,
 [cause]      nvarchar(100) NOT NULL,
 [date_start] date NOT NULL,
 [date_fin]   date NOT NULL,


 CONSTRAINT [FK_219] FOREIGN KEY ([ID_user])  REFERENCES [dbo].[users]([ID]));
GO


CREATE NONCLUSTERED INDEX [fkIdx_221] ON [dbo].[absence] 
 ( [ID_user] ASC )
GO


-- =============================================================
-- = Образование
-- =============================================================
CREATE TABLE [dbo].[education]
(
 [ID]      int IDENTITY (1, 1) NOT NULL,
 [ID_user]   int NOT NULL,
 [education] nvarchar(200) NOT NULL,


 CONSTRAINT [FK_212] FOREIGN KEY ([ID_user])  REFERENCES [dbo].[users]([ID]));
GO


CREATE NONCLUSTERED INDEX [fkIdx_214] ON [dbo].[education] 
 ( [ID_user] ASC )
GO


-- =============================================================
-- = Выплаты
-- =============================================================
CREATE TABLE [dbo].[pay]
(
 [ID]      int IDENTITY (1, 1) NOT NULL,
 [ID_user] int NOT NULL,
 [date]    date NOT NULL,
 [summ]    int NOT NULL,


 CONSTRAINT [FK_231] FOREIGN KEY ([ID_user])  REFERENCES [dbo].[users]([ID]));
GO


CREATE NONCLUSTERED INDEX [fkIdx_233] ON [dbo].[pay] 
 ( [ID_user] ASC )
GO


-- =============================================================
-- = Премии
-- =============================================================
CREATE TABLE [dbo].[prize]
(
 [ID]      int IDENTITY (1, 1) NOT NULL,
 [ID_user] int NOT NULL,
 [date]    date NOT NULL,
 [summ]    int NOT NULL,


 CONSTRAINT [FK_244] FOREIGN KEY ([ID_user])  REFERENCES [dbo].[users]([ID]));
GO


CREATE NONCLUSTERED INDEX [fkIdx_246] ON [dbo].[prize] 
 ( [ID_user] ASC )
GO


-- =============================================================
-- = Аренды
-- =============================================================
CREATE TABLE [dbo].[leas]
(
 [ID]         int IDENTITY (1, 1) NOT NULL,
 [ID_user]    int NOT NULL,
 [address]    nvarchar(200) NOT NULL,
 [data_start] date NOT NULL,
 [data_fin]   date NOT NULL,
 [summ]       nvarchar(50) NOT NULL,


 CONSTRAINT [PK_168] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_169] FOREIGN KEY ([ID_user])  REFERENCES [dbo].[users]([ID]));
GO


CREATE NONCLUSTERED INDEX [fkIdx_171] ON [dbo].[leas] 
 ( [ID_user] ASC )
GO


-- =============================================================
-- = Тип техники
-- =============================================================
CREATE TABLE [dbo].[type]
(
 [ID]       int IDENTITY (1, 1) NOT NULL,
 [typeName] nvarchar(50) NOT NULL,


 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED ([ID] ASC));
GO


-- =============================================================
-- = Техника
-- =============================================================
CREATE TABLE [dbo].[tech]
(
 [ID]          int IDENTITY (1, 1) NOT NULL,
 [name]        nvarchar(50) NOT NULL,
 [ID_type]     int NOT NULL,
 [photo]       varbinary(max) NULL,
 [desc]        text NOT NULL,
 [price]       nvarchar(15) NOT NULL,
 [status_leas] bit NOT NULL,
 [status_rep]  bit NOT NULL,
 [rep_text]    text NULL,
 [discount]    int NULL,


 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_133] FOREIGN KEY ([ID_type])  REFERENCES [dbo].[type]([ID]));
GO


CREATE NONCLUSTERED INDEX [fkIdx_135] ON [dbo].[tech] 
 ( [ID_type] ASC )
GO


-- =============================================================
-- = Характкристики
-- =============================================================
CREATE TABLE [dbo].[characts]
(
 [ID]      int IDENTITY (1, 1) NOT NULL,
 [ID_tech] int NOT NULL,
 [name]    nvarchar(50) NULL,
 [val]     nvarchar(50) NULL,


 CONSTRAINT [PK_182] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_190] FOREIGN KEY ([ID_tech])  REFERENCES [dbo].[tech]([ID]));
GO


CREATE NONCLUSTERED INDEX [fkIdx_192] ON [dbo].[characts] 
 (
  [ID_tech] ASC
 )
GO


-- =============================================================
-- = Список техникики в аренде
-- =============================================================
CREATE TABLE [dbo].[list_tech]
(
 [ID]      int IDENTITY (1, 1) NOT NULL,
 [ID_leas] int NOT NULL,
 [ID_tech] int NOT NULL,


 CONSTRAINT [PK_189] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_160] FOREIGN KEY ([ID_tech])  REFERENCES [dbo].[tech]([ID]),
 CONSTRAINT [FK_207] FOREIGN KEY ([ID_leas])  REFERENCES [dbo].[leas]([ID]));
GO


CREATE NONCLUSTERED INDEX [fkIdx_162] ON [dbo].[list_tech] 
 ( [ID_tech] ASC )
GO

CREATE NONCLUSTERED INDEX [fkIdx_209] ON [dbo].[list_tech] 
 ( [ID_leas] ASC )
GO


-- =============================================================
-- = История
-- =============================================================
CREATE TABLE [dbo].[history]
(
 [ID]        int IDENTITY (1, 1) NOT NULL,
 [objectID]  int NOT NULL,
 [operation] nvarchar(200) NOT NULL,
 [date]      datetime NOT NULL DEFAULT GETDATE(),
 )
GO


-- =============================================================
-- = Триггеры
-- =============================================================
CREATE TRIGGER tech_ins
ON tech
AFTER INSERT
AS
INSERT INTO history ([objectID], [operation])
SELECT ID, 'Добавлена техника ' + [name]
FROM INSERTED
GO

CREATE TRIGGER tech_del
ON tech
AFTER DELETE
AS
INSERT INTO history ([objectID], [operation])
SELECT ID, 'Удалена техника ' + [name]
FROM DELETED
GO

CREATE TRIGGER user_ins
ON users
AFTER INSERT
AS
INSERT INTO history ([objectID], [operation])
SELECT ID, 'Добавлен пользователь ' + [F] + ' ' + [I]
FROM INSERTED
GO

CREATE TRIGGER user_del
ON users
AFTER DELETE
AS
INSERT INTO history ([objectID], [operation])
SELECT ID, 'Удалён пользователь ' + [F] + ' ' + [I]
FROM DELETED
GO

--CREATE TRIGGER tech_del_child
--ON [tech]
--FOR DELETE
--AS
--BEGIN
--DELETE FROM characts WHERE characts.ID_tech = photo
--END
--GO

--CREATE TRIGGER user_del_child
--ON [users]
--FOR DELETE
--AS
--BEGIN
--DELETE FROM absence WHERE ID_user = [ID]
----DELETE FROM education WHERE ID_user = [ID]
----DELETE FROM pay WHERE ID_user = [ID]
----DELETE FROM prize WHERE ID_user = [ID]
--END
--GO


-- =============================================================
-- = Наполнение
-- =============================================================
INSERT INTO post ([postName], [access1], [access2], [access3], [access4], [access5], [access6], [salary]) VALUES ('No post', 'True', 'True', 'True', 'True', 'True', 'True', 0)
INSERT INTO post ([postName], [access1], [access2], [access3], [access4], [access5], [access6], [salary]) VALUES ('Post1', 'True', 'True', 'True', 'True', 'True', 'True', 1)
INSERT INTO post ([postName], [access1], [access2], [access3], [access4], [access5], [access6], [salary]) VALUES ('Post2', 'False', 'False', 'False', 'False', 'False', 'False', 2)
GO
INSERT INTO users (log, pass, F, I, phone, email, ID_post) VALUES ('1', 'C4CA4238A0B923820DCC509A6F75849B', '1', '1', '1', '1', 1)
INSERT INTO users (log, pass, F, I, phone, email, ID_post) VALUES ('2', 'C81E728D9D4C2F636F067F89CC14862C', '2', '2', '2', '2', 2)
INSERT INTO users (log, pass, F, I, phone, email, ID_post) VALUES ('3', 'ECCBC87E4B5CE2FE28308FD9F2A7BAF3', '3', '3', '3', '3', 3)
GO
INSERT INTO absence(ID_user, cause, date_start, date_fin) VALUES (1, 'Причина 1', '2020-12-30', '2020-12-30')
INSERT INTO absence(ID_user, cause, date_start, date_fin) VALUES (1, 'Причина 2', '2020-12-30', '2020-12-30')
INSERT INTO absence(ID_user, cause, date_start, date_fin) VALUES (2, 'Причина 3', '2020-12-30', '2020-12-30')
GO
INSERT INTO education(ID_user, education) VALUES (1, 'MPT 1')
INSERT INTO education(ID_user, education) VALUES (1, 'MPT 2')
INSERT INTO education(ID_user, education) VALUES (2, 'MPT 3')
GO
INSERT INTO pay(ID_user, date, summ) VALUES (1, '2020-12-30', 100)
INSERT INTO pay(ID_user, date, summ) VALUES (1, '2020-12-30', 200)
INSERT INTO pay(ID_user, date, summ) VALUES (2, '2020-12-30', 300)
GO
INSERT INTO prize(ID_user, date, summ) VALUES (1, '2020-12-30', 100)
INSERT INTO prize(ID_user, date, summ) VALUES (1, '2020-12-30', 100)
INSERT INTO prize(ID_user, date, summ) VALUES (2, '2020-12-30', 100)
GO
INSERT INTO [type] VALUES ('No type')
INSERT INTO [type] VALUES ('Type1')
INSERT INTO [type] VALUES ('Type2')
GO
INSERT INTO tech (name, ID_type, photo, [desc], price, status_leas, status_rep, rep_text, discount) VALUES ('1', 1, NULL, '1', 1, 0, 0, '1', 0)
INSERT INTO tech (name, ID_type, photo, [desc], price, status_leas, status_rep, rep_text, discount) VALUES ('2', 2, NULL, '2', 2, 0, 0, '1', 0)
GO
INSERT INTO characts (ID_tech, name, val) VALUES (1, 'char1', 'val1')
INSERT INTO characts (ID_tech, name, val) VALUES (1, 'char2', 'val2')
INSERT INTO characts (ID_tech, name, val) VALUES (2, 'char3', 'val3')
GO
INSERT INTO leas (ID_user, address, data_start, data_fin, summ) VALUES (1, 'Адрес 1', '2020-12-30', '2020-12-30', '0')
INSERT INTO leas (ID_user, address, data_start, data_fin, summ) VALUES (1, 'Адрес 2', '2021-12-30', '2021-12-30', '0')
GO
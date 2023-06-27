-- DATABASE CREATION SECTION 
CREATE DATABASE TheRideYouRentPOE3
USE TheRideYouRentPOE3

-- TABLE CREATION SECTION 
CREATE TABLE CarMake (
MakeId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
[Description] VARCHAR(60) 
);

CREATE TABLE CarBodyType (
BodyTypeId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
[Description] VARCHAR(60)
);

CREATE TABLE Car (
    CarId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    CarNo VARCHAR(7) ,
    MakeId INT,
    Model VARCHAR(150),
    BodyTypeId INT,
    KilometresTravelled INT,
    ServiceKilometres INT,
    Available VARCHAR(3),
    FOREIGN KEY (MakeId) REFERENCES CarMake(MakeId),
    FOREIGN KEY (BodyTypeId) REFERENCES CarBodyType(BodyTypeId)
);

CREATE TABLE Driver (
DriverId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
[Name] VARCHAR(60),
[Address] VARCHAR(200),
Email VARCHAR(150),
Mobile VARCHAR(12)
);

CREATE TABLE Inspector (
InspectorId INT IDENTITY(1,1)  PRIMARY KEY NOT NULL,
InspectorNo VARCHAR(6),
[Name] VARCHAR(60),
Email VARCHAR(150),
Mobile VARCHAR(12),
);


CREATE TABLE Rental (
RentalId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
CarNo INT FOREIGN KEY REFERENCES Car(CarID),
InspectorNo INT FOREIGN KEY REFERENCES Inspector(InspectorId),
DriverId INT FOREIGN KEY REFERENCES Driver(DriverId),
RentalFee INT,
StartDate DATE,
EndDate DATE
);


CREATE TABLE [Return] (
ReturnId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
CarNo INT FOREIGN KEY REFERENCES Car(CarId),
InspectorNo INT FOREIGN KEY REFERENCES Inspector(InspectorId),
DriverId INT FOREIGN KEY REFERENCES Driver(DriverId),
ReturnDate DATE,
ElapsedDate INT,
Fine INT
);

CREATE TABLE [Login](
LoginId  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Username  INT FOREIGN KEY REFERENCES Inspector(inspectorId),
Password VARCHAR(250) 
);

-- TABLE INSERTION SECTION

INSERT INTO CarMake
VALUES ('Hyundai'),('BMW'),('Mercedes Benz'),('Toyota'),('Ford');

INSERT INTO CarBodyType
VALUES ('Hatchback'),('Sedan'),('Coupe'),('SUV');

INSERT INTO Car
VALUES
    ('HYU001', 1, 'Grand i10 1.0 Motion', 1, 1500, 15000, 'YES'),
    ('HYU002', 1, 'i20 1.2 Fluid', 1, 3000, 15000, 'YES'),
    ('BMW001', 2, '320d 1.2', 2, 20000, 50000, 'YES'),
    ('BMW002', 2, '240d 1.4', 2, 9500, 15000, 'YES'),
    ('TOY001', 4, 'Corolla 1.0', 2, 15000, 50000, 'YES'),
    ('TOY002', 4, 'Avanza 1.0', 4, 98000, 15000, 'YES'),
    ('TOY003', 4, 'Corolla Quest 1.0', 2, 15000, 50000, 'YES'),
    ('MER001', 3, 'c180', 2, 5200, 15000, 'YES'),
    ('MER002', 3, 'A200 Sedan', 2, 4080, 15000, 'YES'),
    ('FOR001', 5, 'Fiesta 1.0', 1, 7600, 15000, 'YES');



INSERT INTO Driver
VALUES ('Gabrielle Clarke','917 Heuvel St Botshabelo Free State 9781','gorix10987@macauvpn.com','0837113269'),
('Geoffrey Franklin','1114 Dorp St Paarl Western Cape 7655','noceti8743@drlatvia.com','0847728052'),
('Fawn Cooke','2158 Prospect St Garsfontein Gauteng 0042','yegifav388@enamelme.com','0821966584'),
('Darlene Peters','2529 St. John Street Somerset West Western Cape 7110','mayeka4267@macauvpn.com','0841221244'),
('Vita Soto','1474 Wolmarans St Sundra Mpumalanga 2200','wegog55107@drlatvia.com','0824567924'),
('Opal Rehbein','697 Thutlwa St Letaba Limpopo 0870','yiyow34505@enpaypal.com','0826864938'),
('Vernon Hodgson','1935 Thutlwa St Letsitele Limpopo 0885','gifeh11935@enamelme.com','0855991446'),
('Crispin Wheatly','330 Sandown Rd Cape Town Western Cape 8018','likon78255@macauvpn.com','0838347945'),
('Melanie Cunningham','616 Loop St Atlantis Western Cape 7350','sehapeb835@macauvpn.com','0827329001'),
('Kevin Peay','814 Daffodil Dr Elliotdale Eastern Cape 5118','xajic53991@enpaypal.com','0832077149');

INSERT INTO Inspector
VALUES ('101','Bud Barnes','bud@therideyourent.com',0821585359),
('102','Tracy Reeves','tracy@therideyourent.com',0822889988),
('103','Sandra Goodwin','sandra@therideyourent.com',0837695468),
('104','Shannon Burke','shannon@therideyourent.com',0836802514);

INSERT INTO Rental
VALUES (1,1,1,5000,'2021-08-30','2021-08-31'),
(2,1,1,5000,'2021-09-01','2021-09-10'),
(10,1,2,6500,'2021-09-01','2021-09-10'),
(4,2,5,7000,'2021-09-20','2021-09-25'),
(6,2,4,5000,'2021-10-03','2021-10-31'),
(8,3,4,8000,'2021-10-05','2021-10-15'),
(2,4,7,5000,'2021-10-01','2021-10-15'), 
(7,4,9,5000,'2021-08-10','2021-08-31');

INSERT INTO [Return]
VALUES (1,1,1,'2021-08-31',0,0),
(2,1,1,'2021-09-10',0,0),
(10,1,2,'2021-09-10',0,0),
(4,2,5,'2021-09-30',5,2500),
(6,2,4,'2021-11-02',2,1000), 
(8,3,4,'2021-10-16',1,500), 
(2,4,7,'2021-10-15',0,0), 
(7,4,9,'2021-08-31',0,0);	

insert [Login]
values ((1),('naith'))

-- STORED PROCEDURE SECTION 
DROP PROCEDURE IF EXISTS dbo.CalculateDriverFine

USE TheRideYouRentPOE3
GO

-- Create the CalculateDriverFine stored procedure
CREATE PROCEDURE dbo.CalculateDriverFine
    @DriverId INT
AS
BEGIN
    DECLARE @TotalFine INT
    
    -- Calculate the total fine for the driver
    SELECT @TotalFine = ElapsedDate * 500
    FROM [Return]
    WHERE DriverId = @DriverId
    
    -- Return the total fine
    SELECT @TotalFine AS DriverFine
END
GO



-- TABLE SELECTION SECTION 


-- Select all rows from CarMake table
SELECT * FROM CarMake;

-- Select all rows from CarBodyType table
SELECT * FROM CarBodyType;

-- Select all rows from Car table
SELECT * FROM Car;

-- Select all rows from Driver table
SELECT * FROM Driver;

-- Select all rows from Inspector table
SELECT * FROM Inspector;

-- Select all rows from Rental table
SELECT * FROM Rental;

-- Select all rows from [Return] table
SELECT * FROM [Return];

-- Select all rows from [Login] table
SELECT * FROM [Login];



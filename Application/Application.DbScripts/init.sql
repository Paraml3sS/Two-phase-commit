\connect postgresdb

CREATE SCHEMA SchemaOne;
CREATE SCHEMA SchemaTwo;
CREATE SCHEMA SchemaThree;


CREATE TABLE SchemaOne.HotelBooking(
    Id SERIAL PRIMARY KEY,
    ClientName VARCHAR(50) NOT NULL,
    HotelName VARCHAR(50) NOT NULL,
    Arrival TIMESTAMP NOT NULL,
    Departure TIMESTAMP NOT NULL
);

CREATE TABLE SchemaTwo.FlyBooking(
    Id SERIAL PRIMARY KEY,
    ClientName VARCHAR(50) NOT NULL,
    FlyNumber VARCHAR(50) NOT NULL,
    "From" VARCHAR(50) NOT NULL,
    "To" VARCHAR(50) NOT NULL,
    "Date" TIMESTAMP NOT NULL
);

CREATE TABLE SchemaThree.Account(
    Id SERIAL PRIMARY KEY,
    ClientName VARCHAR(50) NOT NULL,
    Amount INTEGER NOT NULL
);


ALTER TABLE SchemaThree.Account ADD CONSTRAINT AmountPositive CHECK(Amount >= 0);


INSERT INTO SchemaOne.HotelBooking(ClientName,HotelName,Arrival,Departure) VALUES('Жирний', 'Capella Ubud', '2020-01-08 04:05:06', '2021-01-08 04:05:06');
INSERT INTO SchemaOne.HotelBooking(ClientName,HotelName,Arrival,Departure) VALUES('Та', 'Amparo', '2020-01-08 04:05:06', '2021-01-08 04:05:06');
INSERT INTO SchemaOne.HotelBooking(ClientName,HotelName,Arrival,Departure) VALUES('Щасливий', 'Fogo Island Inn', '2020-01-08 04:05:06', '2021-01-08 04:05:06');

INSERT INTO SchemaTwo.FlyBooking(ClientName,FlyNumber,"From","To","Date") VALUES('Жирний', 'KLM 1382', 'Mexico City', 'Madrid', '2021-01-08 04:05:06');
INSERT INTO SchemaTwo.FlyBooking(ClientName,FlyNumber,"From","To","Date") VALUES('Та', 'BA2490', 'Warsaw', 'Osaka Kansai', '2021-01-08 04:05:06');
INSERT INTO SchemaTwo.FlyBooking(ClientName,FlyNumber,"From","To","Date") VALUES('Щасливий', 'UPS1654', 'Kuala Lumpur', 'Seattle', '2021-01-08 04:05:06');

INSERT INTO SchemaThree.Account(ClientName,Amount) VALUES('Жирний', 0);
INSERT INTO SchemaThree.Account(ClientName,Amount) VALUES('Та', 1000);
INSERT INTO SchemaThree.Account(ClientName,Amount) VALUES('Щасливий', 99999999);


\connect postgresdb

CREATE TABLE FlyBooking(
    Id SERIAL PRIMARY KEY,
    ClientName VARCHAR(50) NOT NULL,
    FlyNumber VARCHAR(50) NOT NULL,
    "From" VARCHAR(50) NOT NULL,
    "To" VARCHAR(50) NOT NULL,
    "Date" TIMESTAMP NOT NULL
);


INSERT INTO FlyBooking(ClientName,FlyNumber,"From","To","Date") VALUES('Жирний', 'KLM 1382', 'Mexico City', 'Madrid', '2021-01-08 04:05:06');
INSERT INTO FlyBooking(ClientName,FlyNumber,"From","To","Date") VALUES('Та', 'BA2490', 'Warsaw', 'Osaka Kansai', '2021-01-08 04:05:06');
INSERT INTO FlyBooking(ClientName,FlyNumber,"From","To","Date") VALUES('Щасливий', 'UPS1654', 'Kuala Lumpur', 'Seattle', '2021-01-08 04:05:06');
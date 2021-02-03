\connect postgresdb

CREATE SCHEMA schemaone;
CREATE SCHEMA schematwo;
CREATE SCHEMA schemathree;


CREATE TABLE schemaone.HotelBookings(
    Id SERIAL PRIMARY KEY,
    ClientName VARCHAR(50) NOT NULL,
    HotelName VARCHAR(50) NOT NULL,
    Arrival TIMESTAMP NOT NULL,
    Departure TIMESTAMP NOT NULL
);


insert into schemaone.HotelBookings(ClientName,HotelName,Arrival,Departure) values('Жирний', 'Rius', '2020-01-08 04:05:06', '2021-01-08 04:05:06');

insert into schemaone.HotelBookings(ClientName,HotelName,Arrival,Departure) values('Та', 'Rius', '2020-01-08 04:05:06', '2021-01-08 04:05:06');

insert into schemaone.HotelBookings(ClientName,HotelName,Arrival,Departure) values('Щасливий', 'Rius', '2020-01-08 04:05:06', '2021-01-08 04:05:06');

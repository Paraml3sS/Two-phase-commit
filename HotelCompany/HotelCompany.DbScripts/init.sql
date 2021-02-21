\connect postgresdb

CREATE TABLE HotelBooking(
    Id SERIAL PRIMARY KEY,
    ClientName VARCHAR(50) NOT NULL,
    HotelName VARCHAR(50) NOT NULL,
    Arrival TIMESTAMP NOT NULL,
    Departure TIMESTAMP NOT NULL
);

INSERT INTO HotelBooking(ClientName,HotelName,Arrival,Departure) VALUES('Жирний', 'Capella Ubud', '2020-01-08 04:05:06', '2021-01-08 04:05:06');
INSERT INTO HotelBooking(ClientName,HotelName,Arrival,Departure) VALUES('Та', 'Amparo', '2020-01-08 04:05:06', '2021-01-08 04:05:06');
INSERT INTO HotelBooking(ClientName,HotelName,Arrival,Departure) VALUES('Щасливий', 'Fogo Island Inn', '2020-01-08 04:05:06', '2021-01-08 04:05:06');

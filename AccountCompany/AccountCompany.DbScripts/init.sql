\connect postgresdb


CREATE TABLE Account(
    Id SERIAL PRIMARY KEY,
    ClientName VARCHAR(50) NOT NULL,
    Amount INTEGER NOT NULL
);


ALTER TABLE Account ADD CONSTRAINT AmountPositive CHECK(Amount >= 0);

INSERT INTO Account(ClientName,Amount) VALUES('Жирний', 0);
INSERT INTO Account(ClientName,Amount) VALUES('Та', 1000);
INSERT INTO Account(ClientName,Amount) VALUES('Щасливий', 99999999);
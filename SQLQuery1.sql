-- Create Database
CREATE DATABASE ParkingManagement;
USE ParkingManagement;

-- Table for User Accounts
CREATE TABLE Accounts (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL,
    role NVARCHAR(20) NOT NULL -- Roles: Admin, Employee, Customer, etc.
);

-- Table for Personal Information (ID links to Accounts)
CREATE TABLE PersonalInfo (
    ID INT PRIMARY KEY, -- Same as ID in Accounts
    name NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) NOT NULL UNIQUE, -- User's email address
    balance INT DEFAULT 0, -- Amount deposited (default is 0)
    parkingDate DATETIME, -- Date the vehicle was parked
    retrievalDate DATETIME, -- Date the vehicle was retrieved
    licensePlate NVARCHAR(20) NOT NULL, -- Vehicle's license plate number
    FOREIGN KEY (ID) REFERENCES Accounts(ID)
);

-- Table for Parking Lots with LotSector and LotNumber
CREATE TABLE ParkingLot (
    LotID INT IDENTITY(1,1) PRIMARY KEY,
    LotSector CHAR(1) NOT NULL, -- Sector identifier (e.g., A, B, C)
    UserID INT, -- Links to PersonalInfo (via Accounts)
    EmployeeID INT, -- Links to Accounts
    status NVARCHAR(50) NOT NULL, -- Status of parking lot (e.g., "Occupied", "Available")
    amount INT DEFAULT 0, -- Default amount is 0
    FOREIGN KEY (UserID) REFERENCES PersonalInfo(ID),
    FOREIGN KEY (EmployeeID) REFERENCES Accounts(ID)
);

-- Table for Ticket Types
CREATE TABLE TicketTypes (
    TypeID INT IDENTITY(1,1) PRIMARY KEY,
    typeName NVARCHAR(50) NOT NULL, -- e.g., "Single-use Ticket", "Monthly Ticket"
    price INT NOT NULL, -- Price for each ticket type
    validityDays INT, -- Validity period (in days), NULL if not applicable
    description NVARCHAR(255) -- Additional description of the ticket type
);

-- Table for Tickets
CREATE TABLE Tickets (
    TicketID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL, -- Links to PersonalInfo (via Accounts)
    TypeID INT NOT NULL, -- Links to TicketTypes
    purchaseDate DATETIME NOT NULL,
    expiryDate DATETIME, -- Expiry date, NULL for single-use tickets
    status NVARCHAR(20) NOT NULL, -- e.g., "Active", "Expired"
    FOREIGN KEY (UserID) REFERENCES PersonalInfo(ID),
    FOREIGN KEY (TypeID) REFERENCES TicketTypes(TypeID)
);

-- Insert Sample Data into Accounts
INSERT INTO Accounts (username, password, role) VALUES
('john_doe', 'password123', 'Customer'),
('jane_doe', 'password456', 'Employee'),
('admin_user', 'adminpassword', 'Admin');

-- Insert Sample Data into PersonalInfo
INSERT INTO PersonalInfo (ID, name, email, balance, parkingDate, retrievalDate, licensePlate) VALUES
(1, 'John Doe', 'john.doe@example.com', 100000, '2024-12-01 08:00:00', NULL, 'ABC-1234'),
(2, 'Jane Smith', 'jane.smith@example.com', 150000, '2024-12-02 09:00:00', NULL, 'XYZ-5678');

-- Insert Sample Data into ParkingLot
INSERT INTO ParkingLot (LotSector, UserID, EmployeeID, status, amount) VALUES
('A', 1, 2, 'Occupied', 20000), -- Parking lot A1, occupied by John Doe, managed by Employee ID = 2
('B', NULL, 2, 'Available', 0), -- Parking lot B1, available
('C', 2, 3, 'Occupied', 25000), -- Parking lot C1, occupied by Jane Smith, managed by Admin
('A', NULL, 3, 'Available', 0); -- Parking lot A2, available

-- Insert Sample Data into TicketTypes
INSERT INTO TicketTypes (typeName, price, validityDays, description) VALUES
('Single-use Ticket', 20000, NULL, 'Valid for one-time parking'),
('Monthly Ticket', 500000, 30, 'Valid for 30 days');

-- Insert Sample Data into Tickets
INSERT INTO Tickets (UserID, TypeID, purchaseDate, expiryDate, status) VALUES
(1, 1, '2024-12-01 08:00:00', NULL, 'Active'), -- Single-use ticket for John Doe
(1, 2, '2024-12-01 09:00:00', '2025-01-01 09:00:00', 'Active'), -- Monthly ticket for John Doe
(2, 1, '2024-12-02 09:00:00', NULL, 'Active'); -- Single-use ticket for Jane Smith

select * from ParkingLot
select * from Accounts
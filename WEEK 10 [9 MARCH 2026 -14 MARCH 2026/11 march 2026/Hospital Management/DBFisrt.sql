CREATE DATABASE HospitalDB;
GO
USE HospitalDB;

CREATE TABLE Patients (
    PatientId INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
    DateOfBirth DATE,
    ContactNumber NVARCHAR(20)
);

CREATE TABLE Doctors (
    DoctorId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Specialization NVARCHAR(100)
);

CREATE TABLE Appointments (
    AppointmentId INT PRIMARY KEY IDENTITY,
    PatientId INT FOREIGN KEY REFERENCES Patients(PatientId),
    DoctorId INT FOREIGN KEY REFERENCES Doctors(DoctorId),
    AppointmentDate DATETIME,
    Remarks NVARCHAR(500)
);
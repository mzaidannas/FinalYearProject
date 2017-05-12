
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/12/2016 11:27:00
-- Generated from EDMX file: D:\My All Documents\MyProfessionalWork\FinalYearProject\FinalYearProject\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PDA];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DoctorComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_DoctorComment];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_CommentPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_HistoryPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Patient_History] DROP CONSTRAINT [FK_Patient_HistoryPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_AppointmentDoctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Appointments] DROP CONSTRAINT [FK_AppointmentDoctor];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientAppointment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Appointments] DROP CONSTRAINT [FK_PatientAppointment];
GO
IF OBJECT_ID(N'[dbo].[FK_PrescriptionDoctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Prescriptions] DROP CONSTRAINT [FK_PrescriptionDoctor];
GO
IF OBJECT_ID(N'[dbo].[FK_PrescriptionPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Prescriptions] DROP CONSTRAINT [FK_PrescriptionPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctors] DROP CONSTRAINT [FK_DoctorPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonLogin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Logins] DROP CONSTRAINT [FK_PersonLogin];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Patients] DROP CONSTRAINT [FK_PersonPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorNotification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications] DROP CONSTRAINT [FK_DoctorNotification];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientNotification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications] DROP CONSTRAINT [FK_PatientNotification];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Doctors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctors];
GO
IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[Logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins];
GO
IF OBJECT_ID(N'[dbo].[PatientDoctors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientDoctors];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Patient_History]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patient_History];
GO
IF OBJECT_ID(N'[dbo].[Appointments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Appointments];
GO
IF OBJECT_ID(N'[dbo].[Prescriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Prescriptions];
GO
IF OBJECT_ID(N'[dbo].[Persons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons];
GO
IF OBJECT_ID(N'[dbo].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Doctors'
CREATE TABLE [dbo].[Doctors] (
    [Person_Id] int  NOT NULL,
    [Rating] float  NULL,
    [Career_Start] datetime  NULL,
    [Type] varchar(50)  NULL,
    [Average_Duration] time  NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [Person_Id] int  NOT NULL,
    [Blood_Group] varchar(50)  NULL,
    [Insurance_No] varchar(50)  NULL
);
GO

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [Person_Id] int  NOT NULL,
    [Username] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Email] varchar(50)  NOT NULL
);
GO

-- Creating table 'PatientDoctors'
CREATE TABLE [dbo].[PatientDoctors] (
    [Pat_Id] int  NOT NULL,
    [Doc_Id] int  NOT NULL,
    [Pat_Confidential] bit  NOT NULL,
    [Recovery_Status] int  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Comment_Id] int IDENTITY(1,1) NOT NULL,
    [Pat_Id] int  NOT NULL,
    [Doc_Id] int  NOT NULL,
    [Text] varchar(50)  NULL,
    [Rating] float  NULL,
    [Date] datetime  NULL,
    [Commenter] bit  NULL
);
GO

-- Creating table 'Patient_History'
CREATE TABLE [dbo].[Patient_History] (
    [Pat_Id] int  NOT NULL,
    [Confidential] varchar(50)  NULL,
    [Public] varchar(50)  NULL
);
GO

-- Creating table 'Appointments'
CREATE TABLE [dbo].[Appointments] (
    [Appointment_Id] int IDENTITY(1,1) NOT NULL,
    [Pat_Id] int  NOT NULL,
    [Doc_Id] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Start] time  NOT NULL,
    [End] time  NOT NULL
);
GO

-- Creating table 'Prescriptions'
CREATE TABLE [dbo].[Prescriptions] (
    [Prescription_Id] int IDENTITY(1,1) NOT NULL,
    [Pat_Id] int  NOT NULL,
    [Doc_Id] int  NOT NULL,
    [Medicine_Name] varchar(50)  NULL,
    [Type] varchar(50)  NULL,
    [Amount] int  NULL,
    [Frequency] int  NULL,
    [Purpose] varchar(50)  NULL
);
GO

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [Person_Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [CNIC] varchar(50)  NULL,
    [Address] varchar(50)  NULL,
    [Image_Path] varchar(50)  NULL,
    [DOB] datetime  NULL,
    [Gender] varchar(50)  NULL,
    [Phone] varchar(50)  NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [Notification_Id] int IDENTITY(1,1) NOT NULL,
    [Pat_Id] int NULL,
    [Doc_Id] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Message] varchar(50)  NULL,
    [StartTime] datetime  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Person_Id] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [PK_Doctors]
    PRIMARY KEY CLUSTERED ([Person_Id] ASC);
GO

-- Creating primary key on [Person_Id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([Person_Id] ASC);
GO

-- Creating primary key on [Person_Id] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([Person_Id] ASC);
GO

-- Creating primary key on [Pat_Id], [Doc_Id] in table 'PatientDoctors'
ALTER TABLE [dbo].[PatientDoctors]
ADD CONSTRAINT [PK_PatientDoctors]
    PRIMARY KEY CLUSTERED ([Pat_Id], [Doc_Id] ASC);
GO

-- Creating primary key on [Comment_Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Comment_Id] ASC);
GO

-- Creating primary key on [Pat_Id] in table 'Patient_History'
ALTER TABLE [dbo].[Patient_History]
ADD CONSTRAINT [PK_Patient_History]
    PRIMARY KEY CLUSTERED ([Pat_Id] ASC);
GO

-- Creating primary key on [Appointment_Id] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [PK_Appointments]
    PRIMARY KEY CLUSTERED ([Appointment_Id] ASC);
GO

-- Creating primary key on [Prescription_Id] in table 'Prescriptions'
ALTER TABLE [dbo].[Prescriptions]
ADD CONSTRAINT [PK_Prescriptions]
    PRIMARY KEY CLUSTERED ([Prescription_Id] ASC);
GO

-- Creating primary key on [Person_Id] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [PK_Persons]
    PRIMARY KEY CLUSTERED ([Person_Id] ASC);
GO

-- Creating primary key on [Notification_Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([Notification_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Doc_Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_DoctorComment]
    FOREIGN KEY ([Doc_Id])
    REFERENCES [dbo].[Doctors]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorComment'
CREATE INDEX [IX_FK_DoctorComment]
ON [dbo].[Comments]
    ([Doc_Id]);
GO

-- Creating foreign key on [Pat_Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentPatient]
    FOREIGN KEY ([Pat_Id])
    REFERENCES [dbo].[Patients]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentPatient'
CREATE INDEX [IX_FK_CommentPatient]
ON [dbo].[Comments]
    ([Pat_Id]);
GO

-- Creating foreign key on [Pat_Id] in table 'Patient_History'
ALTER TABLE [dbo].[Patient_History]
ADD CONSTRAINT [FK_Patient_HistoryPatient]
    FOREIGN KEY ([Pat_Id])
    REFERENCES [dbo].[Patients]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Doc_Id] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [FK_AppointmentDoctor]
    FOREIGN KEY ([Doc_Id])
    REFERENCES [dbo].[Doctors]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AppointmentDoctor'
CREATE INDEX [IX_FK_AppointmentDoctor]
ON [dbo].[Appointments]
    ([Doc_Id]);
GO

-- Creating foreign key on [Pat_Id] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [FK_PatientAppointment]
    FOREIGN KEY ([Pat_Id])
    REFERENCES [dbo].[Patients]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientAppointment'
CREATE INDEX [IX_FK_PatientAppointment]
ON [dbo].[Appointments]
    ([Pat_Id]);
GO

-- Creating foreign key on [Doc_Id] in table 'Prescriptions'
ALTER TABLE [dbo].[Prescriptions]
ADD CONSTRAINT [FK_PrescriptionDoctor]
    FOREIGN KEY ([Doc_Id])
    REFERENCES [dbo].[Doctors]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PrescriptionDoctor'
CREATE INDEX [IX_FK_PrescriptionDoctor]
ON [dbo].[Prescriptions]
    ([Doc_Id]);
GO

-- Creating foreign key on [Pat_Id] in table 'Prescriptions'
ALTER TABLE [dbo].[Prescriptions]
ADD CONSTRAINT [FK_PrescriptionPatient]
    FOREIGN KEY ([Pat_Id])
    REFERENCES [dbo].[Patients]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PrescriptionPatient'
CREATE INDEX [IX_FK_PrescriptionPatient]
ON [dbo].[Prescriptions]
    ([Pat_Id]);
GO

-- Creating foreign key on [Person_Id] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [FK_DoctorPerson]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[Persons]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Person_Id] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [FK_PersonLogin]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[Persons]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Person_Id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [FK_PersonPatient]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[Persons]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Doc_Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_DoctorNotification]
    FOREIGN KEY ([Doc_Id])
    REFERENCES [dbo].[Doctors]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorNotification'
CREATE INDEX [IX_FK_DoctorNotification]
ON [dbo].[Notifications]
    ([Doc_Id]);
GO

-- Creating foreign key on [Pat_Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_PatientNotification]
    FOREIGN KEY ([Pat_Id])
    REFERENCES [dbo].[Patients]
        ([Person_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientNotification'
CREATE INDEX [IX_FK_PatientNotification]
ON [dbo].[Notifications]
    ([Pat_Id]);
GO
INSERT INTO [dbo].[Persons] ([Name], [CNIC], [Address], [Image_Path], [DOB], [Gender], [Phone]) VALUES (N'Ali', N'35202-4345345-1', N'Lahore', N'/images/team/team_member_HGA.jpg', N'19930225 10:34:09 PM', N'Male', N'0322800791523')
INSERT INTO [dbo].[Persons] ([Name], [CNIC], [Address], [Image_Path], [DOB], [Gender], [Phone]) VALUES (N'Umer', N'30101-3543546-3', N'Karachi', N'/images/team/team_member_JZD.jpg', N'19870618 10:34:09 AM', N'Male', N'51293681258')
INSERT INTO [dbo].[Persons] ([Name], [CNIC], [Address], [Image_Path], [DOB], [Gender], [Phone]) VALUES (N'Usman', N'35934-5894349-5', N'Islamabad', N'/images/team/team_member_SMQA.jpg', N'19900623 10:34:09 AM', N'Male', N'08612761253')
INSERT INTO [dbo].[Persons] ([Name], [CNIC], [Address], [Image_Path], [DOB], [Gender], [Phone]) VALUES (N'Alisha', N'58393-5944843-4', N'Faislabad', N'/images/team/team_member_ZA.jpg', N'19860801 10:34:09 AM', N'Female', N'78923678123')
INSERT INTO [dbo].[Persons] ([Name], [CNIC], [Address], [Image_Path], [DOB], [Gender], [Phone]) VALUES (N'Usama', N'49443-4883994-4', N'Peshawar', N'/images/team/team_member_JZD.jpg', N'19030509 10:34:09 AM', N'Male', N'8796872345678')

INSERT INTO [dbo].[Doctors] ([Person_Id], [Rating], [Career_Start], [Type], [Average_Duration]) VALUES (1, 3.1, N'20100225 00:00:00 AM', N'Sergon', N'00:15:00')
INSERT INTO [dbo].[Doctors] ([Person_Id], [Rating], [Career_Start], [Type], [Average_Duration]) VALUES (5, 3.2, N'20080725 00:00:00 AM', N'Heart', N'00:20:00')

INSERT INTO [dbo].[Patients] ([Person_Id], [Blood_Group], [Insurance_No]) VALUES (2, N'B+', N'4637246623')
INSERT INTO [dbo].[Patients] ([Person_Id], [Blood_Group], [Insurance_No]) VALUES (3, N'A+', N'759370954')
INSERT INTO [dbo].[Patients] ([Person_Id], [Blood_Group], [Insurance_No]) VALUES (4, N'B-', N'75984')

INSERT INTO [dbo].[Logins] ([Person_Id], [Username], [Password], [Email]) VALUES (1, N'ali', N'1234', N'bcsf12a527@pucit.edu.pk')
INSERT INTO [dbo].[Logins] ([Person_Id], [Username], [Password], [Email]) VALUES (2, N'umer', N'12345', N'bcsf12a513@pucit.edu.pk')
INSERT INTO [dbo].[Logins] ([Person_Id], [Username], [Password], [Email]) VALUES (3, N'usman', N'1234', N'bcsf12a533@pucit.edu.pk')
INSERT INTO [dbo].[Logins] ([Person_Id], [Username], [Password], [Email]) VALUES (4, N'alisha', N'1234', N'bcsf12a537@pucit.edu.pk')
INSERT INTO [dbo].[Logins] ([Person_Id], [Username], [Password], [Email]) VALUES (5, N'usama', N'12345', N'hgabbas1122@gmail.com')

INSERT INTO [dbo].[Patient_History] ([Pat_Id], [Confidential], [Public]) VALUES (2, N'confidential', N'public')
INSERT INTO [dbo].[Patient_History] ([Pat_Id], [Confidential], [Public]) VALUES (3, N'confidential', N'public')
INSERT INTO [dbo].[Patient_History] ([Pat_Id], [Confidential], [Public]) VALUES (4, N'confidential', N'public')

INSERT INTO [dbo].[PatientDoctors] ([Pat_Id], [Doc_Id], [Pat_Confidential], [Recovery_Status]) VALUES (2, 1, 1, 55)
INSERT INTO [dbo].[PatientDoctors] ([Pat_Id], [Doc_Id], [Pat_Confidential], [Recovery_Status]) VALUES (3, 5, 0, 80)
INSERT INTO [dbo].[PatientDoctors] ([Pat_Id], [Doc_Id], [Pat_Confidential], [Recovery_Status]) VALUES (4, 1, 1, 90)

INSERT INTO [dbo].[Appointments] ([Pat_Id], [Doc_Id], [Date], [Start], [End]) VALUES (2, 1, N'20160625 09:30:00 PM', N'09:30:00', N'09:45:00')
INSERT INTO [dbo].[Appointments] ([Pat_Id], [Doc_Id], [Date], [Start], [End]) VALUES (3, 1, N'20160620 06:30:00 PM', N'06:30:00', N'06:45:00')
INSERT INTO [dbo].[Appointments] ([Pat_Id], [Doc_Id], [Date], [Start], [End]) VALUES (4, 5, N'20160615 04:15:00 PM', N'04:15:00', N'04:35:00')
INSERT INTO [dbo].[Appointments] ([Pat_Id], [Doc_Id], [Date], [Start], [End]) VALUES (3, 5, N'20160610 02:00:00 PM', N'02:00:00', N'02:20:00')

INSERT INTO [dbo].[Notifications] ([Pat_Id], [Doc_Id], [Name], [Message], [StartTime], [Status]) VALUES (2, 1, 'Umer', N'', '20160625 05:00:00 PM', 'false')
INSERT INTO [dbo].[Notifications] ([Pat_Id], [Doc_Id], [Name], [Message], [StartTime], [Status]) VALUES (3, 1, 'Usman', N'', '20160620 09:30:00 PM', 'false')
INSERT INTO [dbo].[Notifications] ([Pat_Id], [Doc_Id], [Name], [Message], [StartTime], [Status]) VALUES (4, 5, 'Alisha', N'', '20160615 02:15:00 PM', 'false')
INSERT INTO [dbo].[Notifications] ([Pat_Id], [Doc_Id], [Name], [Message], [StartTime], [Status]) VALUES (3, 5, 'Usman', N'', '20160610 07:45:00 PM', 'false')
INSERT INTO [dbo].[Notifications] ([Doc_Id], [Name], [Message], [StartTime], [Status]) VALUES (5, 'Zaid', N'', '20160619 09:30:00 PM', 'false')

INSERT INTO [dbo].[Prescriptions] ([Pat_Id], [Doc_Id], [Medicine_Name], [Type], [Amount], [Frequency], [Purpose]) VALUES (2, 1, N'Running', N'Excercise', 2, 2, N'For your muscles')
INSERT INTO [dbo].[Prescriptions] ([Pat_Id], [Doc_Id], [Medicine_Name], [Type], [Amount], [Frequency], [Purpose]) VALUES (3, 1, N'PRESCRIPTION NO 2', N'Diet', 1, 2, N'Eat with low fats')
INSERT INTO [dbo].[Prescriptions] ([Pat_Id], [Doc_Id], [Medicine_Name], [Type], [Amount], [Frequency], [Purpose]) VALUES (4, 5, N'Panadol', N'Medicine', 2, 1, N'In case of high fever')

INSERT INTO [dbo].[Comments] ([Pat_Id], [Doc_Id], [Date], [Rating], [Text], [Commenter]) VALUES (2, 1, N'20160125 10:34:09 PM', 4.8, N'This is comment of  pat_id 2', 0)
INSERT INTO [dbo].[Comments] ([Pat_Id], [Doc_Id], [Date], [Rating], [Text], [Commenter]) VALUES (3, 5, N'20160225 10:34:09 PM', 3.5, N'this is comment of  pat_id 3', 0)
INSERT INTO [dbo].[Comments] ([Pat_Id], [Doc_Id], [Date], [Rating], [Text], [Commenter]) VALUES (4, 1, N'20160225 10:34:09 PM', 4.1, N'this is comment of pat_id 4', 0)
INSERT INTO [dbo].[Comments] ([Pat_Id], [Doc_Id], [Date], [Rating], [Text], [Commenter]) VALUES (2, 5, N'20160225 10:34:09 PM', 4.9, N'this is comment of doc_id 5', 1)
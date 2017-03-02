using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public interface IDoctor
    {
        List<Doctor> GetAllDoctors();
        List<Full_Patient> GetAllPatients(int id);
        bool AddPatient(Full_Patient full_patient, int DoctorID);
        Full_Patient PatientDetails(int id);
        void SaveEditedPatient(Full_Patient full_patient);
        void AddDoctor(Doctor doc);
        void SaveDoctor(Doctor doc);
        bool DeletePatient(int pat_id,int doc_id);
        Doctor Details(int id);
        Doctor GetDoctor(int id);
        bool DeleteDoctor(int id);
        IQueryable<Doctor> SearchIndex(string searchString);
        List<Appointment> GetAppointmentList(int id);
        List<Notification> GetNotificationList(int id);
        List<Events> GetCalenderEvents(int id);
        void AddAppointment(Notification notification, int docID);
        Full_Doctor GetFullDoctorForProfile(int? id);
        List<Comment> GetCommentOfDoctor(int docID);
        bool AddPrescription(Prescription prescription, int doctorID);
        List<string> GetPatientHistory(int pat_id, int doc_id);
        List<Prescription> GetAllPrescriptions(int id, int doc);
        Prescription GetPrescription(int id);
        bool DeletePrescription(int id);
        void EditPrescription(Prescription p, int id);
        List<DateTime> GetDoctorSlots(int doc_id, string date);
        void delete_notification(int id);
    }
}
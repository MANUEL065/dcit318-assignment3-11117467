// HealthcareModels.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCareManagement
{
    /// <summary>
    /// Generic repository for entity management.
    /// </summary>
    /// <typeparam name="T">reference type (class)</typeparam>
    public class Repository<T> where T : class
    {
        private readonly List<T> items = new();

        // Add an item to the repository
        public void Add(T item) => items.Add(item);

        // Return a shallow copy of all items
        public List<T> GetAll() => new List<T>(items);

        // Return first item matching predicate or null
        public T? GetById(Func<T, bool> predicate) => items.FirstOrDefault(predicate);

        // Remove first item matching predicate; return true if removed
        public bool Remove(Func<T, bool> predicate)
        {
            var item = items.FirstOrDefault(predicate);
            if (item is null) return false;
            return items.Remove(item);
        }
    }

    /// <summary>
    /// Patient model
    /// Fields: int Id, string Name, int Age, string Gender
    /// </summary>
    public class Patient
    {
        public int Id { get; }
        public string Name { get; }
        public int Age { get; }
        public string Gender { get; }

        public Patient(int id, string name, int age, string gender)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Age = age;
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
        }

        public override string ToString() => $"Id: {Id}, Name: {Name}, Age: {Age}, Gender: {Gender}";
    }

    /// <summary>
    /// Prescription model
    /// Fields: int Id, int PatientId, string MedicationName, DateTime DateIssued
    /// </summary>
    public class Prescription
    {
        public int Id { get; }
        public int PatientId { get; }
        public string MedicationName { get; }
        public DateTime DateIssued { get; }

        public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
        {
            Id = id;
            PatientId = patientId;
            MedicationName = medicationName ?? throw new ArgumentNullException(nameof(medicationName));
            DateIssued = dateIssued;
        }

        public override string ToString() =>
            $"Prescription Id: {Id}, PatientId: {PatientId}, Medication: {MedicationName}, Issued: {DateIssued:d}";
    }
}

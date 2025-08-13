using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCareManagement
{

    /// HealthSystemApp implements the logic required by the assignment.
    /// NOTE: No Main() inside this file. Use Program.cs to call Run().

    public class HealthCareApp
    {
        // Fields required by the spec
        private readonly Repository<Patient> _patientRepo = new();
        private readonly Repository<Prescription> _prescriptionRepo = new();
        private readonly Dictionary<int, List<Prescription>> _prescriptionMap = new();

        
        /// Seed repository with 2–3 patients and 4–5 prescriptions (matching PatientIds).
        public void SeedData()
        {
            // Patients
            _patientRepo.Add(new Patient(1, "John Doe", 34, "Male"));
            _patientRepo.Add(new Patient(2, "Alice Park", 28, "Female"));
            _patientRepo.Add(new Patient(3, "Samuel Brown", 52, "Male"));

            // Prescriptions (PatientId values must match patients above)
            _prescriptionRepo.Add(new Prescription(1, 1, "Amoxicillin 500mg", DateTime.Now.AddDays(-10)));
            _prescriptionRepo.Add(new Prescription(2, 1, "Ibuprofen 200mg", DateTime.Now.AddDays(-6)));
            _prescriptionRepo.Add(new Prescription(3, 2, "Paracetamol 500mg", DateTime.Now.AddDays(-4)));
            _prescriptionRepo.Add(new Prescription(4, 3, "Vitamin D 1000IU", DateTime.Now.AddDays(-2)));
            _prescriptionRepo.Add(new Prescription(5, 2, "Cetirizine 10mg", DateTime.Now.AddDays(-1)));
        }

      
        /// Build _prescriptionMap by grouping prescriptions by PatientId.
        
        public void BuildPrescriptionMap()
        {
            _prescriptionMap.Clear();

            foreach (var prescription in _prescriptionRepo.GetAll())
            {
                if (!_prescriptionMap.TryGetValue(prescription.PatientId, out var list))
                {
                    list = new List<Prescription>();
                    _prescriptionMap[prescription.PatientId] = list;
                }

                list.Add(prescription);
            }
        }


        /// Returns prescriptions for a given patient id using the map.

        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            if (_prescriptionMap.TryGetValue(patientId, out var list))
                return new List<Prescription>(list); // return copy

            return new List<Prescription>();
        }


        /// Print all patients stored in the repository.
        public void PrintAllPatients()
        {
            var patients = _patientRepo.GetAll();
            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found.");
                return;
            }

            Console.WriteLine("Patients:");
            foreach (var p in patients)
            {
                Console.WriteLine($"  {p}");
            }
        }


        /// Print prescriptions for a specific patient (by id) using the prescription map.

        public void PrintPrescriptionsForPatient(int patientId)
        {
            var patient = _patientRepo.GetById(p => p.Id == patientId);

            if (patient is null)
            {
                Console.WriteLine($"Patient with Id {patientId} not found.");
                return;
            }

            var prescriptions = GetPrescriptionsByPatientId(patientId);

            Console.WriteLine($"\nPrescriptions for {patient.Name} (Id: {patientId}):");
            if (prescriptions.Count == 0)
            {
                Console.WriteLine("  No prescriptions found for this patient.");
                return;
            }

            foreach (var pres in prescriptions.OrderBy(p => p.DateIssued))
            {
                Console.WriteLine($"  {pres}");
            }
        }


        /// A simple Run() that executes the required flow:
        /// SeedData -> BuildPrescriptionMap -> PrintAllPatients -> ask user and print prescriptions for chosen patient.
        /// Program.cs should call this when the user selects option 2.
        public void Run()
        {
            SeedData();
            BuildPrescriptionMap();
            PrintAllPatients();

            Console.Write("\nEnter a Patient Id to display prescriptions (or press Enter to skip): ");
            string? raw = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(raw))
            {
                Console.WriteLine("Skipping prescription lookup.");
                return;
            }

            if (!int.TryParse(raw, out int pid))
            {
                Console.WriteLine("Invalid Patient Id input.");
                return;
            }

            PrintPrescriptionsForPatient(pid);
        }
    }
}
